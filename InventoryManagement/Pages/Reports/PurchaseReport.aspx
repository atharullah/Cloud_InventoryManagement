<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseReport.aspx.cs" Inherits="InventoryManagement.Pages.Reports.PurchaseReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="text-center">Purchase Report</div>
        
    </div>
    <h4 class="text-center">
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </h4>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="From Month"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtFromMonth" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="To Month"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtToMonth" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>&nbsp;</label>
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnGenerateReport" Text="Generate Report" OnClick="btnGenerateReport_Click" />
            </div>
        </div>
    </div>
   
    <div class="row">
        <div class="col-md-12">
            <asp:Repeater runat="server" ID="rptrInventoryReport">
                <HeaderTemplate>
                    <table class="table text-center table-bordered table-responsive table-hover">
                        <tr>
                            <th>Item Name</th>
                            <th>Quantity</th>
                            <th>Total Amount</th>
                            <th>VAT(%)</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblInvName" Text='<%#Eval("ItemName")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="Label2" Text='<%#Eval("Quantity")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="Label4" Text='<%#Eval("TotalAmount")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblUsedCount" Text='<%#Eval("GSTVAT")%>'></asp:Label></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr runat="server" visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' class="text-center">
                        <td colspan="4">No Item Added
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
