<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Student_Management.WebForm1" %>
<%@ Register TagPrefix="uc" TagName="Navbar" Src="~/Navbar.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" type="text/css" href="~/css/Styles.css" />

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
            <h1>Login</h1>
            <form id="form1" runat="server">

                <asp:Literal ID="MessageLiteral" runat="server"></asp:Literal>

                <!-- Email Input -->
                <div class="input-group">
                    <label for="tbEmail">Email</label>
                    <asp:TextBox runat="server" ID="tbEmail" TextMode="SingleLine" placeholder="Enter your email"></asp:TextBox>
                </div>

                <!-- Password Input -->
                <div class="input-group">
                    <label for="tbPassword">Password</label>
                    <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                </div>

                <!-- Role Selection (Radio Buttons) -->
                <div class="input-group">
                    <label for="rdlistOfRoles">Role</label>
                    <div class="radio-group">
                        <asp:RadioButtonList runat="server" ID="rdlistOfRoles" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Student" Value="student" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Professor" Value="professor"></asp:ListItem>
                            <asp:ListItem Text="Admin" Value="admin"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <!-- Login Button -->
                <asp:Button id="btn" runat="server" CssClass="btn" OnClick="loginbtnClicked" Text="Login" />

                <!-- Signup Prompt -->
                <div class="signup-link">
                    Don't have an account? 
                    <asp:HyperLink runat="server" NavigateUrl="~/sign_up.aspx">Click here</asp:HyperLink> to create one.
                </div>

                <!-- Error or success message display -->
                <asp:Label ID="lblMessage" runat="server" CssClass="error-message" ForeColor="Red"></asp:Label>

            </form>
        </div>
    </div>

</body>
</html>
