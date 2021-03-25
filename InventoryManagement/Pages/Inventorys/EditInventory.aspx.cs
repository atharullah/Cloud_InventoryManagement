using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Inventorys
{
    public partial class EditInventory : System.Web.UI.Page
    {
        string currentUserName = string.Empty;
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                {
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string InventoryIDObj = hdnInventoryID.Value;
                if (!string.IsNullOrEmpty(InventoryIDObj))
                {
                    int InventoryID = Convert.ToInt32(InventoryIDObj);
                    if (InventoryID > 0)
                    {
                        var inventoryObj = context.InventoryTypes.Where(x => x.IsActive == true && x.InventoryTypeId == InventoryID);
                        if (inventoryObj.Count() > 0)
                        {
                            var InventoryCollection = context.InventoryTypes.Where(x => x.IsActive == true && (x.InventoryCode == txtInventoryCode.Text || x.InventoryTypeName == txtInventoryTypeName.Text) && x.InventoryTypeId != InventoryID);
                            if (InventoryCollection.Count() == 0)
                            {
                                Database.InventoryType inventory = inventoryObj.FirstOrDefault();

                                inventory.FastMoving = chkFastMoving.Checked;
                                inventory.InventoryCode = txtInventoryCode.Text;
                                inventory.InventoryDescription = txtInvDesc.Text;
                                inventory.InventoryTypeName = txtInventoryTypeName.Text;
                                inventory.IsActive = true;
                                inventory.ModifiedBy = currentUserName;
                                inventory.ModifiedDate = DateTime.Now.Date;
                                inventory.LowStockCount = string.IsNullOrEmpty(txtLowCount.Text) ? 0 : Convert.ToInt32(txtLowCount.Text);
                                inventory.PurchaseRate = string.IsNullOrEmpty(txtPurchaseRate.Text) ? 0 : Convert.ToDecimal(txtPurchaseRate.Text);
                                inventory.RagNo = txtRagNo.Text;
                                inventory.SellingRate = string.IsNullOrEmpty(txtSellingRate.Text) ? 0 : Convert.ToDecimal(txtSellingRate.Text);
                                inventory.UnitName = txtUnit.Text;
                                inventory.VAT = txtVAT.Text;
                                context.SaveChanges();

                                lblMessage.Text = "Inventory Updated Successfully";
                                lblMessage.ForeColor = Color.Green;
                            }
                            else
                            {
                                lblMessage.Text = "Inventory Name or Code already Exist kindly choose other";
                                lblMessage.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Inventory ID Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }

                    }
                    else
                    {
                        lblMessage.Text = "Inventory ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Inventory ID Not in Valid Format";
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

        public void FillForm()
        {
            try
            {
                string InventoryIDObj = Request.QueryString[Constants.queryInventoryID];

                if (!string.IsNullOrEmpty(InventoryIDObj))
                {
                    int InventoryTypeID = Convert.ToInt32(InventoryIDObj);
                    if (InventoryTypeID > 0)
                    {
                        var InventoryCollection = context.InventoryTypes.Where(x => x.IsActive == true && x.InventoryTypeId == InventoryTypeID);
                        if (InventoryCollection.Count() > 0)
                        {
                            Database.InventoryType inventoryType = InventoryCollection.FirstOrDefault();
                            txtInvDesc.Text = inventoryType.InventoryDescription;
                            txtInventoryCode.Text = inventoryType.InventoryCode;
                            txtInventoryTypeName.Text = inventoryType.InventoryTypeName;
                            txtLowCount.Text = Convert.ToString(inventoryType.LowStockCount);
                            txtPurchaseRate.Text = Convert.ToString(inventoryType.PurchaseRate);
                            txtRagNo.Text = inventoryType.RagNo;
                            txtSellingRate.Text = Convert.ToString(inventoryType.SellingRate);
                            txtUnit.Text = inventoryType.UnitName;
                            txtVAT.Text = inventoryType.VAT;
                            chkFastMoving.Checked = Convert.ToBoolean(inventoryType.FastMoving);
                            hdnInventoryID.Value = Convert.ToString(InventoryTypeID);
                        }
                        else
                        {
                            lblMessage.Text = "Inventory Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Inventory ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Inventory ID Not in right format";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}