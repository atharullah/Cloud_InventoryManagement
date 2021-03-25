<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllInventory.aspx.cs" Inherits="InventoryManagement.Pages.Inventorys.AllInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/jquery.dynatable.css" rel="stylesheet" />
    <script src="/Scripts/jquery.dynatable.js"></script>
    <script src="/Scripts/Custom.js"></script>
    <link href="/CSS/MyDynaTable.css" rel="stylesheet" />
    <script>
        $(function () {

            $(".invPRateClass").hide();

            if ($("#hdnCurrentUserRole").val() == "1")
            {
                $(".btnShowPRateClass").show();
            }               
            else
            {
                $(".btnShowPRateClass").hide();
            }
                
            $(".btnShowPRateClass").click(function () {
                $(".invPRateClass").toggle("slow");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">Inventories</div>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="Add Inventory" runat="server" PostBackUrl="AddInventory.aspx"></asp:LinkButton>
        </div>
        <div class="col-md-6">
        </div>
        <div class="col-md-2">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block btnShowPRateClass" Text="Show PurchaseRate" runat="server" OnClientClick="return false" ClientIDMode="Static"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

   <%-- <div class="row">
        <div class="col-md-12">
            <div class="text-center midHeader">All Inventory Type</div>
            <asp:GridView runat="server" ID="gridInventoryTypes" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" AllowPaging="true"
                PageSize="15" OnPageIndexChanging="gridInventoryTypes_PageIndexChanging" OnRowCommand="gridInventoryTypes_RowCommand" OnRowEditing="gridInventoryTypes_RowEditing">
                <Columns>
                    <asp:TemplateField HeaderText="Inventory Type Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblInventoryTypeName" Text='<%#Eval("InventoryTypeName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Count">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblInventoryCount" Text='<%#Eval("InventoryCount")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Low Stock Count">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblLowStockCount" Text='<%#Eval("LowStockCount")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase Rate">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPurchaseRate" Text='<%#Eval("PurchaseRate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selling Rate">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblSellingRate" Text='<%#Eval("SellingRate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inventory Code">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblInventoryCode" Text='<%#Eval("InventoryCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rag No">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblRagNo" Text='<%#Eval("RagNo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fast Moving">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblFastMoving" Text='<%#Eval("FastMoving")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hdnUnitName" Value='<%#Eval("UnitName")%>'></asp:HiddenField>
                            <asp:HiddenField runat="server" ID="hdnInventoryDescription" Value='<%#Eval("InventoryDescription")%>'></asp:HiddenField>
                            <asp:HiddenField runat="server" ID="hdnInventoryTypeID" Value='<%#Eval("InventoryTypeId")%>'></asp:HiddenField>
                            <asp:LinkButton CausesValidation="false" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-primary"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No record found
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-md-12">
            <asp:Repeater runat="server" ID="rptrInventory" OnItemCommand="rptrInventory_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover table-bordered text-center datatable">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Code</th>
                                <th>Count</th>
                                <th>Low Count</th>
                                <th class="invPRateClass">Purchase Rate</th>
                                <th>Selling Rate</th>
                                <th>Rag No</th>
                                <th>VAT(%)</th>
                                <th>Fast Moving</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval ("InventoryTypeName") %></td>
                        <td><%#Eval ("InventoryCode") %></td>
                        <td><%#Eval ("InventoryCount") %></td>
                        <td><%#Eval ("LowStockCount") %></td>
                        <td class="invPRateClass"><%#Eval ("PurchaseRate") %></td>
                        <td><%#Eval ("SellingRate") %></td>
                        <td><%#Eval ("RagNo") %></td>
                        <td><%#Eval ("VAT") %></td>
                        <td><%#Eval ("FastMoving") %></td>
                        <td>
                            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("InventoryTypeId")%>'></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr runat="server" visible="<%#((Repeater)Container.NamingContainer).Items.Count==0 %>" style="text-align: center">
                            <td colspan="10">No Record Found
                            </td>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
