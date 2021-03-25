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

namespace InventoryManagement.Main
{
    public partial class SaleOrder : System.Web.UI.Page
    {
        InventoryManagementEntities context = new InventoryManagementEntities();
        public static InventoryManagementEntities webMethodContext = new InventoryManagementEntities();
        string currentUserName = string.Empty;

        #region Page Method

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                {
                    txtSellingDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                    BindDropDown();
                    txtBillNo.Text = GetNextBillNo();
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

        protected void btnAddSaleOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //Message is set from sale order
                PlaceSaleOrder();
                txtBillNo.Text = GetNextBillNo();
                txtSellingDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
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
                    //RepeaterItem item = e.Item;
                    //string tempAmt = ((Label)item.FindControl("lblTotalAmount")).Text;
                    //decimal rowAmt = tempAmt == "" ? 0 : Convert.ToDecimal(tempAmt);
                    //decimal totalOrderAmt = txtTotalOrderAmount.Text == "" ? 0 : Convert.ToDecimal(txtTotalOrderAmount.Text);
                    //txtTotalOrderAmount.Text = (totalOrderAmt - rowAmt).ToString();
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
                txtSellingDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
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

                //var iventoryTypeList = context.InventoryTypes.Where(x => x.IsActive == true).Select(x => new { x.InventoryTypeId, x.InventoryTypeName })
                //                                             .ToDictionary(x => x.InventoryTypeName, y => y.InventoryTypeId);
                //ddlInventoryType.DataTextField = "Key";
                //ddlInventoryType.DataValueField = "Value";
                //ddlInventoryType.DataSource = iventoryTypeList.ToList();
                //ddlInventoryType.DataBind();
                //ddlInventoryType.Items.Insert(0, new ListItem("Select", "0"));

                //ddlInventoryMaking.DataTextField = "Key";
                //ddlInventoryMaking.DataValueField = "Value";
                //ddlInventoryMaking.DataSource = iventoryTypeList;
                //ddlInventoryMaking.DataBind();
                //ddlInventoryMaking.Items.Insert(0, new ListItem("Select", "0"));

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

        public void PlaceSaleOrder()
        {
            try
            {
                if (rptrData.Items.Count != 0 || chkIsMakingRequired.Checked)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Sale Order
                        Database.SalesOrder saleOrder = new Database.SalesOrder();
                        saleOrder.BillNo = txtBillNo.Text;
                        saleOrder.ClassName = txtClass.Text;
                        saleOrder.CreatedBy = currentUserName;
                        saleOrder.CreatedDate = DateTime.Now;
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
                        context.SalesOrders.Add(saleOrder);
                        context.SaveChanges();
                        
                        if (chkIsMakingRequired.Checked)
                        {
                            //Developement work detail
                            DevelopmentWork dwork = new DevelopmentWork();
                            dwork.BillNo = saleOrder.BillNo;
                            dwork.BottomMeasurement = txtBottomMeasurement.Text;
                            if (txtConfirmDate.Text != "")
                                dwork.ConfirmDate = Convert.ToDateTime(txtConfirmDate.Text);
                            dwork.CreatedBy = currentUserName;
                            dwork.CreatedDate = DateTime.Now;
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
                            context.DevelopmentWorks.Add(dwork);
                            context.SaveChanges();
                        }

                        decimal? profit = 0;
                        
                        List<rptrClass> printLst = new List<rptrClass>();
                        if (rptrData.Items.Count > 0)
                        {
                            foreach (RepeaterItem rptrItem in rptrData.Items)
                            {
                                if (rptrItem.Visible)
                                {
                                    SaleOrderDetail saleOrderDetail = new SaleOrderDetail();
                                    saleOrderDetail.CreatedBy = currentUserName;
                                    saleOrderDetail.CreatedDate = DateTime.Now;
                                    saleOrderDetail.InventoryTypeID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("hdnItemID")).Value);
                                    saleOrderDetail.IsActive = true;
                                    saleOrderDetail.Quantity = Convert.ToInt32(((Label)rptrItem.FindControl("lblQuantity")).Text);
                                    saleOrderDetail.SaleOrderID = saleOrder.SaleOrderID;
                                    saleOrderDetail.SellingRate = Convert.ToDecimal(((Label)rptrItem.FindControl("lblSellingRate")).Text);
                                    saleOrderDetail.TotalAmount = Convert.ToDecimal(((Label)rptrItem.FindControl("lblTotalAmount")).Text);
                                    saleOrderDetail.VAT = ((Label)rptrItem.FindControl("lblVat")).Text;
                                    context.SaleOrderDetails.Add(saleOrderDetail);
                                    context.SaveChanges();

                                    Database.InventoryType currentInventory = context.InventoryTypes.Where(x => x.IsActive == true && x.InventoryTypeId == saleOrderDetail.InventoryTypeID).SingleOrDefault();
                                    if (currentInventory != null)
                                    {
                                        var invCount = currentInventory.InventoryCount == null ? 0 : currentInventory.InventoryCount;
                                        currentInventory.InventoryCount = invCount - saleOrderDetail.Quantity;

                                        decimal? pRate = currentInventory.PurchaseRate == null ? 0 : currentInventory.PurchaseRate;
                                        decimal? sRate = saleOrderDetail.SellingRate == null ? 0 : saleOrderDetail.SellingRate;
                                        profit = profit + (sRate - pRate);
                                        context.SaveChanges();
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
                        }

                        saleOrder.Profit = profit;
                        context.SaveChanges();

                        lblMessage.Text = "Record Inserted sucessfully with BILL NO " + saleOrder.BillNo;
                        lblMessage.ForeColor = Color.Green;
                        CreatePrintDiv(saleOrder.BillNo, txtAutoCustomerName.Text, saleOrder.TotalCost, printLst);
                        ClearForm();
                        txtSellingDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                        //BindInvRatePrevBalance();
                        scope.Complete();
                    }
                }
                else
                {
                    lblMessage.Text = "Atleast One Record Should be inserted ";
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

        public void ClearForm()
        {
            try
            {
                txtClass.Text = "";
                txtBottomMeasurement.Text = "";
                txtConfirmDate.Text = "";
                txtDeliveryDate.Text = "";
                txtMakingCost.Text = "";
                txtPaidAmount.Text = "";
                txtAutoCustomerName.Text = "";
                txtQuantity.Text = "";
                txtRemarks.Text = "";
                txtSellingDate.Text = "";
                txtSellingRate.Text = "";
                txtTopMeasurement.Text = "";
                txtTotalAmount.Text = "";
                txtTotalOrderAmount.Text = "";
                txtAutoMakingInventoryType.Text = "";
                txtCustomerID.Text = "";
                txtMakingInventoryType.Text = "";

                txtAutoItemCode.Text = "";
                txtItemCode.Text = "";
                txtItemName.Text = "";
                txtQuantity.Text = "";
                txtTotalAmount.Text = "";
                txtVAT.Text = "";
                txtSellingRate.Text = "";

                chkIsMakingRequired.Checked = false;
                chkIsComplete.Checked = false;
                rptrData.DataSource = null;
                rptrData.DataBind();
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

        //public void BindInvRatePrevBalance()
        //{
        //    try
        //    {
        //        var allInv = context.InventoryTypes.Select(x => new
        //             {
        //                 InventoryTypeId = x.InventoryTypeId,
        //                 SellingRate = x.SellingRate,
        //                 VAT = x.VAT
        //             });
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        hdnInvRate.Value = serializer.Serialize(allInv.ToList());

        //        var allCustDetail = context.SalesOrders.Where(x => x.IsCompleted == false).GroupBy(x => x.CustomerID).Select(x => new
        //                                    {
        //                                        Mobile = x.Key,
        //                                        RemainingAmount = x.Sum(k => k.RemainingAmount)
        //                                    });
        //        hdnPrevBalance.Value = serializer.Serialize(allCustDetail.ToList());
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public string GetNextBillNo()
        {
            string billStartTag = Helper.GetConfigValue(Constants.ConfigSellingBillStartLabel);
            var saleOrderLst = context.SalesOrders.ToList();
            if (saleOrderLst.Count() > 0)
            {
                var lastSaleOrder = saleOrderLst.OrderByDescending(x => x.SaleOrderID).First();
                if (lastSaleOrder != null)
                    return billStartTag + lastSaleOrder.SaleOrderID;
                else
                    return billStartTag + "01";
            }
            else
                return billStartTag + "01";
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
        }

        #endregion

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<Customer> GetCustomerName(string searchText)
        {
            try
            {
                using (InventoryManagementEntities myContext = new InventoryManagementEntities())
                {
                    var customers = myContext.Customers.Where(x => x.IsActive == true && (x.CustomerName.Contains(searchText) || x.CustomerMobile.Contains(searchText))).ToList();
                    return customers;
                    //var saleOrders=myContext.SalesOrders.Where(x=>x.IsActive==true && x.IsCompleted==false).AsEnumerable();

                    //var customerDetailCollection = from customer in customers
                    //                               join saleOrder in saleOrders on customer.CustomerID equals saleOrder.CustomerID
                    //                               select new customerDetail
                    //                               {
                    //                                  CustomerMobile= customer.CustomerMobile,
                    //                                  CustomerName= customer.CustomerName,
                    //                                  RemainingAmount= saleOrder.RemainingAmount,
                    //                                  CustomerID= saleOrder.CustomerID
                    //                               };


                    // return myContext.Customers.Where(x => x.CustomerName.Contains(searchText)).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<Database.InventoryType> GetItemDetail(string searchText)
        {
            try
            {
                using (InventoryManagementEntities myContext = new InventoryManagementEntities())
                {
                    return myContext.InventoryTypes.Where(x => x.InventoryTypeName.Contains(searchText) || x.InventoryCode.Contains(searchText)).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object GetCustomerBalance(int CustomerID)
        {
            try
            {
                var collection = webMethodContext.SalesOrders.Where(x => x.IsActive == true && x.IsCompleted == true && x.CustomerID == CustomerID).GroupBy(x => x.CustomerID)
                                            .Select(x => new
                                            {
                                                CustomerID = x.Key,
                                                Balance = x.Sum(y => y.RemainingAmount)
                                            });
                return collection;
            }
            catch
            {
                return null;
            }
        }

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
                    Inventory.LowStockCount =string.IsNullOrEmpty(txtPopupLowCount.Text)?0:Convert.ToInt32(txtPopupLowCount.Text);
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