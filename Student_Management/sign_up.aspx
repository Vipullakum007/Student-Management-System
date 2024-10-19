<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sign_up.aspx.cs" Inherits="Student_Management.sign_up" %>
<%@ Register TagPrefix="uc" TagName="Navbar" Src="~/Navbar.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link rel="stylesheet" type="text/css" href="~/css/Styles.css" />
</head>
<body>
    <uc:Navbar ID="Navbar1" runat="server" />
    <div class="form-container">
        <div class="auth-container">
            <h1>Sign Up</h1>
            <form id="form1" runat="server">

                <!-- Name Input -->
                <div class="input-group">
                    <label for="tbName">Name</label>
                    <asp:TextBox ID="tbName" runat="server" TextMode="SingleLine" placeholder="Enter your name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="tbName" Display="Dynamic" ErrorMessage="Name is required" ForeColor="Red" />
                </div>

                <!-- Email Input -->
                <div class="input-group">
                    <label for="tbEmail">Email</label>
                    <asp:TextBox ID="tbEmail" runat="server" TextMode="Email" placeholder="Enter your email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="Email is required" ForeColor="Red" />
                </div>

                <!-- Password Input -->
                <div class="input-group">
                    <label for="tbPassword">Password</label>
                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tbPassword" Display="Dynamic" ErrorMessage="Password is required" ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="revPassword" runat="server" 
                    ControlToValidate="tbPassword" 
                    ValidationExpression="^(?=.*[A-Z])(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$" 
                    Display="Dynamic" 
                    ErrorMessage="Password must be at least 8 characters long, contain at least one uppercase letter and one special character." 
                    ForeColor="Red" />
                </div>

                <!-- Confirm Password Input -->
                <div class="input-group">
                    <label for="tbConfirmPassword">Confirm Password</label>
                    <asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm your password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="tbConfirmPassword" Display="Dynamic" ErrorMessage="Confirm Password is required" ForeColor="Red" />
                    <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbConfirmPassword" Display="Dynamic" ErrorMessage="Passwords do not match" ForeColor="Red" />
                </div>


                <!-- Role Selection -->
                <div class="input-group">
                    <label for="rdlistOfRoles">Role</label>
                    <asp:RadioButtonList ID="rdlistOfRoles" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdlistOfRoles_SelectedIndexChanged">
                        <asp:ListItem Text="Student" Value="student" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Professor" Value="professor"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <!-- Student Fields: Branch and Semester -->
                <asp:Panel ID="studentFields" runat="server" Visible="True">
                    <div class="input-group">
                        <label for="ddlBranches">Branch</label>
                        <asp:DropDownList ID="ddlBranches" runat="server">
                            <asp:ListItem Selected="True" Text="Select Branch" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="CE" Value="ce"></asp:ListItem>
                            <asp:ListItem Text="IT" Value="it"></asp:ListItem>
                            <asp:ListItem Text="CH" Value="ch"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDDLBranch" runat="server" ControlToValidate="ddlBranches" Display="Dynamic" ErrorMessage="Branch is required" ForeColor="Red" />
                    </div>

                    <div class="input-group">
                        <label for="tbSemester">Semester</label>
                        <asp:TextBox ID="tbSemester" runat="server" TextMode="Number" placeholder="Enter your semester"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="tbSemester" Display="Dynamic" ErrorMessage="Semester is required" ForeColor="Red" />
                    </div>
                </asp:Panel>

                <!-- Professor Fields: Join Date -->
                <asp:Panel ID="professorFields" runat="server" Visible="False">
                    <div class="input-group">
                        <label for="tbJoinDate">Join Date</label>
                        <asp:TextBox ID="tbJoinDate" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoinDate" runat="server" ControlToValidate="tbJoinDate" Display="Dynamic" ErrorMessage="Join Date is required" ForeColor="Red" />
                    </div>
                </asp:Panel>

                <!-- Date of Birth -->
                <div class="input-group">
                    <label for="tbDob">Date of Birth</label>
                    <asp:TextBox ID="tbDob" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDob" runat="server" ControlToValidate="tbDob" Display="Dynamic" ErrorMessage="DOB is required" ForeColor="Red" />
                </div>

                <!-- Sign Up Button -->
                <asp:Button ID="btn" runat="server" CssClass="btn" Text="Sign Up" OnClick="signUpBtnClicked" />

                <!-- Login Prompt -->
                <div class="signup-link">
                    Already have an account? 
                    <asp:HyperLink runat="server" NavigateUrl="~/home.aspx">Click here</asp:HyperLink> to log in.
                </div>

                <!-- Error or success message display -->
                <asp:Label ID="lblMessage" runat="server" CssClass="error-message" ForeColor="Red"></asp:Label>

                <!-- Validation Summary -->
                <asp:ValidationSummary ID="vsSignup" runat="server" ForeColor="Red" HeaderText="Please check the following: " />

            </form>
        </div>
    </div>

</body>
</html>
