<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student_index.aspx.cs" Inherits="Student_Management.student_index" %>
<%@ Register TagPrefix="uc" TagName="Navbar" Src="~/Navbar.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile</title>
    <link rel="stylesheet" type="text/css" href="../css/Styles.css" />
</head>
<body>
    <uc:Navbar ID="Navbar1" runat="server" />
    <div class="container">

    <form id="form1" runat="server">
    <div>

        Hello ,
        <b><asp:Label ID="lblName" runat="server" Text="User"></asp:Label></b>

    </div>
        <div>
            <asp:GridView ID="gvStudentDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" style="margin-top: 58px">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Dob" HeaderText="Date of Birth" />
                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                    <asp:BoundField DataField="CurrentCPI" Visible="false" HeaderText="Current CPI" />
                    <asp:BoundField DataField="Semester" HeaderText="Semester" />
                    <asp:BoundField DataField="Class_ID" HeaderText="Class ID" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div>
            <h3>Enrolled Courses</h3>
            <asp:GridView ID="gvEnrolledCourses" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Course ID" />
                    <asp:BoundField DataField="Name" HeaderText="Course Name" />
                    <asp:BoundField DataField="InternalMarks" HeaderText="Internal Marks" />
                    <asp:BoundField DataField="ExternalMarks" HeaderText="External Marks" />
                    <asp:BoundField DataField="PracticalMarks" HeaderText="Practical Marks" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                </Columns>
            </asp:GridView>

            <br />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>

        </div>

    </form>
    </div>

</body>
</html>

