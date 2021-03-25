<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllPurchaseOrders.aspx.cs" Inherits="InventoryManagement.Inventory.AllPurchaseOrders" %>

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
                <div class="text-center">Purchase Orders</div>
                <h4 class="text-center">
                    <asp:Label runat="server" ID="lblMessage"></asp:Label></h4>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="Add Purchase Order" runat="server" PostBackUrl="AddPurchaseOrder.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

     <div class="row">
         <div class="col-md-12">
             <asp:Repeater runat="server" ID="rptrPurchaseOrders" OnItemCommand="rptrPurchaseOrders_ItemCommand">
                 <HeaderTemplate>
                     <table class="table table-hover table-bordered text-center datatable">
                         <thead>
                             <tr>
                                 <th>Own Bill No</th>
                                 <th>Total Order Amount</th>
                                 <th>Amount Paid</th>
                                 <th>Purchase Bill No</th>
                                 <th>Purchase Date</th>
                                 <th>Amount Paid Date</th>
                                 <th>Is Completed</th>
                                 <th>Action</th>
                             </tr>
                         </thead>
                         <tbody>
                 </HeaderTemplate>
                 <ItemTemplate>
                     <tr>
                         <td><%#Eval ("OwnBillNo") %></td>
                         <td><%#Eval ("TotalOrderAmount") %></td>
                         <td><%#Eval ("AmountPaid") %></td>
                         <td><%#Eval ("PurchaseBillNo") %></td>
                         <td><%#Eval ("PurchaseDate") %></td>
                         <td><%#Eval ("AmountPaidDate") %></td>
                         <td><%#Eval ("IsCompleted") %></td>
                         <td>
                             <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("InventoryOrderID")%>'></asp:LinkButton>
                         </td>
                     </tr>
                 </ItemTemplate>
                 <FooterTemplate>
                     </tbody>
                    <tfoot>
                        <tr runat="server" visible="<%#((Repeater)Container.NamingContainer).Items.Count==0 %>" style="text-align: center">
                            <td colspan="7">No Record Found
                            </td>
                        </tr>
                    </tfoot>
                     </table>
                 </FooterTemplate>
             </asp:Repeater>
         </div>
     </div>

    <%--<div class="row midHeader">
        <div class="col-md-12">
            <div class="text-center">Search</div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>Bill No/Remarks</label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSearchCombineName"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label>Date</label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSearchDate" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>&nbsp;</label>
                <asp:Button CssClass="btn btn-primary btn-block" ValidationGroup="searchGroup" runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>
    <div class="row text-center midHeader">
        <div class="col-md-12">
            <div class="">Inventory Orders</div>
        </div>
    </div>--%>
    <%--<div class="row">
        <div class="col-md-12">
            <asp:GridView runat="server" ID="gridInventoryList" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" AllowPaging="true" PageSize="15"
                OnPageIndexChanging="gridInventoryList_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Purchase Bill No">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPurchaseBillNo" Text='<%#Eval("PurchaseBillNo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Order Amount">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTotalOrderAmount" CssClass="rptrTotalOrderAmt" Text='<%#Eval("TotalOrderAmount")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount Paid">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAmountPaid" CssClass="rptrAmtPaid" Text='<%#Eval("AmountPaid")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase Date">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPurchaseDate" Text='<%#Eval("PurchaseDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Completed">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblIsCompleted" CssClass="rptrIsComplete" Text='<%#Eval("IsCompleted")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnAmountPaidDate" Value='<%#Eval("AmountPaidDate")%>' />
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnRemarks" Value='<%#Eval("Remarks")%>' />
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnrptrinventoryID" Value='<%#Eval("InventoryOrderID")%>' />
                            <input type="button" value="Edit" class="btn btn-primary rptrEdit" data-target="#modalPopup" data-toggle="modal" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No record found
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>--%>



    <%-- <div class="modal fade" role="dialog" id="modalPopup">
        <asp:HiddenField runat="server" ID="hdnInventoryOrderID" ClientIDMode="Static" />
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    Update Order
                </div>
                <div class="modal-body">
                    <asp:HiddenField runat="server" ID="hdnOldPaidAmt" ClientIDMode="Static" />
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Amount Paid</label>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtpopupAmtPaid" TextMode="Number" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="popupInventory" runat="server" ControlToValidate="txtpopupAmtPaid" ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Total Amount</label>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtpopupTotalAmt" TextMode="Number" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="popupInventory" runat="server" ControlToValidate="txtpopupTotalAmt" ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Order Complete</label>
                                <asp:CheckBox runat="server" ID="chkpopupOrderComplete" CssClass="form-control" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Amount Paid Date</label>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtpopupamtpaiddate" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="popupInventory" runat="server" ControlToValidate="txtpopupamtpaiddate" ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Remarks</label>
                                <asp:TextBox CssClass="form-control" ValidationGroup="popupInventory" runat="server" TextMode="MultiLine" Rows="2" ID="txtpopupRemarks" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Button CssClass="btn btn-primary btn-block" ValidationGroup="popupInventory" runat="server" ID="btnUpdateInventory" Text="Update Inventory" OnClick="btnUpdateInventory_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <%--<div id="hdnInventoryRate">--%>
    <%-- To set item rate --%>
    <%--<asp:HiddenField runat="server" ID="hdnInvRate" ClientIDMode="Static" />
    </div>--%>
</asp:Content>
