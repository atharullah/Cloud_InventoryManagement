<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllAssignWork.aspx.cs" Inherits="InventoryManagement.Pages.AssignWork.AllAssignWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="/CSS/jquery.dynatable.css" rel="stylesheet" />
    <script src="/Scripts/jquery.dynatable.js"></script>
    <script src="/Scripts/Custom.js"></script>
    <link href="/CSS/MyDynaTable.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row topHeader">
        <div class="col-md-12">
            <div class="text-center">All Assign Work</div>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:LinkButton CausesValidation="false" CssClass="btn btn-primary btn-block" Text="Assign Work" runat="server" PostBackUrl="AddAssignWork.aspx"></asp:LinkButton>
        </div>
    </div>
    &nbsp;

     <div class="row">
         <div class="col-md-12">

             <asp:Repeater runat="server" ID="rptrWorkAssign" OnItemCommand="rptrWorkAssign_ItemCommand">
                 <HeaderTemplate>
                     <table class="table table-hover table-bordered text-center datatable">
                         <thead>
                             <tr>
                                 <th>Employee Name</th>
                                 <th>Bill No</th>
                                 <th>No Of Items</th>
                                 <th>Work Assign Date</th>
                                 <th>Expected Completion Date</th>
                                 <th>Amount Paid</th>
                                 <th>Total Cost</th>
                                 <th>Action</th>
                             </tr>
                         </thead>
                         <tbody>
                 </HeaderTemplate>
                 <ItemTemplate>
                     <tr>
                         <td><%#Eval("EmployeeName") %></td>
                         <td><%#Eval("BillNo") %></td>
                         <td><%#Eval("WorkCount") %></td>
                         <td><%#Eval("WorkAssignDate") %></td>
                         <td><%#Eval("ExpectedCompletionDate") %></td>
                         <td><%#Eval("AmountPaid") %></td>
                         <td><%#Eval("TotalCost") %></td>
                         <td>
                             <asp:LinkButton CommandArgument='<%#Eval("WorkAssignID")%>' Text="Edit" CausesValidation="false" CssClass="btn btn-primary" runat="server" CommandName="Edit"></asp:LinkButton>
                         </td>
                     </tr>
                 </ItemTemplate>
                 <FooterTemplate>
                     </tbody>
                    <tfoot>
                        <tr runat="server" visible="<%#((Repeater)Container.NamingContainer).Items.Count==0 %>" style="text-align: center">
                            <td colspan="8">No Record Found
                            </td>
                        </tr>
                    </tfoot>
                     </table>
                 </FooterTemplate>
             </asp:Repeater>

         </div>
     </div>

    <%-- <asp:GridView runat="server" ID="gridWorkAssign" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                 <Columns>
                     <asp:TemplateField HeaderText="Employee Name">
                         <ItemTemplate>
                             <asp:Label runat="server" ID="lblEmployeeName" Text='<%#Eval("EmployeeName") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Bill No">
                         <ItemTemplate>
                             <asp:Label runat="server" ID="lblBillNo" Text='<%#Eval("BillNo") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="No Of Items">
                         <ItemTemplate>
                             <asp:Label runat="server" ID="lblTaskTypeName" Text='<%#Eval("WorkCount") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Work Assign Date">
                         <ItemTemplate>
                             <asp:Label runat="server" ID="lblWorkAssignDate" Text='<%#Eval("WorkAssignDate") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Expected Completion Date">
                         <ItemTemplate>
                             <asp:Label runat="server" ID="lblExpectedCompletionDate" Text='<%#Eval("ExpectedCompletionDate") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Amount Paid">
                         <ItemTemplate>
                             <asp:Label runat="server" ID="lblAmountPaid" Text='<%#Eval("AmountPaid") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Total Cost">
                         <ItemTemplate>
                             <asp:Label runat="server" ID="lblTotalCost" Text='<%#Eval("TotalAmountToPay") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
                 <EmptyDataTemplate>
                     No Record Found
                 </EmptyDataTemplate>
             </asp:GridView>--%>
</asp:Content>
