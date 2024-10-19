<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="Student_Management.WebUserControl1" %>

<nav>
    <ul class="navbar">
        <li id="loginLink" runat="server"><a href="Home.aspx">Login</a></li>
        <li id="studentLink" runat="server"><a href="student_index.aspx">Profile</a></li>
        <li id="professorLink" runat="server"><a href="professor_index.aspx">Professor</a></li>
        
        <!-- Admin Links -->
        <li id="adminLink" runat="server"><a href="admin.aspx">Manage Courses</a></li>
        <li id="addCourseLink" runat="server" visible="false"><a href="AddCourse.aspx">Add Course</a></li>
        
        <li id="enrollmentLink" runat="server"><a href="Enrollment.aspx">Enroll Course</a></li>
        <li id="logoutLink" runat="server"><a href="Logout.aspx" onclick="logout()">Logout</a></li>
    </ul>
</nav>
