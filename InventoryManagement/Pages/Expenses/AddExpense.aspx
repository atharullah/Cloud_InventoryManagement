<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddExpense.aspx.cs" Inherits="InventoryManagement.Pages.Expenses.AddExpense" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Add Expense</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All Expense" runat="server" PostBackUrl="AllExpenses.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;
    <div class="row">

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Expense Bill No. :"></asp:Label>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtExpenseBillNo" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Expense Description :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtExpenseDescription" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtExpenseDescription" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

        </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Expense Amount :"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtExpenseAmount" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtExpenseAmount" TextMode="Number"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Expense Date :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtExpenseDate" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtExpenseDate" TextMode="Date"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnSubmit" Text="Add Expense" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>

