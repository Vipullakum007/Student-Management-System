using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Management
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the role from session
            string role = Session["role"] as string;

            // Default: Hide all role-specific links
            studentLink.Visible = false;
            professorLink.Visible = false;
            adminLink.Visible = false;
            enrollmentLink.Visible = false;
            logoutLink.Visible = false;
            addCourseLink.Visible = false;

            // If the user is logged in, show the correct links based on role
            if (!string.IsNullOrEmpty(role))
            {
                logoutLink.Visible = true;
                loginLink.Visible = false;
                if (role == "student")
                {
                    studentLink.Visible = true;
                    enrollmentLink.Visible = true;
                }
                else if (role == "professor")
                {
                    professorLink.Visible = true;
                }
                else if (role == "admin")
                {
                    adminLink.Visible = true;
                    addCourseLink.Visible = true;
                }
            }
        }
    }
    
}