<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPurchaseOrder.aspx.cs" Inherits="InventoryManagement.Pages.PurchaseOrder.AddPurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/PurchaseOrder.js"></script>
    <style>
        .lnkBtnAddItem,.lnkBtnAddSeller
        {
            float:right;
            padding-right:2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="form-group">
                <div class="text-center">Order Detail</div>
                <h4 class="text-center">
                    <asp:Label runat="server" ID="lblMessage"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All Purchase Orders" runat="server" PostBackUrl="AllPurchaseOrders.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

    <div class="row">
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Own Bill No :"></asp:Label></label>
                <asp:TextBox runat="server" ID="txtOwnBillNo" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Purchase Date :*"></asp:Label>
                    <asp:RequiredFieldValidator ValidationGroup="Inventory" runat="server" ControlToValidate="txtPurchaseDate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtPurchaseDate" ClientIDMode="Static" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Purchase Bill No :*"></asp:Label>
                    <asp:RequiredFieldValidator ValidationGroup="Inventory" runat="server" ControlToValidate="txtPurchaseBillNO" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox runat="server" ID="txtPurchaseBillNO" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Bill Date :*"></asp:Label>
                    <asp:RequiredFieldValidator ValidationGroup="Inventory" runat="server" ControlToValidate="txtBillDate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtBillDate" ClientIDMode="Static" TextMode="Date"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Seller Name :*"></asp:Label>
                    <asp:RequiredFieldValidator ValidationGroup="Inventory" runat="server" ControlToValidate="txtSellerID" ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:LinkButton runat="server" CssClass="lnkBtnAddSeller" CausesValidation="false" OnClientClick="return false">
                    <span class="glyphicon glyphicon-plus"></span>New
                </asp:LinkButton>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtAutoSeller" ClientIDMode="Static" plceholder="Enter Value"></asp:TextBox>
                <asp:TextBox CssClass="form-control hide" runat="server" ID="txtSellerID" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <table class="table table-bordered text-center table-responsive">
                    <tr>
                        <th>Item Code *
                            <asp:LinkButton runat="server" CssClass="lnkBtnAddItem" CausesValidation="false" OnClientClick="return false">
                                <span class="glyphicon glyphicon-plus"></span>New
                            </asp:LinkButton>
                        </th>
                        <th>Item Name*</th>
                        <th>Purchase Rate*</th>
                        <th>Selling Rate*</th>
                        <th>Quantity*</th>
                        <th>VAT(%)</th>
                        <th>Total Amount*</th>
                    </tr>
                    <tr>
                        <td style="width: 16%">
                            <%-- This hidden text box use to validate item should be select from autocomplete --%>
                            <asp:TextBox CssClass="form-control" ValidationGroup="orderItem" ClientIDMode="Static" runat="server" ID="txtAutoItemCode"></asp:TextBox>
                            <asp:TextBox CssClass="form-control hide itemCodeTBoxClass" ValidationGroup="orderItem" ClientIDMode="Static" runat="server" ID="txtItemCode"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="orderItem" runat="server" ControlToValidate="txtItemCode" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control itemNameTBoxClass" ValidationGroup="orderItem" runat="server" ClientIDMode="Static" ID="txtItemName"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="orderItem" runat="server" ControlToValidate="txtItemName" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control itemPrateTBoxClass" ValidationGroup="orderItem" ClientIDMode="Static" TextMode="Number" runat="server" ID="txtPurchasRate" step="0.01" min="0"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="orderItem" runat="server" ControlToValidate="txtPurchasRate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control itemSRateTBoxClass" ValidationGroup="orderItem" ClientIDMode="Static" TextMode="Number" runat="server" ID="txtSellingRate" step="0.01" min="0"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control itemQuantityTBoxClass" ValidationGroup="orderItem" ClientIDMode="Static" TextMode="Number" runat="server" ID="txtQuantity"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="orderItem" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control itemVatTBoxClass" ValidationGroup="orderItem" ClientIDMode="Static" TextMode="Number" runat="server" ID="txtVAT" step="0.01" min="0"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control itemTAmtTBoxClass" ValidationGroup="orderItem" ClientIDMode="Static" TextMode="Number" runat="server" ID="txtTotalAmount" step="0.01" min="0"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="orderItem" runat="server" ControlToValidate="txtTotalAmount" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <div class="hdndivItemID" style="display: none">
                                <asp:HiddenField runat="server" ID="hdnItemID" ClientIDMode="Static" />
                            </div>
                            <asp:Button ValidationGroup="orderItem" runat="server" ID="btnAddRecord" OnClick="btnAddRecord_Click" Text="Add" CssClass="btn btn-primary btn-block clientValidator" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:Repeater runat="server" ID="rptrData" OnItemCommand="rptrData_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-bordered text-center itemRecordTable">
                        <tr>
                            <th>Item Code</th>
                            <th>Item Name</th>
                            <th>Purchase Rate</th>
                            <th>Selling Rate</th>
                            <th>Quantity</th>
                            <th>VAT(%)</th>
                            <th>Total Amount</th>
                            <th>Action</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblItemCode" Text='<%#Eval("ItemCode")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblItemName" Text='<%#Eval("ItemName")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblPurchaseRate" Text='<%#Eval("PurchaseRate")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblSellingRate" Text='<%#Eval("SellingRate")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblQuantity" Text='<%#Eval("Quantity")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblVAT" Text='<%#Eval("VAT")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblTotalAmount" Text='<%#Eval("TotalAmount")%>' CssClass="rptrTotalAmt"></asp:Label></td>
                        <td>
                            <asp:HiddenField runat="server" ID="rptrhdnItemID" Value='<%#Eval("ItemID")%>' />
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" runat="server" ID="lnkEdit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" runat="server" ID="lnkDelete" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are You Sure')"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr runat="server" visible='<%#((Repeater)Container.NamingContainer).Items.Count==0 %>' style="text-align: center">
                        <td colspan="8">
                            <asp:Label runat="server" Text="No Item Added"></asp:Label>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <%-- Only for delete item and edit item of repaeter --%>
            <asp:HiddenField runat="server" ID="hdnItemIndex" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="Customer Type :"></asp:Label>
                        </label>
                        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlCustomerType" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="Total Amount :*"></asp:Label>
                            <asp:RequiredFieldValidator ValidationGroup="Inventory" runat="server" ControlToValidate="txtTotalOrderAmount" ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtTotalOrderAmount" TextMode="Number" ClientIDMode="Static" step="0.01" min="0"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="Amount Paid :*"></asp:Label>
                            <asp:RequiredFieldValidator ValidationGroup="Inventory" runat="server" ControlToValidate="txtPaidAmount" ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPaidAmount" TextMode="Number" ClientIDMode="Static" step="0.01" min="0"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="Amount Paid Date :*"></asp:Label>
                            <asp:RequiredFieldValidator ValidationGroup="Inventory" runat="server" ControlToValidate="txtAmountPaidDate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtAmountPaidDate" ClientIDMode="Static" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="Balance Amount :"></asp:Label></label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtBalance" TextMode="Number" ClientIDMode="Static" ReadOnly="true" step="0.01" min="0"></asp:TextBox>
                    </div>
                </div>
            </div>

        </div>
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="Order Complete :"></asp:Label></label>
                        <asp:CheckBox runat="server" ID="chkOrderComplete" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="Remarks :"></asp:Label></label>
                        <asp:TextBox CssClass="form-control" ValidationGroup="Inventory" runat="server" TextMode="MultiLine" Rows="2" ID="txtDetails"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block clientValidator" ValidationGroup="Inventory" runat="server" ID="btnAddInventoryOrders" Text="Add Order" OnClick="btnAddInventoryOrders_Click" />
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CausesValidation="false" CssClass="btn btn-primary btn-block" ValidationGroup="Inventory" runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
            </div>
        </div>
    </div>

    <div>
        <div runat="server" id="divAddSellerScript" visible="false">
            <script>
                $(function () {
                    $(".divAddSeller").modal();
                });
            </script>
        </div>

        <div class="divAddSeller modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>Add Seller</h4>
                        <asp:Label runat="server" ID="lblPopupSellerResult"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        <asp:Label runat="server" Text="Party Name :*"></asp:Label>
                                        <asp:RequiredFieldValidator ValidationGroup="popupSeller" runat="server" ControlToValidate="txtPopupSellerName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPopupSellerName"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        <asp:Label runat="server" Text="Mobile :*"></asp:Label>
                                        <asp:RegularExpressionValidator ValidationGroup="popupSeller" runat="server" ControlToValidate="txtPopupSellerMobile" ErrorMessage="Not Valid Mobile" ForeColor="Red" ValidationExpression="^\d{10}$" Display="Dynamic"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ValidationGroup="popupSeller" runat="server" ControlToValidate="txtPopupSellerMobile" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPopupSellerMobile" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        <asp:Label runat="server" Text="Landline :"></asp:Label></label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPopupSellerLandline" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        <asp:Label runat="server" Text="Email :"></asp:Label></label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPopupSellerEmail" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button CssClass="btn btn-primary btn-block" ValidationGroup="popupSeller" runat="server" ID="btnPopupAddSeller" Text="Add Seller" OnClick="btnPopupAddSeller_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div>

        <div runat="server" id="divAddInventoryScript" visible="false">
            <script>
                $(function () {
                    $(".divAddInventoryType").modal();
                });
            </script>
        </div>

        <div class="divAddInventoryType modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>Add Inventory Type</h4>
                        <asp:Label runat="server" ID="lblPopupInvResult"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Inventory Type Name :*</label>
                                    <asp:TextBox runat="server" ID="txtPopupInventoryTypeName" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="InvPopup" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPopupInventoryTypeName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Inventory Code :*</label>
                                    <asp:TextBox runat="server" ID="txtPopupInventoryCode" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="InvPopup" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPopupInventoryCode" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Inventory Description :</label>
                                    <asp:TextBox runat="server" ID="txtPopupInvDesc" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Unit Name :*</label>
                                    <asp:TextBox runat="server" ID="txtPopupUnit" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="InvPopup" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPopupUnit" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Purchase Rate :</label>
                                    <asp:TextBox runat="server" ID="txtPopupPurchaseRate" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Selling Rate :</label>
                                    <asp:TextBox runat="server" ID="txtPopupSellingRate" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Rag No :</label>
                                    <asp:TextBox runat="server" ID="txtPopupRagNo" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Low Stock Count :</label>
                                    <asp:TextBox runat="server" ID="txtPopupLowCount" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Fast Moving :</label>
                                    <asp:CheckBox runat="server" ID="chkPopupFastMoving" CssClass="form-control"></asp:CheckBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>VAT(%) :</label>
                                    <asp:TextBox runat="server" ID="txtPopupVat" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ValidationGroup="InvPopup" CssClass="btn btn-primary btn-block" runat="server" ID="btnPopupAddInventory" Text="Add Inventory" OnClick="btnPopupAddInventory_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>
