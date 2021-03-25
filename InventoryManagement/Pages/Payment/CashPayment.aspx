<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CashPayment.aspx.cs" Inherits="InventoryManagement.Pages.Payment.CashPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/SaleOrder.js"></script>
    <%--<script>
        $(function () {
            if ($("#printlblBillNo").text() != "")
                $(".printBtnDiv").show();
            else
                $(".printBtnDiv").hide();

            $("#btnPrintOrder").click(function () {
                if ($("#printlblBillNo").text() != "")
                    PrintDiv();
            });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Payment</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </h4>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
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
                <label>
                    <asp:Label runat="server" Text="Order Type :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlOrderType" ForeColor="Red" ErrorMessage="Required" InitialValue="0"></asp:RequiredFieldValidator>
                </label>
                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlOrderType" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Sale" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Purchase" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Bill No. :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBillNo" ForeColor="Red" ErrorMessage="Required" InitialValue="0"></asp:RequiredFieldValidator>
                </label>
                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlBillNo" AutoPostBack="true" OnSelectedIndexChanged="ddlBillNo_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-3" runat="server" id="divPersonName" visible="false">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Person/Organisation Name :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPersonOrgName" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtPersonOrgName" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-3" runat="server" id="divmobile" visible="false">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Mobile :"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtMobile" TextMode="Number" Enabled="false"></asp:TextBox>
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="panelDetail" Visible="false">
        <div class="row">

            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        <asp:Label runat="server" Text="Remaining Amount :*"></asp:Label>
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtRemainingAmt" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        <asp:Label runat="server" Text="Amount Paid Date :*"></asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmountPaidDate" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtAmountPaidDate" TextMode="Date"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        <asp:Label runat="server" Text="Payee Name :*"></asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPayeeName" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPayeeName"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-3" runat="server" id="divSeller">
                <div class="form-group">
                    <label>
                        <asp:Label runat="server" Text="Pay To Seller :*"></asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPayToSeller" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPayToSeller" TextMode="Number"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-3" runat="server" id="divcustomer">
                <div class="form-group">
                    <label>
                        <asp:Label runat="server" Text="Customer Pay :*"></asp:Label>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerPay" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtCustomerPay" TextMode="Number"></asp:TextBox>
                </div>
            </div>

        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnSubmit" Text="Add Payment" OnClick="btnSubmit_Click" />
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
                <th>Amount Paid</th>
                <td>
                    <asp:Label runat="server" ID="printlblAmountPaid"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
