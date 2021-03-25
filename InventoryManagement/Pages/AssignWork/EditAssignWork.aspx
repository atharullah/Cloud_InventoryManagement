<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAssignWork.aspx.cs" Inherits="InventoryManagement.Pages.AssignWork.EditAssignWork" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Update Work</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnAssignWorkID" />
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All Assign Work" runat="server" PostBackUrl="AllAssignWork.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Employee Name :*"></asp:Label></label>
                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlEmployeeName" Enabled="false"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEmployeeName" ErrorMessage="Required" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Bill No. :*"></asp:Label></label>
                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlBillNo" Enabled="false"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBillNo" ErrorMessage="Required" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Work Type"></asp:Label></label>
                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlWorkType" Enabled="false"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlWorkType" ErrorMessage="Required" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Items Count :*"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtItemCount" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItemCount" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Completed Count"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtCompletedCount" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label5" runat="server" Text="Work Assign Date"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtWorkAssignDate" TextMode="Date"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label2" runat="server" Text="Expected Delivery Date"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtExpectedDeliveryDate" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label3" runat="server" Text="Amount Paid :*"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtAmountPaid" TextMode="Number" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmountPaid" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label4" runat="server" Text="Total Cost :*"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtTotalCost" TextMode="Number" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTotalCost" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label1" runat="server" Text="Amount Paid Date :*"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtPaidDate" TextMode="Date"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPaidDate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label7" runat="server" Text="Is Work Completed"></asp:Label></label>
                <asp:CheckBox CssClass="form-control" runat="server" ID="chkIsWorkCompleted" ClientIDMode="Static" />
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label8" runat="server" Text="Remarks"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <table class="table table-bordered">
                <tr>
                    <th>Inventory Name</th>
                    <th>Quantity</th>
                    <th>Action</th>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ValidationGroup="InventoryDetail" CssClass="form-control" runat="server" ID="ddlInventoryType"></asp:DropDownList>
                        <asp:RequiredFieldValidator ValidationGroup="InventoryDetail" runat="server" ControlToValidate="ddlInventoryType" InitialValue="0" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ValidationGroup="InventoryDetail" CssClass="form-control" TextMode="Number" runat="server" ID="txtQuantity" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="InventoryDetail" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ValidationGroup="InventoryDetail" runat="server" ID="btnAddRecord" OnClick="btnAddRecord_Click" Text="Add" CssClass="btn btn-primary btn-block" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Repeater runat="server" ID="rptrData" OnItemCommand="rptrData_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-bordered printRptr">
                        <tr>
                            <th>Inventory Name</th>
                            <th>Quantity</th>
                            <th>Action</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblItemName" Text='<%#Eval("ItemName")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblQuantity" Text='<%#Eval("Quantity")%>'></asp:Label></td>                        
                        <td>
                            <asp:HiddenField runat="server" ID="hdnItemID" Value='<%#Eval("ItemID")%>' />
                            <asp:HiddenField runat="server" ID="hdnWorkassignUsedID" Value='<%#Eval("WorkAssignInventoryUsedID")%>' />
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" runat="server" ID="lnkEdit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" runat="server" ID="lnkDelete" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are You Sure')"></asp:LinkButton>
                        </td>
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
            <%-- Only for delete item and edit item of repaeter --%>
            <asp:HiddenField runat="server" ID="hdnItemIndex" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnWorkAssign" Text="Assign Work" OnClick="btnWorkAssign_Click" />
            </div>
        </div>
    </div>

</asp:Content>