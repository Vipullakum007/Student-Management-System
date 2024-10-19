using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Management
{
    public partial class professor_index : System.Web.UI.Page
    {
        string profName;
        protected int profId;
        string selectedCourseId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["ProfessorName"] == null)
            {
                Response.Redirect("~/home.aspx?message=Please+log+in+as+Professor+to+see+this+page");
            }
            lblProfName.Text = Session["ProfessorName"].ToString();
            profName = Session["ProfessorName"].ToString();
            if (!IsPostBack)
            {
                fillCourseList();
                //BindGrades();
                GridView2_DataBound(this, EventArgs.Empty);
            }
        }

        protected void fillCourseList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string getProfIdQuery = "SELECT Prof_ID FROM [dbo].[Profesors] WHERE Name = @ProfessorName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(getProfIdQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ProfessorName", profName); // Assuming profName is globally set

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        profId = Convert.ToInt32(result);
                    }
                    else
                    {
                        // Handle case where the professor's ID is not found
                        lblMessage.Text = "Professor not found.";
                        return;
                    }
                    conn.Close();
                }
            }

            // Step 2: Use ProfID to find the courses taught by this professor
            string getCoursesQuery = @"
                SELECT c.Id, c.Name 
                FROM [dbo].[Courses] c
                WHERE c.Taught_by = @ProfID";

            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(getCoursesQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ProfID", profId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        ddlCourse.Items.Clear();

                        ListItem defaultItem = new ListItem("Please Select Course", "-1");
                        ddlCourse.Items.Add(defaultItem);

                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader["Name"].ToString(), reader["Id"].ToString());
                            ddlCourse.Items.Add(item);
                        }
                    }
                    else
                    {
                        ddlCourse.Items.Clear();
                        ddlCourse.Items.Add(new ListItem("No courses available", ""));
                    }
                    reader.Close();
                }
            }
            
        }

        protected void GridView2_DataBound(object sender, EventArgs e)
        {
            if (GridView2.Rows.Count == 0)
            {
                //lblMessage.Text = "No students are enrolled for the selected course.";
            }
            else
            {
                //lblMessage.Text = string.Empty;
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCourseId = ddlCourse.SelectedValue; // Get the selected course ID
            
            HttpCookie courseCookie = new HttpCookie("SelectedCourseID");
            courseCookie.Value = selectedCourseId;
            courseCookie.Expires = DateTime.Now.AddHours(1); // Set cookie expiration
            Response.Cookies.Add(courseCookie);  
            GridView2_DataBound(this, EventArgs.Empty);
            //BindGrades();
        }

        private void BindGrades()
        {
            // Retrieve the selected course ID from the cookie
            string selectedCourseIdStr = Request.Cookies["SelectedCourseID"]?.Value;

            // Ensure selectedCourseId is valid
            if (string.IsNullOrEmpty(selectedCourseIdStr) || !int.TryParse(selectedCourseIdStr, out int selectedCourseId))
            {
                lblMessage.Text = "Invalid course selection.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string getGradesQuery = @"
        SELECT g.StudentID, g.InternalMarks, g.ExternalMarks, g.PracticalMarks, g.Grade
        FROM [dbo].[Grades] g
        WHERE g.CourseID = ' "  + selectedCourseIdStr + " ' ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(getGradesQuery, conn))
                {
                    //cmd.Parameters.AddWithValue("@CourseID", selectedCourseId);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        GridView2.DataSource = reader;
                        GridView2.DataBind();
                    }
                    else
                    {
                        GridView2.DataSource = null;
                        GridView2.DataBind();
                        lblMessage.Text = "No grades found for the selected course.";
                    }
                    reader.Close();
                }
            }
        }

        int totalMarks = 0;
        int passingMarks = 0;
        bool isValid = false;
        private string CalculateGrade(int internalMarks, int externalMarks, int practicalMarks)
        {

            int obtainedMarks = internalMarks + externalMarks + practicalMarks;
            
            if (obtainedMarks < passingMarks)
                return "FF";

            double percentage = (double)obtainedMarks / totalMarks * 100;

            if (obtainedMarks > (0.845 * totalMarks))
                return "AA";
            else if (obtainedMarks > (0.745 * totalMarks))
                return "AB";
            else if (obtainedMarks > (0.645 * totalMarks))
                return "BB";
            else if (obtainedMarks > (0.545 * totalMarks))
                return "BC";
            else
                return "CC";
        }
       protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int internalMarks = Convert.ToInt32(e.NewValues["InternalMarks"]);
            int externalMarks = Convert.ToInt32(e.NewValues["ExternalMarks"]);
            int practicalMarks = Convert.ToInt32(e.NewValues["PracticalMarks"]);

            // Calculate the new grade
            isValid = GetMarksDetails(internalMarks,externalMarks,practicalMarks);
            if (!isValid)
            {
                e.Cancel = true;  // Cancels the update process, keeping old values
                return;
            }

            string newGrade = CalculateGrade(internalMarks, externalMarks, practicalMarks);
            e.NewValues["Grade"] = newGrade;

            
        }

        private bool GetMarksDetails(int internalMarks, int externalMarks, int practicalMarks)
        {
            string selectedCourseIdStr = Request.Cookies["SelectedCourseID"]?.Value;
            if (string.IsNullOrEmpty(selectedCourseIdStr) || !int.TryParse(selectedCourseIdStr, out int selectedCourseId))
            {
                throw new Exception("Invalid or missing course ID.");
            }

            //Response.Write("course id : " + selectedCourseId); 

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = @"
        SELECT Internal_marks, External_marks, Practical_marks, Passing_marks
        FROM [dbo].[Courses]
        WHERE Id = @CourseID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", selectedCourseId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int maxInternalMarks = Convert.ToInt32(reader["Internal_marks"]);
                            int maxExternalMarks = Convert.ToInt32(reader["External_marks"]);
                            int maxPracticalMarks = Convert.ToInt32(reader["Practical_marks"]);
                            passingMarks = Convert.ToInt32(reader["Passing_marks"]);

                            totalMarks = maxInternalMarks + maxExternalMarks + maxPracticalMarks;
                            if(internalMarks > maxInternalMarks || internalMarks < 0)
                            {
                                Response.Write("<script>alert('Wrong internal marks')</script>");
                                lblMessage.Text = "maximum Internal marks is = " + maxInternalMarks;
                                lblMessage.Text = "minimum Internal marks is = 0";
                                return false;
                            }
                            if(externalMarks > maxExternalMarks || externalMarks < 0)
                            {
                                Response.Write("<script>alert('Wrong external marks')</script>");
                                lblMessage.Text = "maximum External marks is = " + maxExternalMarks;
                                lblMessage.Text = "minimum Internal marks is = 0";

                                return false;
                            }
                            if (practicalMarks > maxPracticalMarks || practicalMarks < 0)
                            {
                                Response.Write("<script>alert('Wrong practical marks')</script>");
                                lblMessage.Text = "maximum Practical marks is = " + maxExternalMarks;
                                lblMessage.Text = "minimum Internal marks is = 0";

                                return false;
                            }

                            return true;
                        }
                        else
                        {
                            throw new Exception("Course not found.");
                        }
                    }
                }
            }
        }


    }
}