<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllSellers.aspx.cs" Inherits="InventoryManagement.Pages.Seller.AllSellers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/jquery.dynatable.css" rel="stylesheet" />
    <script src="/Scripts/jquery.dynatable.js"></script>
    <script src="/Scripts/Custom.js"></script>
    <link href="/CSS/MyDynaTable.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Sellers</div>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="Add Seller" runat="server" PostBackUrl="AddSeller.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

   <%-- <div class="row">
        <div class="col-md-12">
            <asp:GridView runat="server" ID="gridSeller" AutoGenerateColumns="false" CssClass="table table-hover table-bordered text-center" PageSize="15"
                AllowPaging="true" OnPageIndexChanging="gridSeller_PageIndexChanging" OnRowCommand="gridSeller_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Seller Name">
                        <ItemTemplate>
                            <asp:Label ID="lblSellerName" Text='<%#Eval("SellerName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seller Mobile">
                        <ItemTemplate>
                            <asp:Label ID="lblSellerMobile" Text='<%#Eval("SellerMobile")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seller Landline">
                        <ItemTemplate>
                            <asp:Label ID="lblSellerLandline" Text='<%#Eval("SellerLandline")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Balance">
                        <ItemTemplate>
                            <asp:Label ID="lblBalance" Text='<%#Eval("Balance")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seller Email">
                        <ItemTemplate>
                            <asp:Label ID="lblSellerEmail" Text='<%#Eval("SellerEmail")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TIN No">
                        <ItemTemplate>
                            <asp:Label ID="lblTINNo" Text='<%#Eval("TINNo")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VAT No">
                        <ItemTemplate>
                            <asp:Label ID="lblVATNo" Text='<%#Eval("VATNo")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("SellerID")%>'></asp:LinkButton>
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
            <asp:Repeater runat="server" ID="rptrSeller" OnItemCommand="rptrSeller_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover table-bordered text-center datatable">
                        <thead>
                            <tr>
                                <th>Seller Name</th>
                                <th>Seller Mobile</th>
                                <th>Seller Landline</th>
                                <th>Seller Email</th>
                                <th>TIN No.</th>
                                <th>VAT No.</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval ("SellerName") %></td>
                        <td><%#Eval ("SellerMobile") %></td>
                        <td><%#Eval ("SellerLandline") %></td>
                        <td><%#Eval ("SellerEmail") %></td>
                         <td><%#Eval ("TINNo") %></td>
                         <td><%#Eval ("VATNo") %></td>
                        <td>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("SellerID")%>'></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                    <tr runat="server" visible="<%#((Repeater)Container.NamingContainer).Items.Count==0 %>" style="text-align: center">
                        <td colspan="8">No Record Found
                        </td>
                    </tr>
                    </tfoot>
            </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
