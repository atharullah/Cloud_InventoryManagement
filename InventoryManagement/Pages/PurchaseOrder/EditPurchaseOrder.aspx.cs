using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Inventory
{
    public partial class EditPurchaseOrder : System.Web.UI.Page
    {
        #region Page Variable
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

        protected void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            try
            {
                if (rptrData.Items.Count > 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        string PurchaseOrderIDObj = Request.QueryString[Constants.queryPurchaseOrderID];

                        if (!string.IsNullOrEmpty(PurchaseOrderIDObj))
                        {
                            int PurchaseOrderID = Convert.ToInt32(PurchaseOrderIDObj);
                            if (PurchaseOrderID > 0)
                            {
                                var PurchaseOrderCollection = context.InventoryOrders.Where(x => x.InventoryOrderID == PurchaseOrderID);
                                if (PurchaseOrderCollection.Count() > 0)
                                {
                                    InventoryOrder itemOrder = AddItemOrder(PurchaseOrderCollection.FirstOrDefault());

                                    foreach (RepeaterItem rptrItem in rptrData.Items)
                                    {
                                        if (rptrItem.Visible)
                                        {
                                            //For bulk purchase each item detail
                                            int orderDetailID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("rptrhdnOrderDetailID")).Value);
                                            if (orderDetailID > 0)
                                            {
                                                var invOrderDetailCollection = context.InventoryOrderDetails.Where(x => x.InventoryOrderDetailID == orderDetailID);
                                                if (invOrderDetailCollection.Count() > 0)
                                                {
                                                    Database.InventoryOrderDetail orderDetail = invOrderDetailCollection.FirstOrDefault();
                                                    orderDetail.ModifiedBy = currentUserName;
                                                    orderDetail.ModifiedDate = DateTime.Now.Date;
                                                    orderDetail.InventoryOrderID = itemOrder.InventoryOrderID;
                                                    orderDetail.InventoryTypeID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("rptrhdnItemID")).Value);
                                                    orderDetail.IsActive = true;
                                                    orderDetail.PurchaseRate = Convert.ToDecimal(((Label)rptrItem.FindControl("lblPurchaseRate")).Text);

                                                    //To add or substract inventory in inventory table
                                                    int? oldQuantity = orderDetail.Quantity;

                                                    orderDetail.Quantity = Convert.ToInt32(((Label)rptrItem.FindControl("lblQuantity")).Text);
                                                    orderDetail.SaleRate = Convert.ToDecimal(((Label)rptrItem.FindControl("lblSellingRate")).Text);
                                                    orderDetail.TotalItemsCost = Convert.ToDecimal(((Label)rptrItem.FindControl("lblTotalAmount")).Text);
                                                    orderDetail.VatNo = ((Label)rptrItem.FindControl("lblVAT")).Text;
                                                    context.SaveChanges();

                                                    //For company inventory detail
                                                    var inventoryCollection = context.InventoryTypes.Where(x => x.InventoryTypeId == orderDetail.InventoryTypeID);
                                                    if (inventoryCollection.Count() > 0)
                                                    {
                                                        var inventory = inventoryCollection.SingleOrDefault();
                                                        //substract old quantity and add new quantity
                                                        inventory.InventoryCount = inventory.InventoryCount - oldQuantity + orderDetail.Quantity;
                                                        inventory.PurchaseRate = orderDetail.PurchaseRate;
                                                        inventory.SellingRate = orderDetail.SaleRate;
                                                        inventory.VAT = orderDetail.VatNo;
                                                        inventory.ModifiedBy = currentUserName;
                                                        inventory.ModifiedDate = DateTime.Now.Date;
                                                        inventory.IsActive = true;
                                                        context.SaveChanges();
                                                    }
                                                }
                                                else
                                                {
                                                    InventoryOrderDetail orderDetail = AddInventoryOrderDetail(rptrItem, itemOrder.InventoryOrderID);

                                                    //If user add extra items in previous order
                                                    var inventoryCollection = context.InventoryTypes.Where(x => x.InventoryTypeId == orderDetail.InventoryTypeID);
                                                    if (inventoryCollection.Count() > 0)
                                                    {
                                                        var inventory = inventoryCollection.SingleOrDefault();
                                                        inventory.InventoryCount += orderDetail.Quantity;
                                                        inventory.PurchaseRate = orderDetail.PurchaseRate;
                                                        inventory.SellingRate = orderDetail.SaleRate;
                                                        inventory.VAT = orderDetail.VatNo;
                                                        inventory.ModifiedBy = currentUserName;
                                                        inventory.ModifiedDate = DateTime.Now.Date;
                                                        inventory.IsActive = true;
                                                        context.SaveChanges();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //For deleted item
                                            int orderDetailID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("rptrhdnOrderDetailID")).Value);
                                            if (orderDetailID > 0)
                                            {
                                                //it means item deleted from existing order
                                                var invOrderDetailCollection = context.InventoryOrderDetails.Where(x => x.InventoryOrderDetailID == orderDetailID);
                                                if (invOrderDetailCollection.Count() > 0)
                                                {
                                                    Database.InventoryOrderDetail orderDetail = invOrderDetailCollection.FirstOrDefault();
                                                    orderDetail.ModifiedBy = currentUserName;
                                                    orderDetail.ModifiedDate = DateTime.Now.Date;
                                                    orderDetail.IsActive = false;
                                                    context.SaveChanges();

                                                    //For company inventory detail
                                                    var inventoryCollection = context.InventoryTypes.Where(x => x.InventoryTypeId == orderDetail.InventoryTypeID);
                                                    if (inventoryCollection.Count() > 0)
                                                    {
                                                        var inventory = inventoryCollection.SingleOrDefault();
                                                        //substract quantity as we delete order Detail
                                                        inventory.InventoryCount = inventory.InventoryCount - orderDetail.Quantity;
                                                        inventory.ModifiedBy = currentUserName;
                                                        inventory.ModifiedDate = DateTime.Now.Date;
                                                        context.SaveChanges();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    lblMessage.Text = "Record Updated sucessfully";
                                    lblMessage.ForeColor = Color.Green;
                                    scope.Complete();
                                }
                            }
                        }
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
        #endregion

        #region User Method
        public void FillForm()
        {
            try
            {
                string PurchaseOrderIDObj = Request.QueryString[Constants.queryPurchaseOrderID];

                if (!string.IsNullOrEmpty(PurchaseOrderIDObj))
                {
                    int PurchaseOrderID = Convert.ToInt32(PurchaseOrderIDObj);
                    if (PurchaseOrderID > 0)
                    {
                        var PurchaseOrderCollection = context.InventoryOrders.Where(x => x.InventoryOrderID == PurchaseOrderID);
                        if (PurchaseOrderCollection.Count() > 0)
                        {
                            //Fill Other Form Data
                            Database.InventoryOrder purchaseOrder = PurchaseOrderCollection.FirstOrDefault();

                            txtAmountPaidDate.Text = purchaseOrder.AmountPaidDate.GetValueOrDefault().ToString(Constants.DateFormatDatePicker);
                            txtBalance.Text = Convert.ToString(purchaseOrder.BalanceAmount);
                            txtBillDate.Text = purchaseOrder.BillDate.GetValueOrDefault().ToString(Constants.DateFormatDatePicker);
                            txtDetails.Text = purchaseOrder.Remarks;
                            txtOwnBillNo.Text = purchaseOrder.OwnBillNo;
                            txtPaidAmount.Text = Convert.ToString(purchaseOrder.AmountPaid);
                            txtPurchaseBillNO.Text = purchaseOrder.PurchaseBillNo;
                            txtPurchaseDate.Text = purchaseOrder.PurchaseDate.GetValueOrDefault().ToString(Constants.DateFormatDatePicker);
                            txtTotalOrderAmount.Text = Convert.ToString(purchaseOrder.TotalOrderAmount);
                            ddlCustomerType.SelectedValue = Convert.ToString(purchaseOrder.CustomerTypeID);
                            txtSellerID.Text = Convert.ToString(purchaseOrder.SellerID);
                            Database.Seller seller = context.Sellers.Where(x => x.SellerID == purchaseOrder.SellerID).FirstOrDefault();
                            txtAutoSeller.Text = seller.SellerName;
                            chkOrderComplete.Checked = Convert.ToBoolean(purchaseOrder.IsCompleted);

                            //Fill Repeater Data
                            var invOrderCollection = context.InventoryOrderDetails.Where(x => x.InventoryOrderID == purchaseOrder.InventoryOrderID).ToList();
                            if (invOrderCollection.Count() > 0)
                            {
                                var purchaseOrders = from invOrder in invOrderCollection
                                                     join inv in context.InventoryTypes on invOrder.InventoryTypeID equals inv.InventoryTypeId
                                                     select new rptrClass
                                                     {
                                                         ItemID = invOrder.InventoryTypeID,
                                                         ItemName = inv.InventoryTypeName,
                                                         PurchaseRate = invOrder.PurchaseRate,
                                                         Quantity = invOrder.Quantity,
                                                         SellingRate = invOrder.SaleRate,
                                                         TotalAmount = invOrder.TotalItemsCost,
                                                         VAT = invOrder.VatNo,
                                                         ItemCode = inv.InventoryCode,
                                                         OrderDetailID = invOrder.InventoryOrderDetailID
                                                     };
                                rptrData.DataSource = purchaseOrders.ToList();
                                rptrData.DataBind();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "PurchaseOrder Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "PurchaseOrder ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "PurchaseOrder ID Not in right format";
                    lblMessage.ForeColor = Color.Red;
                }
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

        public InventoryOrderDetail AddInventoryOrderDetail(RepeaterItem rptrItem, int inventoryID)
        {
            try
            {
                InventoryOrderDetail orderDetail = new InventoryOrderDetail();
                orderDetail.CreatedBy = currentUserName;
                orderDetail.CreatedDate = DateTime.Now.Date;
                orderDetail.InventoryOrderID = inventoryID;
                orderDetail.InventoryTypeID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("rptrhdnItemID")).Value);
                orderDetail.IsActive = true;
                orderDetail.PurchaseRate = Convert.ToDecimal(((Label)rptrItem.FindControl("lblPurchaseRate")).Text);
                orderDetail.Quantity = Convert.ToInt32(((Label)rptrItem.FindControl("lblQuantity")).Text);
                orderDetail.SaleRate = Convert.ToDecimal(((Label)rptrItem.FindControl("lblSellingRate")).Text);
                orderDetail.TotalItemsCost = Convert.ToDecimal(((Label)rptrItem.FindControl("lblTotalAmount")).Text);
                orderDetail.VatNo = ((Label)rptrItem.FindControl("lblVAT")).Text;
                context.InventoryOrderDetails.Add(orderDetail);
                context.SaveChanges();
                return orderDetail;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public InventoryOrder AddItemOrder(InventoryOrder itemOrder)
        {
            try
            {
                itemOrder.AmountPaid = txtPaidAmount.Text == "" ? 0 : Convert.ToDecimal(txtPaidAmount.Text);
                itemOrder.AmountPaidDate = txtAmountPaidDate.Text == "" ? DateTime.Now.Date : Convert.ToDateTime(txtAmountPaidDate.Text);
                itemOrder.BillDate = txtBillDate.Text == "" ? DateTime.Now.Date : Convert.ToDateTime(txtBillDate.Text);
                itemOrder.ModifiedBy = currentUserName;
                itemOrder.ModifiedDate = DateTime.Now.Date;
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
                context.SaveChanges();
                return itemOrder;
            }
            catch (Exception)
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
            public int OrderDetailID { get; set; }
        }
        #endregion

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
                    Inventory.LowStockCount = Convert.ToInt32(txtPopupLowCount.Text);
                    Inventory.PurchaseRate = Convert.ToDecimal(txtPopupPurchaseRate.Text);
                    Inventory.RagNo = txtPopupRagNo.Text;
                    Inventory.SellingRate = Convert.ToDecimal(txtPopupSellingRate.Text);
                    Inventory.UnitName = txtPopupUnit.Text;
                    Inventory.VAT = txtPopupVat.Text;
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