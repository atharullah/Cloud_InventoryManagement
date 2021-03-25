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
    public partial class AddInventory : System.Web.UI.Page
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
            try
            {
                txtInvDesc.Text = "";
                txtInventoryCode.Text = "";
                txtInventoryTypeName.Text = "";
                txtLowCount.Text = "";
                txtPurchaseRate.Text = "";
                txtRagNo.Text = "";
                txtSellingRate.Text = "";
                txtUnit.Text = "";
                txtVAT.Text = "";
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Data Saved Successfully";
                lblMessage.ForeColor = Color.Green;
            }
        }

        protected void btnAddInventory_Click(object sender, EventArgs e)
        {
            try
            {
                string invName = txtInventoryTypeName.Text;
                string invCode = txtInventoryCode.Text;
                var invType = context.InventoryTypes.Where(x => x.InventoryCode == invCode || x.InventoryTypeName == invName);
                if(invType.Count()==0)
                {
                    Database.InventoryType Inventory = new Database.InventoryType();
                    Inventory.CreatedBy = currentUserName;
                    Inventory.CreatedDate = DateTime.Now.Date;
                    Inventory.FastMoving = chkFastMoving.Checked;
                    Inventory.InventoryCode = txtInventoryCode.Text;
                    Inventory.InventoryDescription = txtInvDesc.Text;
                    Inventory.InventoryTypeName = txtInventoryTypeName.Text;
                    Inventory.IsActive = true;
                    Inventory.LowStockCount = string.IsNullOrEmpty(txtLowCount.Text) ? 0 : Convert.ToInt32(txtLowCount.Text);
                    Inventory.PurchaseRate = string.IsNullOrEmpty(txtPurchaseRate.Text) ? 0 : Convert.ToDecimal(txtPurchaseRate.Text);
                    Inventory.RagNo = txtRagNo.Text;
                    Inventory.SellingRate = string.IsNullOrEmpty(txtSellingRate.Text) ? 0 : Convert.ToDecimal(txtSellingRate.Text);
                    Inventory.UnitName = txtUnit.Text;
                    Inventory.VAT = txtVAT.Text;
                    context.InventoryTypes.Add(Inventory);
                    context.SaveChanges();

                    lblMessage.Text = "Inventory Added Successfully";
                    lblMessage.ForeColor = Color.Green;
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Inventory Name or code already exist kindly choose another one";
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
    }
}