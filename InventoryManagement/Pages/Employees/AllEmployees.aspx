<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllEmployees.aspx.cs" Inherits="InventoryManagement.Pages.Employees.AllEmployees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/jquery.dynatable.css" rel="stylesheet" />
    <script src="/Scripts/jquery.dynatable.js"></script>
    <script src="/Scripts/Custom.js"></script>
    <link href="/CSS/MyDynaTable.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Employees</div>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="Add Employee" runat="server" PostBackUrl="AddEmployee.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

     <div class="row">
        <div class="col-md-12">
            <asp:Repeater runat="server" ID="rptrEmployee" OnItemCommand="rptrEmployee_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover table-bordered text-center datatable">
                        <thead>
                            <tr>
                                <th>Employee Name</th>
                                <th>Employee Mobile</th>
                                <th>Employee Email</th>
                                <th>JoiningDate</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval ("EmployeeName") %></td>
                        <td><%#Eval ("Mobile") %></td>
                        <td><%#Eval ("Email") %></td>
                        <td><%#Eval ("JoiningDate") %></td>
                        <td>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("EmployeeID")%>'></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr runat="server" visible="<%#((Repeater)Container.NamingContainer).Items.Count==0 %>" style="text-align: center">
                        <td colspan="6">No Record Found
                        </td>
                    </tr>
                    </tfoot>
            </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
