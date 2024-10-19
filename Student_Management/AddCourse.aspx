<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="Student_Management.AddCourse" %>
<%@ Register TagPrefix="uc" TagName="Navbar" Src="~/Navbar.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Course</title>
    <link rel="stylesheet" type="text/css" href="~/css/Styles.css" />
    <script src="~/Scripts/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script>
        function showMessage(message) {
            alert(message);
        }
    </script>
</head>
<body>
    <uc:Navbar ID="Navbar1" runat="server" />
    <div class="form-container">
        <div class="auth-container">
            <h1>Add Course</h1>
            <form id="form1" runat="server">
                <asp:Literal ID="MessageLiteral" runat="server"></asp:Literal>

                <!-- Course Title Input -->
                <div class="input-group">
                    <label for="tbTitle">Course Title</label>
                    <asp:TextBox runat="server" ID="tbTitle" TextMode="SingleLine" placeholder="Enter course title"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvTitle" ControlToValidate="tbTitle" ErrorMessage="Course Title is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Course Description Input -->
                <div class="input-group">
                    <label for="tbDescription">Description</label>
                    <asp:TextBox runat="server" ID="tbDescription" TextMode="MultiLine" Rows="4" placeholder="Enter course description"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvDescription" ControlToValidate="tbDescription" ErrorMessage="Description is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Course Code Input -->
                <div class="input-group">
                    <label for="tbCode">Course Code</label>
                    <asp:TextBox runat="server" ID="tbCode" TextMode="SingleLine" placeholder="Enter course code"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvCode" ControlToValidate="tbCode" ErrorMessage="Course Code is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Credit Input -->
                <div class="input-group">
                    <label for="tbCredit">Credits</label>
                    <asp:TextBox runat="server" ID="tbCredit" TextMode="SingleLine" placeholder="Enter number of credits"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvCredit" ControlToValidate="tbCredit" ErrorMessage="Credits are required." ForeColor="Red" Display="Dynamic" />
                    <asp:RangeValidator runat="server" ID="rvCredit" ControlToValidate="tbCredit" MinimumValue="1" MaximumValue="100" Type="Integer" ErrorMessage="Credits must be at least 1." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Internal Marks Input -->
                <div class="input-group">
                    <label for="tbInternalMarks">Internal Marks</label>
                    <asp:TextBox runat="server" ID="tbInternalMarks" TextMode="SingleLine" placeholder="Enter internal marks"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvInternalMarks" ControlToValidate="tbInternalMarks" ErrorMessage="Internal Marks are required." ForeColor="Red" Display="Dynamic" />
                    <asp:RangeValidator runat="server" ID="rvInternalMarks" ControlToValidate="tbInternalMarks" MinimumValue="1" MaximumValue="100" Type="Integer" ErrorMessage="Internal Marks must be at least 1." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- External Marks Input -->
                <div class="input-group">
                    <label for="tbExternalMarks">External Marks</label>
                    <asp:TextBox runat="server" ID="tbExternalMarks" TextMode="SingleLine" placeholder="Enter external marks"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvExternalMarks" ControlToValidate="tbExternalMarks" ErrorMessage="External Marks are required." ForeColor="Red" Display="Dynamic" />
                    <asp:RangeValidator runat="server" ID="rvExternalMarks" ControlToValidate="tbExternalMarks" MinimumValue="1" MaximumValue="100" Type="Integer" ErrorMessage="External Marks must be at least 1." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Practical Marks Input -->
                <div class="input-group">
                    <label for="tbPracMarks">Practical Marks</label>
                    <asp:TextBox runat="server" ID="tbPracMarks" TextMode="SingleLine" placeholder="Enter practical marks"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvPracMarks" ControlToValidate="tbPracMarks" ErrorMessage="Practical Marks are required." ForeColor="Red" Display="Dynamic" />
                    <asp:RangeValidator runat="server" ID="rvPracMarks" ControlToValidate="tbPracMarks" MinimumValue="1" MaximumValue="100" Type="Integer" ErrorMessage="Practical Marks must be at least 1." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Passing Marks Input -->
                <div class="input-group">
                    <label for="tbPassingMarks">Passing Marks</label>
                    <asp:TextBox runat="server" ID="tbPassingMarks" TextMode="SingleLine" placeholder="Enter passing marks"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvPassingMarks" ControlToValidate="tbPassingMarks" ErrorMessage="Passing Marks are required." ForeColor="Red" Display="Dynamic" />
                    <asp:RangeValidator runat="server" ID="rvPassingMarks" ControlToValidate="tbPassingMarks" MinimumValue="1" MaximumValue="100" Type="Integer" ErrorMessage="Passing Marks must be at least 1." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Semester Input -->
                <div class="input-group">
                    <label for="tbSem">Semester</label>
                    <asp:TextBox runat="server" ID="tbSem" TextMode="SingleLine" placeholder="Enter semester"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvSem" ControlToValidate="tbSem" ErrorMessage="Semester is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Branch Dropdown -->
                <div class="input-group">
                    <label for="ddlBranch">Branch</label>
                    <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="SqlDataSourceBranch" DataTextField="bName" DataValueField="BranchID"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvBranch" ControlToValidate="ddlBranch" InitialValue="" ErrorMessage="Branch is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Professor Dropdown -->
                <div class="input-group">
                    <label for="ddlProfessor1">Professor</label>
                    <asp:DropDownList ID="ddlProfessor1" runat="server" DataSourceID="SqlDataSourceProf" DataTextField="Name" DataValueField="Prof_ID"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvProfessor" ControlToValidate="ddlProfessor1" InitialValue="" ErrorMessage="Professor is required." ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Submit Button -->
                <asp:Button id="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Clicked" Text="Add Course" />

                <!-- Success or Error Message -->
                <asp:Label ID="lblMessage" runat="server" CssClass="error-message" ForeColor="Red"></asp:Label>

                <asp:SqlDataSource ID="SqlDataSourceBranch" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [BranchID], [bName] FROM [Branch]">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSourceProf" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Prof_ID], [Name] FROM [Profesors]">
                </asp:SqlDataSource>
            </form>
        </div>
    </div>
</body>
</html>
