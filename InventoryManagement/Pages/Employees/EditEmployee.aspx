<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="InventoryManagement.Pages.Employees.EditEmployee" %>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Edit Employee</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnEmployeeID" />
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All Employee" runat="server" PostBackUrl="AllEmployees.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;
    <div class="row">

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Employee Name :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmployeeName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmployeeName"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Employee Mobile :*"></asp:Label>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmployeeMobile" ErrorMessage="Not Valid Mobile" ForeColor="Red" ValidationExpression="^\d{10}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmployeeMobile" ForeColor="Red" ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmployeeMobile" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Employee Email :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail" TextMode="Email"></asp:TextBox>

            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Employee Details :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmployeeDetail" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Employee Address :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmployeeAddress" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Joining Date :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmpJoiningDate" TextMode="Date"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnUpdate" Text="Update Employee" OnClick="btnUpdate_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
