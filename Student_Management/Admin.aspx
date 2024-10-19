<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Student_Management.Admin" %>
<%@ Register TagPrefix="uc" TagName="Navbar" Src="~/Navbar.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Courses</title>
    <link rel="stylesheet" type="text/css" href="../css/Styles.css" />

</head>
<body>
    <uc:Navbar ID="Navbar1" runat="server" />
    <div class="container">

    <form id="form1" runat="server">
        <div>
            <h3>Welcom to admin Side. </h3>

            </div>

            <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" AllowSorting="True" OnRowDeleting="gvCourses_RowDeleting"  >
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Code " HeaderText="Code " SortExpression="Code " />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="Credit" HeaderText="Credit" SortExpression="Credit" />
                    <asp:BoundField DataField="Internal_marks" HeaderText="Internal_marks" SortExpression="Internal_marks" />
                    <asp:BoundField DataField="External_marks" HeaderText="External_marks" SortExpression="External_marks" />
                    <asp:BoundField DataField="Practical_marks" HeaderText="Practical_marks" SortExpression="Practical_marks" />
                    <asp:BoundField DataField="Passing_marks" HeaderText="Passing_marks" SortExpression="Passing_marks" />
                    <asp:BoundField DataField="semseter" HeaderText="semseter" SortExpression="semseter" />
                    <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch" />
                    <asp:BoundField DataField="Taught_by" HeaderText="Taught_by" SortExpression="Taught_by" />
                </Columns>
            </asp:GridView>

            <br />
            <br />
            <asp:SqlDataSource ID="SqlDataSourceCourse" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [Courses] WHERE [Id] = @original_Id AND [Name] = @original_Name AND [Code ] = @original_Code_ AND [Description] = @original_Description AND [Credit] = @original_Credit AND [Internal_marks] = @original_Internal_marks AND [External_marks] = @original_External_marks AND [Practical_marks] = @original_Practical_marks AND [Passing_marks] = @original_Passing_marks AND [semseter] = @original_semseter AND [Branch] = @original_Branch AND [Taught_by] = @original_Taught_by" InsertCommand="INSERT INTO [Courses] ([Name], [Code ], [Description], [Credit], [Internal_marks], [External_marks], [Practical_marks], [Passing_marks], [semseter], [Branch], [Taught_by]) VALUES (@Name, @Code_, @Description, @Credit, @Internal_marks, @External_marks, @Practical_marks, @Passing_marks, @semseter, @Branch, @Taught_by)" SelectCommand="SELECT * FROM [Courses]" UpdateCommand="UPDATE [Courses] SET [Name] = @Name, [Code ] = @Code_, [Description] = @Description, [Credit] = @Credit, [Internal_marks] = @Internal_marks, [External_marks] = @External_marks, [Practical_marks] = @Practical_marks, [Passing_marks] = @Passing_marks, [semseter] = @semseter, [Branch] = @Branch, [Taught_by] = @Taught_by WHERE [Id] = @original_Id AND [Name] = @original_Name AND [Code ] = @original_Code_ AND [Description] = @original_Description AND [Credit] = @original_Credit AND [Internal_marks] = @original_Internal_marks AND [External_marks] = @original_External_marks AND [Practical_marks] = @original_Practical_marks AND [Passing_marks] = @original_Passing_marks AND [semseter] = @original_semseter AND [Branch] = @original_Branch AND [Taught_by] = @original_Taught_by" ConflictDetection="CompareAllValues" OldValuesParameterFormatString="original_{0}">
                <DeleteParameters>
                    <asp:Parameter Name="original_Id" Type="Int32" />
                    <asp:Parameter Name="original_Name" Type="String" />
                    <asp:Parameter Name="original_Code_" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Credit" Type="Double" />
                    <asp:Parameter Name="original_Internal_marks" Type="Int32" />
                    <asp:Parameter Name="original_External_marks" Type="Int32" />
                    <asp:Parameter Name="original_Practical_marks" Type="Int32" />
                    <asp:Parameter Name="original_Passing_marks" Type="Int32" />
                    <asp:Parameter Name="original_semseter" Type="Int32" />
                    <asp:Parameter Name="original_Branch" Type="Int32" />
                    <asp:Parameter Name="original_Taught_by" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Code_" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Credit" Type="Double" />
                    <asp:Parameter Name="Internal_marks" Type="Int32" />
                    <asp:Parameter Name="External_marks" Type="Int32" />
                    <asp:Parameter Name="Practical_marks" Type="Int32" />
                    <asp:Parameter Name="Passing_marks" Type="Int32" />
                    <asp:Parameter Name="semseter" Type="Int32" />
                    <asp:Parameter Name="Branch" Type="Int32" />
                    <asp:Parameter Name="Taught_by" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Code_" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Credit" Type="Double" />
                    <asp:Parameter Name="Internal_marks" Type="Int32" />
                    <asp:Parameter Name="External_marks" Type="Int32" />
                    <asp:Parameter Name="Practical_marks" Type="Int32" />
                    <asp:Parameter Name="Passing_marks" Type="Int32" />
                    <asp:Parameter Name="semseter" Type="Int32" />
                    <asp:Parameter Name="Branch" Type="Int32" />
                    <asp:Parameter Name="Taught_by" Type="Int32" />
                    <asp:Parameter Name="original_Id" Type="Int32" />
                    <asp:Parameter Name="original_Name" Type="String" />
                    <asp:Parameter Name="original_Code_" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Credit" Type="Double" />
                    <asp:Parameter Name="original_Internal_marks" Type="Int32" />
                    <asp:Parameter Name="original_External_marks" Type="Int32" />
                    <asp:Parameter Name="original_Practical_marks" Type="Int32" />
                    <asp:Parameter Name="original_Passing_marks" Type="Int32" />
                    <asp:Parameter Name="original_semseter" Type="Int32" />
                    <asp:Parameter Name="original_Branch" Type="Int32" />
                    <asp:Parameter Name="original_Taught_by" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceBranch" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [BranchID], [bName] FROM [Branch]">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceProf" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Prof_ID], [Name] FROM [Profesors]">
            </asp:SqlDataSource>
            <br />
            <br />
    </form>
    </div>

</body>
</html>
