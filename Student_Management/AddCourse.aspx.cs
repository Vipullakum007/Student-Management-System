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
    public partial class AddCourse : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Session["role"] == null || Session["role"].ToString() != "admin")
            {
                Response.Redirect("~/home.aspx?message=Please+log+in+as+Admin+to+see+this+page");
            }

            if (!IsPostBack)
            {
                // Populate dropdowns (ddlBranch, ddlProfessor1) if needed
                // Example: LoadBranches();
                // Example: LoadProfessors();
            }
        }

        protected void btnSubmit_Clicked(object sender, EventArgs e)
        {
            // Get the values from the input fields
            string name = tbTitle.Text;
            string code = tbCode.Text;

            // Define the SQL connection and query
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

            // Check if the course already exists
            string checkQuery = @"
    SELECT COUNT(*) FROM [dbo].[Courses] 
    WHERE [Name] = @Name OR [Code] = @Code";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    // Add parameters to the check command
                    checkCmd.Parameters.AddWithValue("@Name", name);
                    checkCmd.Parameters.AddWithValue("@Code", code);

                    // Open the connection and execute the check query
                    conn.Open();
                    int courseExists = (int)checkCmd.ExecuteScalar(); // Get the count of courses with the same name or code

                    if (courseExists > 0)
                    {
                        // Course already exists
                        Response.Write("<script>alert('Course already exists')</script>");
                        lblMessage.Text = "Course already exists.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        conn.Close();
                        return; // Stop further execution
                    }

                    // If the course doesn't exist, proceed with the insert operation
                    string insertQuery = @"
            INSERT INTO [dbo].[Courses] 
            ([Name], [Code], [Description], [Credit], [Internal_marks], [External_marks], [Practical_marks], [Passing_marks], [semseter], [Branch], [Taught_by]) 
            VALUES 
            (@Name, @Code, @Description, @Credit, @Internal_marks, @External_marks, @Practical_marks, @Passing_marks, @Semester, @Branch, @Taught_by)";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        // Add parameters to the insert command
                        insertCmd.Parameters.AddWithValue("@Name", name);
                        insertCmd.Parameters.AddWithValue("@Code", code);
                        insertCmd.Parameters.AddWithValue("@Description", tbDescription.Text);
                        insertCmd.Parameters.AddWithValue("@Credit", int.Parse(tbCredit.Text));
                        insertCmd.Parameters.AddWithValue("@Internal_marks", int.Parse(tbInternalMarks.Text));
                        insertCmd.Parameters.AddWithValue("@External_marks", int.Parse(tbExternalMarks.Text));
                        insertCmd.Parameters.AddWithValue("@Practical_marks", int.Parse(tbPracMarks.Text));
                        insertCmd.Parameters.AddWithValue("@Passing_marks", int.Parse(tbPassingMarks.Text));
                        insertCmd.Parameters.AddWithValue("@Semester", int.Parse(tbSem.Text));
                        insertCmd.Parameters.AddWithValue("@Branch", int.Parse(ddlBranch.SelectedValue));
                        insertCmd.Parameters.AddWithValue("@Taught_by", int.Parse(ddlProfessor1.SelectedValue));

                        // Execute the insert command
                        insertCmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }

            // Optionally, clear the input fields after submission
            tbTitle.Text = "";
            tbDescription.Text = "";
            tbCode.Text = "";
            tbCredit.Text = "";
            tbInternalMarks.Text = "";
            tbExternalMarks.Text = "";
            tbPracMarks.Text = "";
            tbPassingMarks.Text = "";
            tbSem.Text = "";
            ddlBranch.SelectedIndex = 0;
            ddlProfessor1.SelectedIndex = 0;

            // Display a success message
            Response.Write("<script>alert('Course added successfully')</script>");
            lblMessage.Text = "Course added successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnCancel_Clicked(object sender, EventArgs e)
        {
            // Redirect back to the admin page or wherever appropriate
            Response.Redirect("Admin.aspx");
        }
    }
}
