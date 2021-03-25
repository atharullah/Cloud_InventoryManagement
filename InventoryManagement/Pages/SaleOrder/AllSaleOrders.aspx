<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllSaleOrders.aspx.cs" Inherits="InventoryManagement.Pages.SaleOrder.AllSaleOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/jquery.dynatable.css" rel="stylesheet" />
    <script src="/Scripts/jquery.dynatable.js"></script>
    <script src="/Scripts/Custom.js"></script>
    <link href="/CSS/MyDynaTable.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="form-group">
                <div class="text-center">Sale Orders</div>
                <h4 class="text-center">
                    <asp:Label runat="server" ID="lblMessage"></asp:Label></h4>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="Add Sale Order" runat="server" PostBackUrl="SaleOrder.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

     <div class="row">
         <div class="col-md-12">
             <asp:Repeater runat="server" ID="rptrSaleOrders" OnItemCommand="rptrSaleOrders_ItemCommand">
                 <HeaderTemplate>
                     <table class="table table-hover table-bordered text-center datatable">
                         <thead>
                             <tr>
                                 <th>Bill No</th>
                                 <th>Customer Name</th>
                                 <th>Mobile</th>
                                 <th>Total Cost</th>
                                 <th>Paid Amount</th>
                                 <th>Remaining Amount</th>
                                 <th>SaleOrderDate</th>
                                 <th>Completed</th>
                                 <th>Making</th>
                                 <th>Action</th>
                             </tr>
                         </thead>
                         <tbody>
                 </HeaderTemplate>
                 <ItemTemplate>
                     <tr>
                         <td><%#Eval ("BillNo") %></td>
                         <td><%#Eval ("CustomerName") %></td>
                         <td><%#Eval ("CustomerMobile") %></td>
                         <td><%#Eval ("TotalCost") %></td>
                         <td><%#Eval ("PaidAmount") %></td>
                         <td><%#Eval ("RemainingAmount") %></td>
                         <td><%#Eval ("SaleOrderDate") %></td>
                         <td><%#Eval ("IsCompleted") %></td>
                         <td><%#Eval ("IsMakingRequired") %></td>
                         <td>
                             <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("SaleOrderID")%>'></asp:LinkButton>
                         </td>
                     </tr>
                 </ItemTemplate>
                 <FooterTemplate>
                     </tbody>
                    <tfoot>
                        <tr runat="server" visible="<%#((Repeater)Container.NamingContainer).Items.Count==0 %>" style="text-align: center">
                            <td colspan="9">No Record Found
                            </td>
                        </tr>
                    </tfoot>
                     </table>
                 </FooterTemplate>
             </asp:Repeater>
         </div>
     </div>
</asp:Content>
