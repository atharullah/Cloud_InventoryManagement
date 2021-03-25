using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Payment
{
    public partial class CashPayment : System.Web.UI.Page
    {
        string currentUserName = string.Empty;
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                    txtAmountPaidDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlOrderType.SelectedValue != "0" && ddlBillNo.SelectedValue != "0")
                {
                    if (ddlOrderType.SelectedValue == "1")
                    {
                        int saleOrderID = Convert.ToInt32(ddlBillNo.SelectedValue);
                        Database.SalesOrder saleOrder = context.SalesOrders.Where(x => x.SaleOrderID == saleOrderID).SingleOrDefault();
                        if (saleOrder.BillNo == ddlBillNo.SelectedItem.Text)
                        {
                            saleOrder.ModifiedBy = currentUserName;
                            saleOrder.ModifiedDate = DateTime.Now.Date;
                            int amountPaid = txtCustomerPay.Text == "" ? 0 : Convert.ToInt32(txtCustomerPay.Text);
                            saleOrder.PaidAmount = (saleOrder.PaidAmount == null ? 0 : saleOrder.PaidAmount) + amountPaid;
                            decimal? totalCost = saleOrder.TotalCost == null ? 0 : saleOrder.TotalCost;
                            saleOrder.RemainingAmount = totalCost - saleOrder.PaidAmount;
                            context.SaveChanges();
                            lblMessage.Text = "Payment Updated For Sale Order for " + saleOrder.BillNo;
                            lblMessage.ForeColor = Color.Green;
                            CreatePrintDiv(saleOrder.BillNo, txtPayeeName.Text, amountPaid);
                        }
                        else
                        {
                            lblMessage.Text = "Not Found Bill No " + saleOrder.BillNo;
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    if (ddlOrderType.SelectedValue == "2")
                    {
                        int purchaseOrderID = Convert.ToInt32(ddlBillNo.SelectedValue);
                        Database.InventoryOrder purchaseOrder = context.InventoryOrders.Where(x => x.IsActive == true && x.InventoryOrderID == purchaseOrderID).SingleOrDefault();
                        if (purchaseOrder.PurchaseBillNo == ddlBillNo.SelectedItem.Text)
                        {
                            purchaseOrder.ModifiedBy = currentUserName;
                            purchaseOrder.ModifiedDate = DateTime.Now.Date;
                            int amountPaid = txtPayToSeller.Text == "" ? 0 : Convert.ToInt32(txtPayToSeller.Text);
                            purchaseOrder.AmountPaid = (purchaseOrder.AmountPaid == null ? 0 : purchaseOrder.AmountPaid) + amountPaid;
                            decimal? totalCost = purchaseOrder.TotalOrderAmount == null ? 0 : purchaseOrder.TotalOrderAmount;
                            purchaseOrder.BalanceAmount = totalCost - purchaseOrder.AmountPaid;
                            purchaseOrder.AmountPaidDate = DateTime.Now.Date;
                            context.SaveChanges();
                            lblMessage.Text = "Payment Updated For Sale Order for " + purchaseOrder.PurchaseBillNo;
                            lblMessage.ForeColor = Color.Green;
                            CreatePrintDiv(purchaseOrder.PurchaseBillNo, txtPayeeName.Text, amountPaid);
                        }
                        else
                        {
                            lblMessage.Text = "Not Found Bill No " + purchaseOrder.PurchaseBillNo;
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void ClearForm()
        {
            //txtAmount.Text = "";
            //txtBillNo.Text = "";
            //txtChequeDueDate.Text = "";
            //txtChequeIssueDate.Text = "";
            //txtChequeNo.Text = "";
            //chkChequeClear.Checked = false;
        }

        protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //For Sale
                if (ddlOrderType.SelectedValue == "1")
                {
                    var saleCollection = context.SalesOrders.Where(x => x.IsActive == true && x.IsCompleted == false)
                                                .Select(x => new
                                                {
                                                    x.SaleOrderID,
                                                    x.BillNo
                                                });
                    ddlBillNo.DataSource = saleCollection.ToDictionary(x => x.BillNo, y => y.SaleOrderID);
                    ddlBillNo.DataTextField = "Key";
                    ddlBillNo.DataValueField = "Value";
                    ddlBillNo.DataBind();
                    ddlBillNo.Items.Insert(0, new ListItem("Select", "0"));
                    panelDetail.Visible = false;
                }

                //For Purchase
                if (ddlOrderType.SelectedValue == "2")
                {
                    var saleCollection = context.InventoryOrders.Where(x => x.IsActive == true && x.IsCompleted == false)
                                                .Select(x => new
                                                {
                                                    x.InventoryOrderID,
                                                    x.PurchaseBillNo
                                                });
                    ddlBillNo.DataSource = saleCollection.ToDictionary(x => x.PurchaseBillNo, y => y.InventoryOrderID);
                    ddlBillNo.DataTextField = "Key";
                    ddlBillNo.DataValueField = "Value";
                    ddlBillNo.DataBind();
                    ddlBillNo.Items.Insert(0, new ListItem("Select", "0"));
                    panelDetail.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBillNo.SelectedValue != "0")
                {
                    string orderType = ddlOrderType.SelectedValue;
                    panelDetail.Visible = true;
                    divPersonName.Visible = true;
                    divmobile.Visible = true;

                    //For Sale Order
                    if (orderType == "1")
                    {
                        int saleOrderID = Convert.ToInt32(ddlBillNo.SelectedValue);
                        var saleOrderCollection = context.SalesOrders.Where(x => x.IsActive == true && x.SaleOrderID == saleOrderID);
                        if (saleOrderCollection.Count() > 0)
                        {
                            Database.SalesOrder saleOrder = saleOrderCollection.FirstOrDefault();
                            Database.Customer customer = context.Customers.Where(x => x.IsActive == true && x.CustomerID == saleOrder.CustomerID).SingleOrDefault();
                            txtMobile.Text = customer.CustomerMobile;
                            txtPersonOrgName.Text = customer.CustomerName;
                            txtRemainingAmt.Text =Convert.ToString(saleOrder.RemainingAmount);
                            divSeller.Visible = false;
                        }
                    }

                    //For Purchase Order
                    if (orderType == "2")
                    {
                        int purchaseOrderID = Convert.ToInt32(ddlBillNo.SelectedValue);
                        var purchaseOrderCollection = context.InventoryOrders.Where(x => x.IsActive == true && x.InventoryOrderID == purchaseOrderID);
                        if (purchaseOrderCollection.Count() > 0)
                        {
                            Database.InventoryOrder invOrder = purchaseOrderCollection.FirstOrDefault();
                            Database.Seller seller = context.Sellers.Where(x => x.IsActive == true && x.SellerID == invOrder.SellerID).SingleOrDefault();
                            txtMobile.Text = seller.SellerMobile;
                            txtPersonOrgName.Text = seller.SellerName;
                            txtRemainingAmt.Text =Convert.ToString(invOrder.BalanceAmount);
                            divcustomer.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void CreatePrintDiv(string billNo, string personName, decimal? totalAmt)
        {
            try
            {
                printlblBillNo.Text = billNo;
                printlblPersonName.Text = personName;
                printlblAmountPaid.Text = Convert.ToString(totalAmt);
                printlblCompanyName.Text = Helper.GetConfigValue(Constants.ConfigCompanyName);
                printlblCompanyAddress.Text = Helper.GetConfigValue(Constants.ConfigCompanyAddress);
            }
            catch
            {
                throw;
            }
        }
    }
}