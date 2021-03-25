using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Transactions;
using System.IO;
using System.Web.Script.Services;

namespace InventoryManagement.Pages.SaleOrder
{
    public partial class EditSaleOrder : System.Web.UI.Page
    {
        InventoryManagementEntities context = new InventoryManagementEntities();
        string currentUserName = string.Empty;

        #region Page Method

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                {
                    BindDropDown();
                    FillForm();
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            try
            {
                List<rptrClass> data = new List<rptrClass>();

                rptrClass obj = new rptrClass();
                obj.ItemID = hdnItemID.Value;
                obj.ItemName = txtItemName.Text;
                obj.ItemCode = txtItemCode.Text;
                obj.Quantity = txtQuantity.Text;
                obj.SellingRate = txtSellingRate.Text;
                obj.TotalAmount = txtTotalAmount.Text;
                obj.VAT = txtVAT.Text;
                data.Add(obj);

                if (rptrData.Items.Count > 0)
                {
                    data = BindRepeater(data);
                }
                rptrData.DataSource = data;
                rptrData.DataBind();
                ClearRepeater();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void rptrData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    RepeaterItem item = e.Item;
                    txtItemName.Text = ((Label)item.FindControl("lblItemName")).Text;
                    txtItemCode.Text = ((Label)item.FindControl("lblItemCode")).Text;
                    txtAutoItemCode.Text = ((Label)item.FindControl("lblItemCode")).Text;
                    txtQuantity.Text = ((Label)item.FindControl("lblQuantity")).Text;
                    txtSellingRate.Text = ((Label)item.FindControl("lblSellingRate")).Text;
                    txtTotalAmount.Text = ((Label)item.FindControl("lblTotalAmount")).Text;
                    txtVAT.Text = ((Label)item.FindControl("lblVat")).Text;
                    hdnItemIndex.Value = Convert.ToString(e.Item.ItemIndex);
                }

                if (e.CommandName == "Delete")
                {
                    e.Item.Visible = false;
                    List<rptrClass> data = BindRepeater();
                    rptrData.DataSource = data;
                    rptrData.DataBind();
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnUpdateSaleOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //Message is set from sale order
                UpdateSaleOrder();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        #endregion

        #region User Method

        public void FillForm()
        {
            try
            {
                string SaleOrderIDObj = Request.QueryString[Constants.querySaleOrderID];

                if (!string.IsNullOrEmpty(SaleOrderIDObj))
                {
                    int SaleOrderID = Convert.ToInt32(SaleOrderIDObj);
                    if (SaleOrderID > 0)
                    {
                        var SaleCollection = context.SalesOrders.Where(x => x.IsActive == true && x.SaleOrderID == SaleOrderID);
                        if (SaleCollection.Count() > 0)
                        {
                            Database.SalesOrder saleOrder = SaleCollection.FirstOrDefault();
                            txtBalance.Text = Convert.ToString(saleOrder.RemainingAmount);
                            txtBillNo.Text = saleOrder.BillNo;
                            txtClass.Text = saleOrder.ClassName;
                            txtCustomerID.Text = Convert.ToString(saleOrder.CustomerID);
                            txtPaidAmount.Text = Convert.ToString(saleOrder.PaidAmount);
                            txtRemarks.Text = saleOrder.Remarks;
                            txtSellingDate.Text = saleOrder.SaleOrderDate == null ? "" : saleOrder.SaleOrderDate.Value.ToString(Constants.DateFormatDatePicker);
                            txtTotalOrderAmount.Text = Convert.ToString(saleOrder.TotalCost);
                            ddlCustomerType.SelectedValue = Convert.ToString(saleOrder.CustomerTypeID);
                            chkIsComplete.Checked = saleOrder.IsCompleted == null ? false : Convert.ToBoolean(saleOrder.IsCompleted);
                            chkIsMakingRequired.Checked = saleOrder.IsMakingRequired == null ? false : Convert.ToBoolean(saleOrder.IsMakingRequired);

                            int? customerID = saleOrder.CustomerID;
                            if (customerID != null)
                            {
                                var customerCollection = context.Customers.Where(x => x.CustomerID == customerID);
                                if (customerCollection.Count() > 0)
                                {
                                    Database.Customer customer = customerCollection.FirstOrDefault();
                                    txtAutoCustomerName.Text = customer.CustomerName;
                                }
                            }

                            if (saleOrder.IsMakingRequired == true)
                            {
                                var devWorkCollection = context.DevelopmentWorks.Where(x => x.IsActive == true && x.SaleOrderID == SaleOrderID);
                                if (devWorkCollection.Count() > 0)
                                {
                                    Database.DevelopmentWork devWork = devWorkCollection.FirstOrDefault();
                                    txtAutoMakingInventoryType.Text = Convert.ToString(devWork.InventoryTypeID);
                                    txtBottomMeasurement.Text = devWork.BottomMeasurement;
                                    txtConfirmDate.Text = devWork.ConfirmDate == null ? "" : devWork.ConfirmDate.Value.ToString(Constants.DateFormatDatePicker);
                                    txtDeliveryDate.Text = devWork.DeliveryDate == null ? "" : devWork.DeliveryDate.Value.ToString(Constants.DateFormatDatePicker);
                                    txtMakingCost.Text = Convert.ToString(devWork.MakingCost);
                                    txtMakingInventoryType.Text = Convert.ToString(devWork.InventoryTypeID);
                                    txtTopMeasurement.Text = devWork.TopMeasurement;
                                    ddlWorkType.SelectedValue = Convert.ToString(devWork.WorkTypeID);

                                    int? invTypeID = devWork.InventoryTypeID;
                                    if (invTypeID != null)
                                    {
                                        var invCollection = context.InventoryTypes.Where(x => x.InventoryTypeId == invTypeID);
                                        if (invCollection.Count() > 0)
                                        {
                                            Database.InventoryType inventory = invCollection.FirstOrDefault();
                                            txtAutoMakingInventoryType.Text = inventory.InventoryTypeName;
                                        }
                                    }
                                }
                                else
                                {
                                    //Nothing to Do
                                }
                            }

                            var saleDetailCollection = context.SaleOrderDetails.Where(x => x.IsActive == true && x.SaleOrderID == SaleOrderID).AsEnumerable();
                            var data = from saleDetail in saleDetailCollection
                                       join inv in context.InventoryTypes.Where(x => x.IsActive == true).AsEnumerable() on saleDetail.InventoryTypeID equals inv.InventoryTypeId
                                       select new rptrClass
                                       {
                                           ItemCode = inv.InventoryCode,
                                           ItemID = inv.InventoryTypeId.ToString(),
                                           ItemName = inv.InventoryTypeName,
                                           Quantity = saleDetail.Quantity.ToString(),
                                           SellingRate = saleDetail.SellingRate.ToString(),
                                           TotalAmount = saleDetail.TotalAmount.ToString(),
                                           VAT = saleDetail.VAT,
                                           SaleOrderDetailID = saleDetail.SaleOrderDetailID
                                       };
                            rptrData.DataSource = data.ToList();
                            rptrData.DataBind();
                            hdnSaleOrderID.Value = Convert.ToString(SaleOrderID);
                        }
                        else
                        {
                            lblMessage.Text = "Customer Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Customer ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Customer ID Not in right format";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch
            {
                throw;
            }
        }

        public void BindDropDown()
        {
            try
            {
                var customerTypeList = context.CustomerTypes.Where(x => x.IsActive == true).Select(x => new { x.CustomerTypeID, x.CustomerTypeName })
                                                            .ToDictionary(x => x.CustomerTypeName, y => y.CustomerTypeID);
                ddlCustomerType.DataSource = customerTypeList;
                ddlCustomerType.DataTextField = "Key";
                ddlCustomerType.DataValueField = "Value";
                ddlCustomerType.DataBind();

                var workTypeList = context.WorkTypes.Where(x => x.IsActive == true).Select(x => new { x.WorkTypeID, x.WorkTypeName })
                                                    .ToDictionary(x => x.WorkTypeName, y => y.WorkTypeID);
                ddlWorkType.DataSource = workTypeList;
                ddlWorkType.DataTextField = "Key";
                ddlWorkType.DataValueField = "Value";
                ddlWorkType.DataBind();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateSaleOrder()
        {
            try
            {
                if (rptrData.Items.Count != 0 || chkIsMakingRequired.Checked)
                {
                    string saleOrderIDObj = hdnSaleOrderID.Value;
                    if (!string.IsNullOrEmpty(saleOrderIDObj))
                    {
                        int saleID = Convert.ToInt32(saleOrderIDObj);
                        var saleCollection = context.SalesOrders.Where(x => x.IsActive == true && x.SaleOrderID == saleID);
                        if (saleCollection.Count() > 0)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                Database.SalesOrder saleOrder = saleCollection.FirstOrDefault();
                                saleOrder.BillNo = txtBillNo.Text;
                                saleOrder.ClassName = txtClass.Text;
                                saleOrder.ModifiedBy = currentUserName;
                                saleOrder.ModifiedDate = DateTime.Now;
                                saleOrder.CustomerID = Convert.ToInt32(txtCustomerID.Text);
                                saleOrder.CustomerTypeID = Convert.ToInt32(ddlCustomerType.SelectedValue);
                                saleOrder.IsActive = true;
                                saleOrder.IsCompleted = chkIsComplete.Checked;
                                saleOrder.IsMakingRequired = chkIsMakingRequired.Checked;
                                saleOrder.PaidAmount = txtPaidAmount.Text == "" ? 0 : Convert.ToDecimal(txtPaidAmount.Text);
                                //Profit is set at the end of all items
                                //saleOrder.Profit=
                                saleOrder.Remarks = txtRemarks.Text;
                                saleOrder.SaleOrderDate = Convert.ToDateTime(txtSellingDate.Text);
                                saleOrder.TotalCost = txtTotalOrderAmount.Text == "" ? 0 : Convert.ToDecimal(txtTotalOrderAmount.Text);

                                //Below due to total cost above set
                                saleOrder.RemainingAmount = saleOrder.TotalCost - saleOrder.PaidAmount;
                                context.SaveChanges();

                                if (chkIsMakingRequired.Checked)
                                {
                                    string devWorkIDObj = hdnDevWorkID.Value;
                                    if (!string.IsNullOrEmpty(devWorkIDObj))
                                    {
                                        int devWorkID = Convert.ToInt32(devWorkIDObj);
                                        var devWorkCollection = context.DevelopmentWorks.Where(x => x.IsActive == true && x.DevelopmentWorkID == devWorkID);
                                        if (devWorkCollection.Count() > 0)
                                        {
                                            //Developement work detail
                                            DevelopmentWork dwork = devWorkCollection.FirstOrDefault();
                                            dwork.BillNo = saleOrder.BillNo;
                                            dwork.BottomMeasurement = txtBottomMeasurement.Text;
                                            if (txtConfirmDate.Text != "")
                                                dwork.ConfirmDate = Convert.ToDateTime(txtConfirmDate.Text);
                                            dwork.ModifiedBy = currentUserName;
                                            dwork.ModifiedDate = DateTime.Now.Date;
                                            if (txtDeliveryDate.Text != "")
                                                dwork.DeliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);
                                            if (fileuploadDesign.FileName != "")
                                            {
                                                string fileNamePath = ConfigurationManager.AppSettings[Constants.ConfigDesignPhotoPath] + "\\" + fileuploadDesign.FileName;
                                                fileuploadDesign.SaveAs(fileNamePath);
                                                dwork.DesignPhotoUrl = ConfigurationManager.AppSettings[Constants.ConfigDesignPhotoURL] + "\\" + fileuploadDesign.FileName;
                                            }
                                            dwork.InventoryTypeID = Convert.ToInt32(txtMakingInventoryType.Text);
                                            dwork.IsActive = true;
                                            dwork.MakingCost = txtMakingCost.Text == "" ? 0 : Convert.ToDecimal(txtMakingCost.Text);
                                            dwork.SaleOrderID = saleOrder.SaleOrderID;
                                            dwork.TopMeasurement = txtTopMeasurement.Text;
                                            dwork.WorkTypeID = Convert.ToInt32(ddlWorkType.SelectedValue);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                                decimal? profit = 0;

                                List<rptrClass> printLst = new List<rptrClass>();
                                if (rptrData.Items.Count > 0)
                                {
                                    foreach (RepeaterItem rptrItem in rptrData.Items)
                                    {
                                        if (rptrItem.Visible)
                                        {
                                            int saleOrderDetailID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("hdnSaleOrderDetailID")).Value);

                                            var saleOrderDetailCollection = context.SaleOrderDetails.Where(x => x.IsActive == true && x.SaleOrderDetailID == saleOrderDetailID);

                                            if (saleOrderDetailCollection.Count() > 0)
                                            {
                                                Database.SaleOrderDetail saleOrderDetail = saleOrderDetailCollection.FirstOrDefault();
                                                var oldQuantity = saleOrderDetail.Quantity;

                                                saleOrderDetail.ModifiedBy = currentUserName;
                                                saleOrderDetail.ModifiedDate = DateTime.Now;
                                                saleOrderDetail.InventoryTypeID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("hdnItemID")).Value);
                                                saleOrderDetail.IsActive = true;
                                                saleOrderDetail.Quantity = Convert.ToInt32(((Label)rptrItem.FindControl("lblQuantity")).Text);
                                                saleOrderDetail.SaleOrderID = saleOrder.SaleOrderID;
                                                saleOrderDetail.SellingRate = Convert.ToDecimal(((Label)rptrItem.FindControl("lblSellingRate")).Text);
                                                saleOrderDetail.TotalAmount = Convert.ToDecimal(((Label)rptrItem.FindControl("lblTotalAmount")).Text);
                                                saleOrderDetail.VAT = ((Label)rptrItem.FindControl("lblVat")).Text;
                                                context.SaveChanges();

                                                Database.InventoryType currentInventory = context.InventoryTypes.Where(x => x.IsActive == true && x.InventoryTypeId == saleOrderDetail.InventoryTypeID).SingleOrDefault();
                                                if (currentInventory != null)
                                                {
                                                    var invCount = currentInventory.InventoryCount == null ? 0 : currentInventory.InventoryCount;
                                                    currentInventory.InventoryCount = invCount + (oldQuantity - saleOrderDetail.Quantity);
                                                    context.SaveChanges();

                                                    decimal? pRate = currentInventory.PurchaseRate == null ? 0 : currentInventory.PurchaseRate;
                                                    decimal? sRate = saleOrderDetail.SellingRate == null ? 0 : saleOrderDetail.SellingRate;
                                                    profit = profit + (sRate - pRate);
                                                }

                                                rptrClass obj = new rptrClass();
                                                obj.ItemName = ((Label)rptrItem.FindControl("lblItemName")).Text;
                                                obj.Quantity = Convert.ToString(saleOrderDetail.Quantity);
                                                obj.SellingRate = Convert.ToString(saleOrderDetail.SellingRate);
                                                obj.TotalAmount = Convert.ToString(saleOrderDetail.TotalAmount);
                                                obj.VAT = saleOrderDetail.VAT;
                                                printLst.Add(obj);
                                            }
                                        }
                                        else
                                        {
                                            int saleOrderDetailID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("hdnSaleOrderDetailID")).Value);

                                            var saleOrderCollection = context.SaleOrderDetails.Where(x => x.IsActive == true && x.SaleOrderDetailID == saleOrderDetailID);

                                            if (saleOrderCollection.Count() > 0)
                                            {
                                                Database.SaleOrderDetail saleOrderDetail = saleOrderCollection.FirstOrDefault();
                                                var oldQuantity = saleOrderDetail.Quantity;

                                                saleOrderDetail.ModifiedBy = currentUserName;
                                                saleOrderDetail.ModifiedDate = DateTime.Now;
                                                saleOrderDetail.IsActive = false;
                                                context.SaveChanges();

                                                Database.InventoryType currentInventory = context.InventoryTypes.Where(x => x.IsActive == true && x.InventoryTypeId == saleOrderDetail.InventoryTypeID).SingleOrDefault();
                                                if (currentInventory != null)
                                                {
                                                    var invCount = currentInventory.InventoryCount == null ? 0 : currentInventory.InventoryCount;
                                                    currentInventory.InventoryCount = invCount + oldQuantity;
                                                    context.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }

                                saleOrder.Profit = profit;
                                context.SaveChanges();

                                lblMessage.Text = "Record updated sucessfully of BILL NO " + saleOrder.BillNo;
                                lblMessage.ForeColor = Color.Green;
                                CreatePrintDiv(saleOrder.BillNo, txtAutoCustomerName.Text, saleOrder.TotalCost, printLst);
                                txtSellingDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                                scope.Complete();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Sale Order ID Not Found ";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Sale Order ID Not in proper format ";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Sale Order is not proper ";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch
            {
                throw;
            }
        }

        public void CreatePrintDiv(string billNo, string personName, decimal? totalAmt, List<rptrClass> printLst)
        {
            try
            {
                printlblBillNo.Text = billNo;
                printlblPersonName.Text = personName;
                printlblTotalAmount.Text = Convert.ToString(totalAmt);
                printlblCompanyName.Text = Helper.GetConfigValue(Constants.ConfigCompanyName);
                printlblCompanyAddress.Text = Helper.GetConfigValue(Constants.ConfigCompanyAddress);
                rptrPrint.DataSource = printLst;
                rptrPrint.DataBind();
            }
            catch
            {
                throw;
            }
        }

        public void ClearRepeater()
        {
            try
            {
                txtSellingRate.Text = "";
                txtTotalAmount.Text = "";
                txtQuantity.Text = "";
                txtVAT.Text = "";
                txtItemName.Text = "";
                txtItemCode.Text = "";
                txtAutoItemCode.Text = "";
                hdnItemIndex.Value = "";
                hdnItemID.Value = "";
            }
            catch
            {
                throw;
            }
        }

        public List<rptrClass> BindRepeater(List<rptrClass> data = null)
        {
            try
            {
                if (data == null)
                    data = new List<rptrClass>();
                int editItemIndex = hdnItemIndex.Value == "" ? -1 : Convert.ToInt32(hdnItemIndex.Value);
                foreach (RepeaterItem item in rptrData.Items)
                {
                    //for deleting of repeater row
                    if (item.Visible)
                    {
                        //To check edit button press so not add record
                        if (item.ItemIndex != editItemIndex)
                        {
                            rptrClass objNext = new rptrClass();
                            objNext.ItemID = ((HiddenField)item.FindControl("hdnItemID")).Value;
                            objNext.ItemName = ((Label)item.FindControl("lblItemName")).Text;
                            objNext.Quantity = ((Label)item.FindControl("lblQuantity")).Text;
                            objNext.SellingRate = ((Label)item.FindControl("lblSellingRate")).Text;
                            objNext.TotalAmount = ((Label)item.FindControl("lblTotalAmount")).Text;
                            objNext.VAT = ((Label)item.FindControl("lblVat")).Text;
                            objNext.ItemCode = ((Label)item.FindControl("lblItemCode")).Text;
                            objNext.SaleOrderDetailID = Convert.ToInt32(((HiddenField)item.FindControl("hdnSaleOrderDetailID")).Value);
                            data.Add(objNext);
                        }
                    }
                }
                hdnItemIndex.Value = "-1";
                return data;
            }
            catch
            {
                throw;
            }
        }

        public class rptrClass
        {
            public string ItemName { get; set; }
            public string ItemID { get; set; }
            public string SellingRate { get; set; }
            public string Quantity { get; set; }
            public string TotalAmount { get; set; }
            public string VAT { get; set; }
            public string ItemCode { get; set; }
            public int SaleOrderDetailID { get; set; }
        }

        #endregion

        protected void btnPopupAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                var customerCollection = context.Customers.Where(x => x.CustomerName == txtPopupCustomerName.Text && x.CustomerMobile == txtPopupCustomerMobile.Text && x.IsActive == true);
                if (customerCollection.Count() == 0)
                {
                    Database.Customer customer = new Database.Customer();
                    customer.CreatedBy = currentUserName;
                    customer.CreatedDate = DateTime.Now.Date;
                    customer.CustomerMobile = txtPopupCustomerMobile.Text;
                    customer.CustomerName = txtPopupCustomerName.Text;
                    customer.CustomerEmail = txtPopupEmail.Text;
                    customer.IsActive = true;
                    context.Customers.Add(customer);
                    context.SaveChanges();

                    txtPopupCustomerMobile.Text = "";
                    txtPopupCustomerName.Text = "";
                    txtPopupEmail.Text = "";
                    ClearPopup();

                    txtAutoCustomerName.Text = customer.CustomerName;
                    txtCustomerID.Text = Convert.ToString(customer.CustomerID);

                    divAddCustomerScript.Visible = false;
                }
                else
                {
                    lblPopupCustomerResult.Text = "Customer Already exist with same name and Mobile";
                    lblPopupCustomerResult.ForeColor = Color.Red;
                    divAddCustomerScript.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblPopupCustomerResult.Text = "Something went Wrong kindly check log";
                lblPopupCustomerResult.ForeColor = Color.Red;
                divAddCustomerScript.Visible = true;
            }
        }

        protected void btnPopupAddInventory_Click(object sender, EventArgs e)
        {
            try
            {
                string invName = txtPopupInventoryTypeName.Text;
                string invCode = txtPopupInventoryCode.Text;
                var invType = context.InventoryTypes.Where(x => x.InventoryCode == invCode || x.InventoryTypeName == invName);
                if (invType.Count() == 0)
                {
                    Database.InventoryType Inventory = new Database.InventoryType();
                    Inventory.CreatedBy = currentUserName;
                    Inventory.CreatedDate = DateTime.Now.Date;
                    Inventory.FastMoving = chkPopupFastMoving.Checked;
                    Inventory.InventoryCode = txtPopupInventoryCode.Text;
                    Inventory.InventoryDescription = txtPopupInvDesc.Text;
                    Inventory.InventoryTypeName = txtPopupInventoryTypeName.Text;
                    Inventory.IsActive = true;
                    Inventory.LowStockCount = string.IsNullOrEmpty(txtPopupLowCount.Text) ? 0 : Convert.ToInt32(txtPopupLowCount.Text);
                    Inventory.PurchaseRate = string.IsNullOrEmpty(txtPopupPurchaseRate.Text) ? 0 : Convert.ToDecimal(txtPopupPurchaseRate.Text);
                    Inventory.RagNo = txtPopupRagNo.Text;
                    Inventory.SellingRate = string.IsNullOrEmpty(txtPopupSellingRate.Text) ? 0 : Convert.ToDecimal(txtPopupSellingRate.Text);
                    Inventory.UnitName = txtPopupUnit.Text;
                    Inventory.VAT = txtPopupVat.Text;
                    context.InventoryTypes.Add(Inventory);
                    context.SaveChanges();

                    ClearPopup();
                    txtAutoItemCode.Text = Inventory.InventoryCode;
                    txtItemCode.Text = Inventory.InventoryCode;
                    txtItemName.Text = Inventory.InventoryTypeName;
                    txtSellingRate.Text = Convert.ToString(Inventory.SellingRate);
                    txtVAT.Text = Inventory.VAT;
                    hdnItemID.Value = Convert.ToString(Inventory.InventoryTypeId);

                    divAddInventoryScript.Visible = false;
                }
                else
                {
                    lblPopupInvResult.Text = "Inventory Name or code already exist kindly choose another one";
                    lblPopupInvResult.ForeColor = Color.Red;
                    divAddInventoryScript.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblPopupInvResult.Text = "Something went Wrong kindly check log";
                lblPopupInvResult.ForeColor = Color.Red;
                divAddInventoryScript.Visible = true;
            }
        }

        public void ClearPopup()
        {
            try
            {
                txtPopupCustomerMobile.Text = "";
                txtPopupCustomerName.Text = "";
                txtPopupEmail.Text = "";
                txtPopupInvDesc.Text = "";
                txtPopupInventoryCode.Text = "";
                txtPopupInventoryTypeName.Text = "";
                txtPopupLowCount.Text = "";
                txtPopupPurchaseRate.Text = "";
                txtPopupRagNo.Text = "";
                txtPopupSellingRate.Text = "";
                txtPopupUnit.Text = "";
                txtPopupVat.Text = "";
                lblPopupCustomerResult.Text = "";
                lblPopupInvResult.Text = "";
            }
            catch
            {

            }
        }
    }
}