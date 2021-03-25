using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Inventory
{
    public partial class AllPurchaseOrders : System.Web.UI.Page
    {
        InventoryManagementEntities context = new InventoryManagementEntities();
        string currentUserName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                BindRepeater();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void BindRepeater()
        {
            try
            {
                var datasource = from invOrder in context.InventoryOrders.AsEnumerable()
                                 select new
                                 {
                                     InventoryOrderID = invOrder.InventoryOrderID,
                                     AmountPaid = invOrder.AmountPaid,
                                     IsCompleted = invOrder.IsCompleted,
                                     PurchaseBillNo = invOrder.PurchaseBillNo,
                                     TotalOrderAmount = invOrder.TotalOrderAmount,
                                     PurchaseDate = invOrder.PurchaseDate.Value.ToString(Constants.DateFormatDisplay),
                                     AmountPaidDate = invOrder.AmountPaidDate.Value.ToString("yyy-MM-dd"),
                                     Remarks = invOrder.Remarks,
                                     OwnBillNo=invOrder.OwnBillNo
                                 };
                rptrPurchaseOrders.DataSource = datasource.ToList();
                rptrPurchaseOrders.DataBind();
            }
            catch
            {
                throw;
            }
        }

        //public class GridClass
        //{
        //    public int InventoryOrderID { get; set; }
        //    public decimal? AmountPaid { get; set; }
        //    public bool? IsCompleted { get; set; }
        //    public string PurchaseBillNo { get; set; }
        //    public decimal? TotalOrderAmount { get; set; }
        //    public string PurchaseDate { get; set; }
        //    public string AmountPaidDate { get; set; }
        //    public string Remarks { get; set; }
        //}

        protected void rptrPurchaseOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string PurchaseOrderID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditPurchaseOrder.aspx?" + Constants.queryPurchaseOrderID + "=" + PurchaseOrderID,false);
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