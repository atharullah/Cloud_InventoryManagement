<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="InventoryManagement.Pages.Users.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Add User</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All User" runat="server" PostBackUrl="AllUsers.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;
    <div class="row">

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Employee Name :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEmployee" InitialValue="0" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlEmployee"></asp:DropDownList>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="User Name :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtUserName"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Password :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Role :*"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRole" InitialValue="0" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </label>
                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlRole"></asp:DropDownList>
                
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="Remarks :"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnSubmit" Text="Add User" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
