<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddInventory.aspx.cs" Inherits="InventoryManagement.Pages.Inventorys.AddInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Add Inventory</div>
            <h4 class="text-center">
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="All Inventory" runat="server" PostBackUrl="AllInventory.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;
  
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>Inventory Type Name :*</label>
                <asp:TextBox runat="server" ID="txtInventoryTypeName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInventoryTypeName" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>Inventory Code :*</label>
                <asp:TextBox runat="server" ID="txtInventoryCode" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInventoryCode" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>Inventory Description :</label>
                <asp:TextBox runat="server" ID="txtInvDesc" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>Unit Name :*</label>
                <asp:TextBox runat="server" ID="txtUnit" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnit" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>Purchase Rate :</label>
                <asp:TextBox runat="server" ID="txtPurchaseRate" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>Selling Rate :</label>
                <asp:TextBox runat="server" ID="txtSellingRate" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>Rag No :</label>
                <asp:TextBox runat="server" ID="txtRagNo" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>Low Stock Count :</label>
                <asp:TextBox runat="server" ID="txtLowCount" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>Fast Moving :</label>
                <asp:CheckBox runat="server" ID="chkFastMoving" CssClass="form-control"></asp:CheckBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>VAT(%) :</label>
                <asp:TextBox runat="server" ID="txtVAT" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnAddInventory" Text="Add Inventory" OnClick="btnAddInventory_Click" />
            </div>
        </div>
    </div>
</asp:Content>

