using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Management
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear all session data
            Session["isLoggedIn"] = false;
            Session["role"] = "";
            Session.Clear();
            Session.Abandon();
            Response.Write("<script>alert('Logout successfully')</script>");
            Response.Redirect("home.aspx");
        }
    }
}