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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if a message query string exists
                string message = Request.QueryString["message"];
                if (!string.IsNullOrEmpty(message))
                {
                    // Display the message using a JavaScript alert
                    MessageLiteral.Text = $"<script>showMessage('{message}');</script>";
                }
            }

        }
        protected void loginbtnClicked(object sender, EventArgs e)
        {
           if(rdlistOfRoles.SelectedItem.Value=="student")
            {
                forStudentUserLogin();
            }
           else if(rdlistOfRoles.SelectedItem.Value=="professor")
            {
                forProfessorUserLogin();
            }
           else if(rdlistOfRoles.SelectedItem.Value=="admin")
            {
                forAdminUserLogin();
            }
        }
        
        protected void forProfessorUserLogin()
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both email and password.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = @"
    SELECT Name
    FROM [dbo].[Profesors] 
    WHERE Email = @Email AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    conn.Close();

                    if (result != null)
                    {
                        string professorName = result.ToString();

                        // Store professor's email and name in session variables
                        Session["ProfessorEmail"] = email;
                        Session["ProfessorName"] = professorName;
                        Session["role"] = "professor";
                        Response.Redirect("professor_index.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid email or password.";
                    }
                }
            }
        }


        protected void forAdminUserLogin()
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both email and password.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = @"
                SELECT Name
                FROM [dbo].[Admins] 
                WHERE Email = @Email AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    conn.Close();

                    if (result != null)
                    { 
                        string name = result.ToString();
                        Session["name"] = name;
                        Session["role"] = "admin";

                        Response.Redirect("~/Admin.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid email or password.";
                    }
                }
            }

        }
        protected void forStudentUserLogin()
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both email and password.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            string query = @"
            SELECT Id, Name
            FROM [dbo].[Students] 
            WHERE Email = @Email AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                int studentId = Convert.ToInt32(reader["Id"]);  // Fetching the Id
                                string studentName = reader["Name"].ToString();  // Fetching the Name
                                Session["isLoggedIn"] = true;
                                // Storing in session
                                Session["StudentId"] = studentId;   // Store the Id in the session
                                Session["StudentEmail"] = email;
                                Session["StudentName"] = studentName;
                                Session["role"] = "student";

                                // Redirect to student profile page
                                Response.Redirect("student_index.aspx");
                            }
                        }
                        else
                        {
                            // If no rows are found, display an error message
                            lblMessage.Text = "Invalid Email or Password.";
                        }
                    }
                    conn.Close();
                }
            }

        }


    }
}