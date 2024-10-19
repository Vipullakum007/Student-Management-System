using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Student_Management
{
	public partial class Enrollment : System.Web.UI.Page
	{
        DataSet ds1;
        string branch;
        string branchId;
        string sem;
        string email;
		protected void Page_Load(object sender, EventArgs e)
		{
            if(Session["StudentName"] == null)
            {            
                Response.Redirect("~/home.aspx");
            }
            lblUsername.Text = Session["StudentName"].ToString();
            email = Session["StudentEmail"].ToString();
            if (!IsPostBack)
            {
                FetchBranchAndSemester(email);
                fillCourseList();
            }
        }

        private void FetchBranchAndSemester(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = "SELECT Branch, Semester FROM [dbo].[Students] WHERE Email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            branch = reader["Branch"].ToString();
                            sem = reader["Semester"].ToString();
                        }
                    }
                }
                string query2 = "Select BranchID from [dbo].[Branch] WHERE Code = @bName" ;
                using (SqlCommand cmd = new SqlCommand(query2, conn))
                {
                    cmd.Parameters.AddWithValue("@bName", branch);
                    

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            branchId = reader["BranchID"].ToString();
                        }
                    }
                }
            }
            
        }


        protected void fillCourseList()
        {

            string connectionString = WebConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            //string query = "SELECT  Id, Name FROM Courses WHERE Branch = @Branch and semseter=@sem"; // Adjust the query according to your database schema
            string query = @"
        SELECT c.Id, c.Name 
        FROM Courses c 
        WHERE c.Branch = @Branch AND c.semseter = @sem 
        AND c.Id NOT IN (
            SELECT e.CourseID 
            FROM Enrollments e 
            WHERE e.StudentID = (
                SELECT s.Id 
                FROM Students s 
                WHERE s.Email = @Email
            )
        )";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Branch", Int32.Parse(branchId));
                cmd.Parameters.AddWithValue("@sem", Int32.Parse(sem));
                cmd.Parameters.AddWithValue("@Email", email);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlCourse.Items.Clear();

                    ddlCourse.Items.Add(new ListItem("Select Course", "0"));

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string courseName = reader["Name"].ToString();
                            string courseId = reader["Id"].ToString();
                            ddlCourse.Items.Add(new ListItem(courseName, courseId));
                        }
                    }
                    else
                    {
                        ddlCourse.Items.Add(new ListItem("No courses available", "0"));
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    //Response.Write($"An error occurred: {ex.Message}");
                    lblMessage.Text = ex.Message;
                }
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        
        protected void btnEnroll_Click(object sender, EventArgs e)
        {
            // Check if session has expired
            if (Session["StudentName"] == null)
            {
                Response.Redirect("~/home.aspx");
                return;
            }

            string studentName = Session["StudentName"].ToString();
            string courseId = ddlCourse.SelectedValue;
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Retrieve StudentID based on StudentName
                string queryGetStudentId = "SELECT Id FROM Students WHERE Name = @Name";
                SqlCommand cmdGetStudentId = new SqlCommand(queryGetStudentId, conn);
                cmdGetStudentId.Parameters.AddWithValue("@Name", studentName);

                object result = cmdGetStudentId.ExecuteScalar();

                if (result != null)
                {
                    int studentId = (int)result;
                    /*
                    string queryEnroll = "INSERT INTO Grades (StudentID,CourseID,InternalMarks,ExternalMarks,PracticalMarks,Grade) VALUES (@StudentID , @CourseID,0,0,0,'-')";
                    SqlCommand cmdEnroll = new SqlCommand(queryEnroll, conn);
                    cmdEnroll.Parameters.AddWithValue("@StudentID", studentId);
                    cmdEnroll.Parameters.AddWithValue("@CourseID", courseId);

                    cmdEnroll.ExecuteNonQuery();
                    */
                    string checkEnrollmentQuery = "SELECT COUNT(*) FROM Enrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
                    SqlCommand cmdCheckEnrollment = new SqlCommand(checkEnrollmentQuery, conn);
                    cmdCheckEnrollment.Parameters.AddWithValue("@StudentID", studentId);
                    cmdCheckEnrollment.Parameters.AddWithValue("@CourseID", courseId);

                    int enrollmentCount = (int)cmdCheckEnrollment.ExecuteScalar();

                    if (enrollmentCount == 0)
                    {
                        // Insert into Enrollment table
                        string queryEnroll = "INSERT INTO Enrollments (StudentID, CourseID) VALUES (@StudentID, @CourseID)";
                        SqlCommand cmdEnroll = new SqlCommand(queryEnroll, conn);
                        cmdEnroll.Parameters.AddWithValue("@StudentID", studentId);
                        cmdEnroll.Parameters.AddWithValue("@CourseID", courseId);
                        
                        cmdEnroll.ExecuteNonQuery();

                        string queryEnrollToGrade = "INSERT INTO Grades (StudentID,CourseID,InternalMarks,ExternalMarks,PracticalMarks,Grade) VALUES (@StudentID , @CourseID,0,0,0,'-')";
                        SqlCommand cmdEnrollToGrade = new SqlCommand(queryEnrollToGrade, conn);
                        cmdEnrollToGrade.Parameters.AddWithValue("@StudentID", studentId);
                        cmdEnrollToGrade.Parameters.AddWithValue("@CourseID", courseId);

                        cmdEnrollToGrade.ExecuteNonQuery();

                        lblMessage.Text = "Enrollment successful!";
                    }
                    else
                    {
                        lblMessage.Text = "You are already enrolled in this course!";
                    }

                }
                else
                {
                    lblMessage.Text = "Student not found!";
                }

                conn.Close();
            }
        }

    }
}