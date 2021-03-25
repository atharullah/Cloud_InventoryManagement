<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCheque.aspx.cs" Inherits="InventoryManagement.Pages.Cheque.AddCheque" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Add Cheque</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Payment Bill No. :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBillNo" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtBillNo"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Cheque No. :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtChequeNo" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtChequeNo"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Cheque Due Date :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtChequeDueDate" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtChequeDueDate" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Amount :"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmount" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtAmount" TextMode="Number"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Cheque Issue Date :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtChequeIssueDate" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtChequeIssueDate" TextMode="Date"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Cheque Clear :*"></asp:Label>
                </label>
                <asp:CheckBox CssClass="form-control" runat="server" ID="chkChequeClear"></asp:CheckBox>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnSubmit" Text="Add Cheque" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
