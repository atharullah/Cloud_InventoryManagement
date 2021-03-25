<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditSeller.aspx.cs" Inherits="InventoryManagement.Pages.Seller.EditSeller" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Edit Seller</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnSellerID" />
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All Seller" runat="server" PostBackUrl="AllSellers.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;
    <div class="row">

        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Party Name :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSellerName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSellerName"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Mobile :*"></asp:Label>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMobile" ErrorMessage="Not Valid Mobile" ForeColor="Red" ValidationExpression="^\d{10}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtMobile" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Landline :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtLandline" TextMode="Number"></asp:TextBox>
            </div>
        </div>

    </div>
    
    <div class="row">
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Account No. :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtAccountNo" TextMode="Number"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Account Holder Name :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtAccountHolderName"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Bank Name :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtBankName"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="IFSC Code :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtIFSC"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="VAT NO. :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtVatNo"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="TIN NO. :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtTinNo"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Email :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail" TextMode="Email"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Party Address :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSellerAddress" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnUpdate" Text="Update Party" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Content>
