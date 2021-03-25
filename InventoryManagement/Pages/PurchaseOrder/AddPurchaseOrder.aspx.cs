using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.PurchaseOrder
{
    public partial class AddPurchaseOrder : System.Web.UI.Page
    {
        #region Page Member
        InventoryManagementEntities context = new InventoryManagementEntities();
        string currentUserName = string.Empty;
        #endregion

        #region Page Method

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                {
                    BindDefault();
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnAddInventoryOrders_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptrData.Items.Count > 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        InventoryOrder itemOrder = new InventoryOrder();
                        itemOrder.AmountPaid = txtPaidAmount.Text == "" ? 0 : Convert.ToDecimal(txtPaidAmount.Text);
                        itemOrder.AmountPaidDate = txtAmountPaidDate.Text == "" ? DateTime.Now.Date : Convert.ToDateTime(txtAmountPaidDate.Text);
                        itemOrder.BillDate = txtBillDate.Text == "" ? DateTime.Now.Date : Convert.ToDateTime(txtBillDate.Text);
                        itemOrder.CreatedBy = currentUserName;
                        itemOrder.CreatedDate = DateTime.Now.Date;
                        itemOrder.CustomerTypeID = Convert.ToInt32(ddlCustomerType.SelectedValue);
                        itemOrder.IsActive = true;
                        itemOrder.IsCompleted = chkOrderComplete.Checked;
                        itemOrder.OwnBillNo = txtOwnBillNo.Text;
                        itemOrder.PurchaseBillNo = txtPurchaseBillNO.Text;
                        itemOrder.PurchaseDate = txtPurchaseDate.Text == "" ? DateTime.Now.Date : Convert.ToDateTime(txtPurchaseDate.Text);
                        itemOrder.Remarks = txtDetails.Text;
                        itemOrder.SellerID = txtSellerID.Text == "" ? 0 : Convert.ToInt32(txtSellerID.Text);
                        itemOrder.TotalOrderAmount = txtTotalOrderAmount.Text == "" ? 0 : Convert.ToDecimal(txtTotalOrderAmount.Text);
                        itemOrder.BalanceAmount = itemOrder.TotalOrderAmount - itemOrder.AmountPaid;
                        context.InventoryOrders.Add(itemOrder);
                        context.SaveChanges();

                        foreach (RepeaterItem rptrItem in rptrData.Items)
                        {
                            if (rptrItem.Visible)
                            {
                                //For bulk purchase each item detail
                                InventoryOrderDetail orderDetail = new InventoryOrderDetail();
                                orderDetail.CreatedBy = currentUserName;
                                orderDetail.CreatedDate = DateTime.Now.Date;
                                orderDetail.InventoryOrderID = itemOrder.InventoryOrderID;
                                orderDetail.InventoryTypeID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("rptrhdnItemID")).Value);
                                orderDetail.IsActive = true;
                                orderDetail.PurchaseRate = Convert.ToDecimal(((Label)rptrItem.FindControl("lblPurchaseRate")).Text);
                                orderDetail.Quantity = Convert.ToInt32(((Label)rptrItem.FindControl("lblQuantity")).Text);
                                orderDetail.SaleRate = Convert.ToDecimal(((Label)rptrItem.FindControl("lblSellingRate")).Text);
                                orderDetail.TotalItemsCost = Convert.ToDecimal(((Label)rptrItem.FindControl("lblTotalAmount")).Text);
                                orderDetail.VatNo = ((Label)rptrItem.FindControl("lblVAT")).Text;
                                context.InventoryOrderDetails.Add(orderDetail);
                                context.SaveChanges();

                                //For company inventory detail
                                var inventoryCollection = context.InventoryTypes.Where(x => x.InventoryTypeId == orderDetail.InventoryTypeID);
                                if (inventoryCollection.Count() > 0)
                                {
                                    var inventory = inventoryCollection.SingleOrDefault();
                                    var existingCount = inventory.InventoryCount == null ? 0 : inventory.InventoryCount;
                                    inventory.InventoryCount = existingCount + orderDetail.Quantity;
                                    inventory.PurchaseRate = orderDetail.PurchaseRate;
                                    inventory.SellingRate = orderDetail.SaleRate;
                                    inventory.VAT = orderDetail.VatNo;
                                    inventory.ModifiedBy = currentUserName;
                                    inventory.ModifiedDate = DateTime.Now.Date;
                                    inventory.IsActive = true;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    lblMessage.Text = "One of the Record Item Should not select proper items";
                                    lblMessage.ForeColor = Color.Red;
                                    return;
                                }
                            }
                        }

                        //Value is set from javascript of autocomplete
                        //string companyIDObj = hdnCompanyID.Value;
                        //if(! string.IsNullOrEmpty(companyIDObj))
                        //{
                        //    int sellerID = Convert.ToInt32(companyIDObj);
                        //    var sellerCollection = context.Sellers.Where(x => x.SellerID == sellerID);
                        //    if(sellerCollection.Count()>0)
                        //    {
                        //        Database.Seller seller = sellerCollection.First();
                        //        string paidAmtobj = txtPaidAmount.Text;

                        //        seller.Balance += txtPaidAmount.Text == "" ? 0 : Convert.ToInt32();
                        //    }
                        //}

                        lblMessage.Text = "Record Inserted sucessfully";
                        lblMessage.ForeColor = Color.Green;
                        ClearForm();
                        BindDefault();
                        scope.Complete();
                    }
                }
                else
                {
                    lblMessage.Text = "Atleast One Record Item Should Be There";
                    lblMessage.ForeColor = Color.Red;
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
                obj.ItemID = Convert.ToInt32(hdnItemID.Value);
                obj.ItemName = txtItemName.Text;
                obj.ItemCode = txtItemCode.Text;
                obj.PurchaseRate = Convert.ToDecimal(txtPurchasRate.Text);
                obj.Quantity = Convert.ToInt32(txtQuantity.Text);
                obj.SellingRate = Convert.ToDecimal(txtSellingRate.Text);
                obj.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);
                obj.VAT = txtVAT.Text;
                data.Add(obj);
                if (rptrData.Items.Count > 0)
                {
                    data = BindRepeater(data);
                }
                rptrData.DataSource = data;
                rptrData.DataBind();
                ClearRepeater();
                lblMessage.Text = "";
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
                    hdnItemID.Value = ((HiddenField)item.FindControl("rptrhdnItemID")).Value;
                    txtPurchasRate.Text = ((Label)item.FindControl("lblPurchaseRate")).Text;
                    txtQuantity.Text = ((Label)item.FindControl("lblQuantity")).Text;
                    txtSellingRate.Text = ((Label)item.FindControl("lblSellingRate")).Text;
                    txtTotalAmount.Text = ((Label)item.FindControl("lblTotalAmount")).Text;
                    txtVAT.Text = ((Label)item.FindControl("lblVAT")).Text;
                    txtItemCode.Text = ((Label)item.FindControl("lblItemCode")).Text;
                    txtAutoItemCode.Text = ((Label)item.FindControl("lblItemCode")).Text;
                    txtItemName.Text = ((Label)item.FindControl("lblItemName")).Text;
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
                ClearRepeater();
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
        public void ClearForm()
        {
            try
            {
                txtSellerID.Text = "";
                txtDetails.Text = "";
                txtQuantity.Text = "";
                txtTotalAmount.Text = "";
                txtPaidAmount.Text = "";
                txtPurchaseBillNO.Text = "";
                txtPurchasRate.Text = "";
                txtSellingRate.Text = "";
                txtTotalAmount.Text = "";
                chkOrderComplete.Checked = false;
                txtTotalOrderAmount.Text = "";
                txtAutoSeller.Text = "";
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
                txtPurchasRate.Text = "";
                txtSellingRate.Text = "";
                txtVAT.Text = "";
                txtTotalAmount.Text = "";
                txtQuantity.Text = "";
                txtItemCode.Text = "";
                txtItemName.Text = "";
                txtAutoItemCode.Text = "";
                txtItemCode.Text = "";
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
                var customerTypeLst = context.CustomerTypes.Where(x => x.IsActive == true).Select(x => new { x.CustomerTypeID, x.CustomerTypeName }).ToDictionary(x => x.CustomerTypeName, y => y.CustomerTypeID);
                ddlCustomerType.DataSource = customerTypeLst;
                ddlCustomerType.DataTextField = "Key";
                ddlCustomerType.DataValueField = "Value";
                ddlCustomerType.DataBind();
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
                            objNext.ItemID = Convert.ToInt32(((HiddenField)item.FindControl("rptrhdnItemID")).Value);
                            objNext.ItemName = ((Label)item.FindControl("lblItemName")).Text;
                            objNext.PurchaseRate = Convert.ToDecimal(((Label)item.FindControl("lblPurchaseRate")).Text);
                            objNext.Quantity = Convert.ToInt32(((Label)item.FindControl("lblQuantity")).Text);
                            objNext.SellingRate = Convert.ToDecimal(((Label)item.FindControl("lblSellingRate")).Text);
                            objNext.TotalAmount = Convert.ToDecimal(((Label)item.FindControl("lblTotalAmount")).Text);
                            objNext.VAT = ((Label)item.FindControl("lblVAT")).Text;
                            objNext.ItemCode = ((Label)item.FindControl("lblItemCode")).Text;
                            data.Add(objNext);
                        }
                    }
                }
                //to make edit done
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
        //        var InventoryDetail = context.InventoryTypes.Select(x => new
        //        {
        //            label = x.InventoryCode + "-" + x.InventoryTypeName,
        //            value = x.InventoryCode,
        //            InventoryCode = x.InventoryCode,
        //            InventoryTypeName = x.InventoryTypeName,
        //            PurchaseRate = x.PurchaseRate,
        //            SellingRate = x.SellingRate,
        //            VAT = x.VAT,
        //            InventoryTypeId = x.InventoryTypeId
        //        });
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        hdnInventoryDetail.Value = serializer.Serialize(InventoryDetail.ToList());

        //        var seller = context.Sellers.Select(x => new
        //        {
        //            label = x.SellerName + "-" + x.SellerMobile,
        //            value = x.SellerName,
        //            SellerName = x.SellerName,
        //            Balance = x.Balance,
        //            SellerMobile = x.SellerMobile,
        //            SellerID = x.SellerID
        //        });
        //        hdnSellerDetail.Value = serializer.Serialize(seller.ToList());
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public class rptrClass
        {
            public string ItemName { get; set; }
            public string ItemCode { get; set; }
            public int ItemID { get; set; }
            public decimal? PurchaseRate { get; set; }
            public decimal? SellingRate { get; set; }
            public int? Quantity { get; set; }
            public string VAT { get; set; }
            public decimal? TotalAmount { get; set; }
        }

        public string GetNextBillNo()
        {
            string billStartTag = Helper.GetConfigValue(Constants.ConfigPurchaseBillStartLabel);
            var saleOrderLst = context.InventoryOrders.ToList();
            if (saleOrderLst.Count() > 0)
            {
                var lastSaleOrder = saleOrderLst.OrderByDescending(x => x.InventoryOrderID).First();
                if (lastSaleOrder != null)
                    return billStartTag + lastSaleOrder.InventoryOrderID;
                else
                    return billStartTag + "01";
            }
            else
                return billStartTag + "01";
        }

        public void BindDefault()
        {
            try
            {
                txtPurchaseDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                txtAmountPaidDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                txtBillDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                txtOwnBillNo.Text = GetNextBillNo();
                BindDropDown();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<Database.Seller> GetSellerName(string searchText)
        {
            try
            {
                using (InventoryManagementEntities myContext = new InventoryManagementEntities())
                {
                    return myContext.Sellers.Where(x => x.SellerName.Contains(searchText) || x.SellerMobile.Contains(searchText)).ToList();
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

        protected void btnPopupAddSeller_Click(object sender, EventArgs e)
        {
            try
            {
                var sellerCollection = context.Sellers.Where(x => x.IsActive == true && x.SellerName == txtPopupSellerName.Text && x.SellerMobile == txtPopupSellerMobile.Text);
                if (sellerCollection.Count() == 0)
                {
                    Database.Seller seller = new Database.Seller();
                    seller.CreatedBy = currentUserName;
                    seller.CreatedDate = DateTime.Now.Date;
                    seller.SellerLandline = txtPopupSellerLandline.Text;
                    seller.SellerMobile = txtPopupSellerMobile.Text;
                    seller.SellerName = txtPopupSellerName.Text;
                    seller.SellerEmail = txtPopupSellerEmail.Text;
                    seller.IsActive = true;
                    context.Sellers.Add(seller);
                    context.SaveChanges();

                    txtAutoSeller.Text = seller.SellerName;
                    txtSellerID.Text = Convert.ToString(seller.SellerID);

                    lblPopupSellerResult.Text = "Seller Added Successfully";
                    lblPopupSellerResult.ForeColor = Color.Green;
                    ClearPopup();
                    divAddSellerScript.Visible = false;
                }
                else
                {
                    lblPopupSellerResult.Text = "Seller Already exist with same name or Mobile";
                    lblPopupSellerResult.ForeColor = Color.Red;
                    divAddSellerScript.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblPopupSellerResult.Text = "Something went Wrong kindly check log";
                lblPopupSellerResult.ForeColor = Color.Red;
                divAddSellerScript.Visible = true;
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
                    Inventory.SellingRate = string.IsNullOrEmpty(txtSellingRate.Text) ? 0 : Convert.ToDecimal(txtSellingRate.Text);
                    Inventory.UnitName = txtPopupUnit.Text;
                    Inventory.VAT = txtVAT.Text;
                    context.InventoryTypes.Add(Inventory);
                    context.SaveChanges();

                    ClearPopup();
                    txtItemCode.Text = Inventory.InventoryCode;
                    txtAutoItemCode.Text = Inventory.InventoryCode;
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
                txtPopupInvDesc.Text = "";
                txtPopupInventoryCode.Text = "";
                txtPopupInventoryTypeName.Text = "";
                txtPopupLowCount.Text = "";
                txtPopupPurchaseRate.Text = "";
                txtPopupRagNo.Text = "";
                txtPopupSellingRate.Text = "";
                txtPopupUnit.Text = "";
                txtPopupVat.Text = "";
                lblPopupInvResult.Text = "";

                txtPopupSellerEmail.Text = "";
                txtPopupSellerLandline.Text = "";
                txtPopupSellerMobile.Text = "";
                txtPopupSellerName.Text = "";
            }
            catch
            {

            }
        }
    }
}