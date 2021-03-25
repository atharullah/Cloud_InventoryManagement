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
    public partial class AllInventory : System.Web.UI.Page
    {
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                BindGrid();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void BindGrid()
        {
            try
            {
                rptrInventory.DataSource = context.InventoryTypes.Where(x=>x.IsActive==true).Select(x => new
                {
                    x.FastMoving,
                    x.InventoryCode,
                    x.InventoryDescription,
                    x.InventoryTypeId,
                    x.InventoryTypeName,
                    x.LowStockCount,
                    x.PurchaseRate,
                    x.RagNo,
                    x.SellingRate,
                    x.InventoryCount,
                    x.VAT
                }).OrderBy(x=>x.InventoryTypeName).ToList();
                rptrInventory.DataBind();
            }
            catch
            {
                throw;
            }
        }

        protected void rptrInventory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string InventoryID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditInventory.aspx?" + Constants.queryInventoryID + "=" + InventoryID,false);
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