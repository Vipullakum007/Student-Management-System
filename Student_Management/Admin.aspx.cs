using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Student_Management
{
    public partial class Admin : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();// for courses
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || Session["role"].ToString() != "admin")
            {
                Response.Redirect("~/home.aspx?message=Please+log+in+as+Admin+to+see+this+page");
            }
            if (!IsPostBack)
            {
                fillCourseList(); // Show courses on initial page load
            }
        }
        protected void fillCourseList()
        {
            // Define the SQL connection and query to retrieve all courses
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = "SELECT * FROM [dbo].[Courses]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    // Fill the DataSet with the retrieved data
                    ds1.Clear();
                    adapter.Fill(ds1, "Courses");
                }
            }
            gvCourses.DataSource = ds1.Tables["Courses"];
            gvCourses.DataBind();
        }

        protected void gvCourses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCourses.EditIndex = e.NewEditIndex;
            fillCourseList();
        }

        protected void gvCourses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvCourses.Rows[e.RowIndex];
            string name = (row.Cells[1].Controls[0] as TextBox).Text;
            string code = (row.Cells[2].Controls[0] as TextBox).Text;
            string description = (row.Cells[3].Controls[0] as TextBox).Text;

            int courseId = Convert.ToInt32(gvCourses.DataKeys[e.RowIndex].Value);

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = @"
            UPDATE [dbo].[Courses]
            SET [Name] = @Name, [Code] = @Code, [Description] = @Description
            WHERE [Id] = @CourseID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            gvCourses.EditIndex = -1;
            fillCourseList();
        }

        protected void gvCourses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCourses.EditIndex = -1;
            fillCourseList();
        }
        protected void gvCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Response.Write("<script>alert('Are you sure to delete ?')</script>");
            int courseId = Convert.ToInt32(gvCourses.DataKeys[e.RowIndex].Value);

            // Define the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Delete from related tables first
                string deleteEnrollmentsQuery = "DELETE FROM [dbo].[Enrollments] WHERE [CourseID] = @CourseID";
                string deleteGradesQuery = "DELETE FROM [dbo].[Grades] WHERE [CourseID] = @CourseID";

                using (SqlCommand cmd = new SqlCommand(deleteEnrollmentsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand(deleteGradesQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.ExecuteNonQuery();
                }

                // Now delete the course
                string deleteCourseQuery = "DELETE FROM [dbo].[Courses] WHERE [Id] = @CourseID";
                using (SqlCommand cmd = new SqlCommand(deleteCourseQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            fillCourseList(); // Refresh the course list
        }
    }
}