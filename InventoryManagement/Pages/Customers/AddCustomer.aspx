<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCustomer.aspx.cs" Inherits="InventoryManagement.Pages.Customers.AddCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Add Customer</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All Customer" runat="server" PostBackUrl="AllCustomers.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;
    <div class="row">

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Customer Name :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtCustomerName"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Mobile :*"></asp:Label>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtCustomerMobile" ErrorMessage="Not Valid Mobile" ForeColor="Red" ValidationExpression="^\d{10}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerMobile" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtCustomerMobile" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Email :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail" TextMode="Email"></asp:TextBox>
            </div>
        </div>

    </div>

    <div class="row">
        
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Customer Address :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtCustomerAddress" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnSubmit" Text="Add Customer" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
