<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllUsers.aspx.cs" Inherits="InventoryManagement.Pages.Users.AllUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/jquery.dynatable.css" rel="stylesheet" />
    <script src="/Scripts/jquery.dynatable.js"></script>
    <script src="/Scripts/Custom.js"></script>
    <link href="/CSS/MyDynaTable.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Users</div>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="Add User" runat="server" PostBackUrl="AddUser.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

    <%--<div class="row">
        <div class="col-md-12">
            <asp:GridView runat="server" ID="gridUser" AutoGenerateColumns="false" CssClass="table table-hover table-bordered text-center" PageSize="15"
                 AllowPaging="true" OnPageIndexChanging="gridUser_PageIndexChanging" OnRowCommand="gridUser_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeName" Text='<%#Eval("EmployeeName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" Text='<%#Eval("UserName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:Label ID="lblMobile" Text='<%#Eval("Mobile")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" Text='<%#Eval("Email")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate>
                            <asp:Label ID="lblIsActive" Text='<%#Eval("IsActive")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("UserID")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No Record
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        </div>--%>

    <div class="row">
        <div class="col-md-12">
            <asp:Repeater runat="server" ID="rptrUser" OnItemCommand="rptrUser_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover table-bordered text-center datatable">
                        <thead>
                            <tr>
                                <th>Employee Name</th>
                                <th>User Name</th>
                                <th>Mobile</th>
                                <th>Email</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval ("EmployeeName") %></td>
                        <td><%#Eval ("UserName") %></td>
                        <td><%#Eval ("Mobile") %></td>
                        <td><%#Eval ("Email") %></td>
                        <td>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("UserID")%>'></asp:LinkButton>
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
