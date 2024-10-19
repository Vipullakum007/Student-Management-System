using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Configuration;

namespace Student_Management
{
    public partial class sign_up : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected bool EmailExists(SqlConnection conn, string email)
        {
            //Response.Write("checking email " + email);
            string command = "SELECT COUNT(*) FROM [dbo].[Students] WHERE Email = @Email ";
            using (SqlCommand cmd = new SqlCommand(command, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                //Response.Write(count);
                conn.Close();
                return count > 0; // returns true if email exists
            }
            command = "SELECT COUNT(*) FROM [dbo].[Profesors] WHERE Email = @Email";
            using (SqlCommand cmd = new SqlCommand(command, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                Response.Write(count);
                conn.Close();
                return count > 0; // returns true if email exists
            }
            
        }

        protected int generateClassId(SqlConnection conn)
        {
            string command = @"
            SELECT Class_ID 
            FROM [dbo].[Classroom] 
            WHERE Semester = @Semester AND Branch = @Branch";
            using (SqlCommand cmd = new SqlCommand(command, conn))
            {
                cmd.Parameters.AddWithValue("@Semester", Int32.Parse(tbSemester.Text));
                cmd.Parameters.AddWithValue("@Branch", ddlBranches.SelectedItem.Value);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // Ensure there's at least one row returned
                if (reader.Read())
                {
                    int id1 = reader.GetInt32(0);
                    if (reader.Read())
                    {
                        int id2 = reader.GetInt32(0);
                        conn.Close();
                        return tbName.Text[0] <= 107 || tbName.Text[0] <= 75 ? id1 : id2;
                    }
                }
                conn.Close();
            }
            return -1; // Or throw an exception if no class ID found
        }

        protected void addStudentToDb(SqlConnection conn)
        {
            if (EmailExists(conn, tbEmail.Text))
            {
                Response.Write("<script>alert('User already exists ! Try Login ')</script>");
                lblMessage.Text = "User already exists with that email address ! Try Login . ";
                return;
            }

            try
            {
                using (conn)
                {
                    string command = @"
                    INSERT INTO [dbo].[Students] 
                    (Name, Dob, Branch, CurrentCPI, Semester, Class_ID, Email, Password) 
                    VALUES (@Name, @Dob, @Branch, @CurrentCPI, @Semester, @Class_ID, @Email, @Password)";

                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", tbName.Text);
                        cmd.Parameters.AddWithValue("@Dob", SqlDateTime.Parse(tbDob.Text));
                        cmd.Parameters.AddWithValue("@Branch", ddlBranches.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@CurrentCPI", 0.0);
                        cmd.Parameters.AddWithValue("@Semester", Int32.Parse(tbSemester.Text));
                        cmd.Parameters.AddWithValue("@Class_ID", generateClassId(conn));
                        cmd.Parameters.AddWithValue("@Email", tbEmail.Text);
                        cmd.Parameters.AddWithValue("@Password", tbPassword.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    Response.Redirect("~/home.aspx");
                }
            }
            catch (Exception exc)
            {
                lblMessage.Text = exc.Message;
            }
        }

        protected void addProfessorToDb(SqlConnection conn)
        {
            if (EmailExists(conn, tbEmail.Text))
            {
                lblMessage.Text = "User already exists with that email address.";
                return;
            }

            string command = @"
            INSERT INTO [dbo].[Profesors] 
            (Name, Email, Password, Join_Date) 
            VALUES (@Name, @Email, @Password, @Join_Date)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", tbName.Text);
                    cmd.Parameters.AddWithValue("@Email", tbEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", tbPassword.Text);
                    cmd.Parameters.AddWithValue("@Join_Date", SqlDateTime.Parse(tbJoinDate.Text));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                Response.Redirect("~/home.aspx");
            }
            catch (Exception exc)
            {
                lblMessage.Text = exc.Message;
            }
        }

        protected void signUpBtnClicked(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

            if (rdlistOfRoles.SelectedItem.Value == "student")
            {
                addStudentToDb(conn);
            }
            else if (rdlistOfRoles.SelectedItem.Value == "professor")
            {
                addProfessorToDb(conn);
            }
        }

        protected void rdlistOfRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRole = rdlistOfRoles.SelectedItem.Value;
            studentFields.Visible = selectedRole == "student";
            professorFields.Visible = selectedRole == "professor";
        }

        protected void LoginBtnClicked(object sender, EventArgs e)
        {
            Response.Redirect("~/home.aspx");
        }
    }
}
