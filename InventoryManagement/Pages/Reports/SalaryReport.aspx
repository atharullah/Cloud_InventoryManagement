<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalaryReport.aspx.cs" Inherits="InventoryManagement.Pages.Reports.SalaryReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topHeader">
        <div class="text-center">Employee Salary Report</div>
    </div>
    <h4 class="text-center">
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </h4>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="From Month"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtFromMonth" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>
                    <asp:Label runat="server" Text="To Month"></asp:Label></label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtToMonth" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                 <label>&nbsp;</label>
                <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnGenerateReport" Text="Generate Report" OnClick="btnGenerateReport_Click" />
            </div>
        </div>
    </div>
    
   <div class="row">
        <div class="col-md-12">
            <asp:Repeater runat="server" ID="rptrSalaryReport">
                <HeaderTemplate>
                    <table class="table text-center table-bordered table-responsive table-hover">
                        <tr>
                            <th>Employee Name</th>
                            <th>Paid Amount</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblInvName" Text='<%#Eval("EmployeeName")%>'></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="Label2" Text='<%#Eval("PaidAmount")%>'></asp:Label></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr runat="server" visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' class="text-center">
                        <td colspan="2">No Item Added
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>