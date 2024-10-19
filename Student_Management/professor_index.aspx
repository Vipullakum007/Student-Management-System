<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="professor_index.aspx.cs" Inherits="Student_Management.professor_index" %>
<%@ Register TagPrefix="uc" TagName="Navbar" Src="~/Navbar.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Professor | Index</title>
    <link rel="stylesheet" type="text/css" href="../css/Styles.css" />

</head>
<body>
    <uc:Navbar ID="Navbar1" runat="server" />
    <div class="container">

    <form id="form1" runat="server">
            <h2>
                Professor Index Page
            </h2>
        <div>
            
            Hello ,
            <b><asp:Label ID="lblProfName" runat="server" ForeColor="#3399FF"></asp:Label></b>
            <br />
            <br />
            <asp:DropDownList ID="ddlCourse" runat="server" Height="34px" Width="205px" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
            <br />
            <asp:SqlDataSource ID="SqlDataSourceForGradeTable" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" OldValuesParameterFormatString="original_{0}" 
                SelectCommand="SELECT [GradeID], [StudentID], [InternalMarks], [ExternalMarks], [PracticalMarks], [Grade] FROM [Grades] WHERE ([CourseID] = @CourseID)" DeleteCommand="DELETE FROM [Grades] WHERE [GradeID] = @original_GradeID" InsertCommand="INSERT INTO [Grades] ([StudentID], [InternalMarks], [ExternalMarks], [PracticalMarks], [Grade]) VALUES (@StudentID, @InternalMarks, @ExternalMarks, @PracticalMarks, @Grade)" UpdateCommand="UPDATE [Grades] SET [StudentID] = @StudentID, [InternalMarks] = @InternalMarks, [ExternalMarks] = @ExternalMarks, [PracticalMarks] = @PracticalMarks, [Grade] = @Grade WHERE [GradeID] = @original_GradeID"
                >
                <DeleteParameters>
                    <asp:Parameter Name="original_GradeID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="StudentID" Type="Int32" />
                    <asp:Parameter Name="InternalMarks" Type="Int32" />
                    <asp:Parameter Name="ExternalMarks" Type="Int32" />
                    <asp:Parameter Name="PracticalMarks" Type="Int32" />
                    <asp:Parameter Name="Grade" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlCourse" Name="CourseID" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="StudentID" Type="Int32" />
                    <asp:Parameter Name="InternalMarks" Type="Int32" />
                    <asp:Parameter Name="ExternalMarks" Type="Int32" />
                    <asp:Parameter Name="PracticalMarks" Type="Int32" />
                    <asp:Parameter Name="Grade" Type="String" />
                    <asp:Parameter Name="original_GradeID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <br />
            
            <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
              CellPadding="4" DataKeyNames="GradeID" ForeColor="#333333" GridLines="None" 
              OnRowUpdating="GridView2_RowUpdating" DataSourceID="SqlDataSourceForGradeTable" 
              OnDataBound="GridView2_DataBound">
            <AlternatingRowStyle BackColor="White" />
            <EmptyDataTemplate>
                <p>No students are enrolled for this course yet.</p>
            </EmptyDataTemplate>
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="GradeID" HeaderText="GradeID" InsertVisible="False" ReadOnly="True" SortExpression="GradeID" />
                    <asp:BoundField DataField="StudentID" HeaderText="StudentID" SortExpression="StudentID" />
                    <asp:BoundField DataField="InternalMarks" HeaderText="InternalMarks" SortExpression="InternalMarks" />
                    <asp:BoundField DataField="ExternalMarks" HeaderText="ExternalMarks" SortExpression="ExternalMarks" />
                    <asp:BoundField DataField="PracticalMarks" HeaderText="PracticalMarks" SortExpression="PracticalMarks" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" />
                </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>


            <br />

            <br />
            <br />
            <br />
            <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
            
        </div>
    </form>
    </div>

</body>
</html>
