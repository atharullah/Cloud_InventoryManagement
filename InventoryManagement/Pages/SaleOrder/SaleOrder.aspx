<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SaleOrder.aspx.cs" Inherits="InventoryManagement.Main.SaleOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/SaleOrder.js"></script>
    <style>
        .newCustomer {
            float: right;
            padding-right: 2%;
        }

        .lnkbtnNewItem {
            float: right;
            padding-left: 2%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Sales Order</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static"></asp:Label>
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All Sale Orders" runat="server" PostBackUrl="AllSaleOrders.aspx"></asp:LinkButton>
            </div>
        </div>
        <div class="col-md-4"></div>
        <div class="col-md-4">
            <div class="form-group">
                <div class="form-group text-right printBtnDiv" style="display: none">
                    <input type="button" id="btnPrintOrder" class="btn btn-primary btn-block" value="Print Order" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="form-group">
                <label>Bill NO</label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtBillNo" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>Order Date :*</label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSellingDate" ClientIDMode="Static" TextMode="Date"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="orderDetail" runat="server" ControlToValidate="txtSellingDate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    Customer Name :*
                </label>
                <asp:LinkButton runat="server" CausesValidation="false" OnClientClick="return false" CssClass="newCustomer">
                    <span class="glyphicon glyphicon-plus"></span>New
                </asp:LinkButton>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtAutoCustomerName" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox CssClass="form-control hide" runat="server" ID="txtCustomerID" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="orderDetail" runat="server" ControlToValidate="txtCustomerID" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>Class Name</label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtClass"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <div class="form-group">
                <label>Making Required</label>
                <asp:CheckBox CssClass="form-control" runat="server" ID="chkIsMakingRequired" ClientIDMode="Static" />
            </div>
        </div>
    </div>

    <div class="row topHeader rowMaking" style="display: none">
        <div class="col-md-12">
            <div class="text-center">Making Detail</div>
        </div>
    </div>
    <div class="row rowMaking" style="display: none">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Inventory Type</label>
                        <asp:LinkButton runat="server" CausesValidation="false" OnClientClick="return false" CssClass="lnkbtnNewItem">
                            <span class="glyphicon glyphicon-plus"></span>New
                        </asp:LinkButton>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtAutoMakingInventoryType" ClientIDMode="Static"></asp:TextBox>
                        <asp:TextBox CssClass="form-control hide" runat="server" ID="txtMakingInventoryType" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Making Cost</label>
                        <asp:TextBox CssClass="form-control" ValidationGroup="orderDetail" runat="server" ID="txtMakingCost" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Work Type Status</label>
                        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlWorkType"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Top Measurement</label>
                        <asp:TextBox runat="server" ID="txtTopMeasurement" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Bottom Measurement</label>
                        <asp:TextBox runat="server" ID="txtBottomMeasurement" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Design Photo</label>
                        <asp:FileUpload CssClass="form-control" runat="server" ID="fileuploadDesign" />
                        <asp:Image runat="server" ID="imgDesignPhoto" Visible="false" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Confirm Date</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtConfirmDate" ClientIDMode="Static" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Delivery Date</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtDeliveryDate" ClientIDMode="Static" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Item Detail</div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <table class="table table-bordered">
                <tr>
                    <th>Item Code
                        <asp:LinkButton runat="server" CausesValidation="false" OnClientClick="return false" CssClass="lnkbtnNewItem">
                            <span class="glyphicon glyphicon-plus"></span>New
                        </asp:LinkButton>
                    </th>
                    <th>Item Name</th>
                    <th>Quantity</th>
                    <th>Selling Rate</th>
                    <th>VAT(%)</th>
                    <th>Total Amount</th>
                    <th>Action</th>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ValidationGroup="InventoryDetail" CssClass="form-control" runat="server" ID="txtAutoItemCode" ClientIDMode="Static"></asp:TextBox>
                        <asp:TextBox ValidationGroup="InventoryDetail" CssClass="form-control hide" runat="server" ID="txtItemCode" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="InventoryDetail" runat="server" ControlToValidate="txtItemCode" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtItemName" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="InventoryDetail" runat="server" ControlToValidate="txtItemName" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ValidationGroup="InventoryDetail " CssClass="form-control" TextMode="Number" runat="server" ID="txtQuantity" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="InventoryDetail" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ValidationGroup="InventoryDetail" CssClass="form-control" TextMode="Number" runat="server" ID="txtSellingRate" ClientIDMode="Static" step="0.01" min="0"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="InventoryDetail" runat="server" ControlToValidate="txtSellingRate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ValidationGroup="InventoryDetail itemVatTBoxClass" CssClass="form-control" TextMode="Number" runat="server" ID="txtVAT" ClientIDMode="Static" step="0.01" min="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ValidationGroup="InventoryDetail" CssClass="form-control" TextMode="Number" runat="server" ID="txtTotalAmount" ClientIDMode="Static" step="0.01" min="0"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="InventoryDetail" runat="server" ControlToValidate="txtTotalAmount" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <div class="hdndivItemID" style="display: none">
                            <asp:HiddenField runat="server" ID="hdnItemID" ClientIDMode="Static" />
                        </div>
                        <asp:Button ValidationGroup="InventoryDetail" runat="server" ID="btnAddRecord" OnClick="btnAddRecord_Click" Text="Add" CssClass="btn btn-primary btn-block" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" id="divItems" runat="server">
            <asp:Repeater runat="server" ID="rptrData" OnItemCommand="rptrData_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-bordered printRptr">
                        <tr>
                            <th>Item Code</th>
                            <th>Item Name</th>
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
                            <asp:Label runat="server" ID="lblSellingRate" Text='<%#Eval("SellingRate")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblQuantity" Text='<%#Eval("Quantity")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblVat" Text='<%#Eval("VAT")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblTotalAmount" CssClass="rptrTotalAmtClass" Text='<%#Eval("TotalAmount")%>'></asp:Label></td>
                        <td>
                            <%-- To store value in inventory type id column --%>
                            <asp:HiddenField runat="server" ID="hdnItemID" Value='<%#Eval("ItemID")%>' />
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" runat="server" ID="lnkEdit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" runat="server" ID="lnkDelete" Text="Delete" CommandName="Delete"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr runat="server" visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' class="text-center">
                        <td colspan="7">No Item Added
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
                        <label>Customer Type</label>
                        <asp:DropDownList runat="server" ID="ddlCustomerType" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Total Amount :*</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtTotalOrderAmount" TextMode="Number" ClientIDMode="Static" step="0.01" min="0"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="orderDetail" runat="server" ControlToValidate="txtTotalOrderAmount" ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Amount Paid :*</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPaidAmount" TextMode="Number" ClientIDMode="Static" step="0.01" min="0"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="orderDetail" runat="server" ControlToValidate="txtPaidAmount" ErrorMessage="Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Balance Amount</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtBalance" TextMode="Number" ClientIDMode="Static" ReadOnly="true" step="0.01" min="0"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Order Complete</label>
                        <asp:CheckBox CssClass="form-control" runat="server" ID="chkIsComplete" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Previous Balance</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPreviousBalance" TextMode="Number" ClientIDMode="Static" step="0.01" min="0"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Remarks</label>
                        <asp:TextBox CssClass="form-control" ValidationGroup="Inventory" runat="server" TextMode="MultiLine" Rows="4" ID="txtRemarks"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" ValidationGroup="orderDetail" runat="server" ID="btnAddSaleOrder" Text="Place Order" OnClick="btnAddSaleOrder_Click" />
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CausesValidation="false" CssClass="btn btn-primary btn-block" ValidationGroup="orderDetail" runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
            </div>
        </div>
    </div>

    <div style="display: none" id="printDiv">
        <h3 style="text-align:center">
            <asp:Label runat="server" ID="printlblCompanyName"></asp:Label>
        </h3>
        <h4 style="text-align:center">
            <asp:Label runat="server" ID="printlblCompanyAddress"></asp:Label>
        </h4>
        <table class="table table-bordered">
            <tr>
                <th>Bill No
                </th>
                <td>
                    <asp:Label runat="server" ID="printlblBillNo" ClientIDMode="Static"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Person Name
                </th>
                <td>
                    <asp:Label runat="server" ID="printlblPersonName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Total Amount</th>
                <td>
                    <asp:Label runat="server" ID="printlblTotalAmount"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Repeater runat="server" ID="rptrPrint">
            <HeaderTemplate>
                <table class="table table-bordered">
                    <tr>
                        <th>Item Name</th>
                        <th>Selling Rate</th>
                        <th>Quantity</th>
                        <th>VAT</th>
                        <th>Total Amount</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblItemName" Text='<%#Eval("ItemName")%>'></asp:Label></td>

                    <td>
                        <asp:Label runat="server" ID="lblSellingRate" Text='<%#Eval("SellingRate")%>'></asp:Label></td>
                    <td>
                        <asp:Label runat="server" ID="lblQuantity" Text='<%#Eval("Quantity")%>'></asp:Label></td>
                    <td>
                        <asp:Label runat="server" ID="lblVat" Text='<%#Eval("VAT")%>'></asp:Label></td>
                    <td>
                        <asp:Label runat="server" ID="lblTotalAmount" Text='<%#Eval("TotalAmount")%>'></asp:Label></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr runat="server" visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' class="text-center">
                    <td colspan="5">No Item</td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <div>

        <div runat="server" id="divAddCustomerScript" visible="false">
            <script>
                $(function () {
                    $(".divAddCustomer").modal();
                });
            </script>
        </div>

        <div class="divAddCustomer modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4>Add Customer</h4>
                        <asp:Label runat="server" ID="lblPopupCustomerResult"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        <asp:Label runat="server" Text="Customer Name :*"></asp:Label>
                                    </label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPopupCustomerName"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="popupCustomer" runat="server" ControlToValidate="txtPopupCustomerName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        <asp:Label runat="server" Text="Mobile :*"></asp:Label>
                                    </label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPopupCustomerMobile" TextMode="Number"></asp:TextBox>
                                    <asp:RegularExpressionValidator ValidationGroup="popupCustomer" runat="server" ControlToValidate="txtPopupCustomerMobile" ErrorMessage="Not Valid Mobile" ForeColor="Red" ValidationExpression="^\d{10}$" Display="Dynamic"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="popupCustomer" runat="server" ControlToValidate="txtPopupCustomerMobile" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        <asp:Label runat="server" Text="Email :"></asp:Label></label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPopupEmail" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ValidationGroup="popupCustomer" CssClass="btn btn-primary btn-block" runat="server" ID="btnPopupAddCustomer" Text="Add Customer" OnClick="btnPopupAddCustomer_Click" />
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
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="InvPopup" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPopupInventoryTypeName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Inventory Code :*</label>
                                    <asp:TextBox runat="server" ID="txtPopupInventoryCode" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="InvPopup" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPopupInventoryCode" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ValidationGroup="InvPopup" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPopupUnit" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
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
