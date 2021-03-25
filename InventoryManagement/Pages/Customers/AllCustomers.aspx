<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllCustomers.aspx.cs" Inherits="InventoryManagement.Pages.Customers.AllCustomers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/jquery.dynatable.css" rel="stylesheet" />
    <script src="/Scripts/jquery.dynatable.js"></script>
    <script src="/Scripts/Custom.js"></script>
    <link href="/CSS/MyDynaTable.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Customers</div>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="Add Customer" runat="server" PostBackUrl="AddCustomer.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

     <div class="row">
        <div class="col-md-12">
            <asp:Repeater runat="server" ID="rptrCustomer" OnItemCommand="rptrCustomer_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover table-bordered text-center datatable">
                        <thead>
                            <tr>
                                <th>Customer Name</th>
                                <th>Customer Mobile</th>
                                <th>Customer Email</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval ("CustomerName") %></td>
                        <td><%#Eval ("CustomerMobile") %></td>
                        <td><%#Eval ("CustomerEmail") %></td>
                        <td>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("CustomerID")%>'></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr runat="server" visible="<%#((Repeater)Container.NamingContainer).Items.Count==0 %>" style="text-align: center">
                        <td colspan="5">No Record Found
                        </td>
                    </tr>
                    </tfoot>
            </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>

