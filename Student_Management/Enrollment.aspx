<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Enrollment.aspx.cs" Inherits="Student_Management.Enrollment" %>
<%@ Register TagPrefix="uc" TagName="Navbar" Src="~/Navbar.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enroll Course</title>
    <link rel="stylesheet" type="text/css" href="../css/Styles.css" />

</head>
<body>
    <uc:Navbar ID="Navbar1" runat="server" />
    <div class="container">

    <form id="form1" runat="server">
        Hello ,<b> 
        <asp:Label ID="lblUsername" runat="server" Text="User"></asp:Label>
            </b> 
        <br />
        <asp:DropDownList ID="ddlCourse" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnEnroll" runat="server" Text="Enroll" OnClick="btnEnroll_Click" />
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </form>
    </div>

</body>
</html>
