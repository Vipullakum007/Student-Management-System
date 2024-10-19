using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Management
{
    public partial class student_index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fetchStudentDetails();
                
            }
            lblName.Text = Session["StudentName"].ToString();
            BindEnrolledCourses();
        }
        private void fetchStudentDetails()
        {
            
            string studentEmail = Session["StudentEmail"] as string;

            if (string.IsNullOrEmpty(studentEmail))
            {
                // not login then redirect to the login page
                Response.Redirect("home.aspx?message=Please+log+in+as+Student+to+see+this+page");
                //Response.Redirect("home.aspx");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = @"
        SELECT Id, Name, Dob, Branch, CurrentCPI, Semester, Class_ID, Email 
        FROM [dbo].[Students] 
        WHERE Email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameter for the email
                    cmd.Parameters.AddWithValue("@Email", studentEmail);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        // Check if any records were returned
                        if (dt.Rows.Count > 0)
                        {
                            gvStudentDetails.DataSource = dt;
                            gvStudentDetails.DataBind();
                        }
                        else
                        {
                            // Handle case where no student record is found
                            // e.g., show a message or redirect
                            lblMessage.Text = "No student details found.";
                        }
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Enrollment.aspx");
        }

        private void BindEnrolledCourses()
        {
            string studentId = GetStudentId();  // Assume this gets the logged-in student's ID from session or query string

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = @"
            SELECT c.Id, c.Name, g.InternalMarks, g.ExternalMarks, g.PracticalMarks, g.Grade
            FROM Grades g
            INNER JOIN Courses c ON g.CourseID = c.Id
            WHERE g.StudentID = @StudentID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            gvEnrolledCourses.DataSource = reader;
                            gvEnrolledCourses.DataBind();
                        }
                    }
                }
            }
        }

        private string GetStudentId()
        {
            // Logic to retrieve student ID, either from session or query string
            return Session["StudentId"]?.ToString();
        }
    }
}