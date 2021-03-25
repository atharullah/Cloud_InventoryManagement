using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.SaleOrder
{
    public partial class AllSaleOrders : System.Web.UI.Page
    {
        InventoryManagementEntities context = new InventoryManagementEntities();
        string currentUserName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
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
                var datasource = from saleOrder in context.SalesOrders.AsEnumerable()
                                 join customer in context.Customers on saleOrder.CustomerID equals customer.CustomerID
                                 join customerType in context.CustomerTypes on saleOrder.CustomerTypeID equals customerType.CustomerTypeID
                                 select new
                                 {
                                     saleOrder.SaleOrderID,
                                     saleOrder.BillNo,
                                     customer.CustomerMobile,
                                     saleOrder.ClassName,
                                     customer.CustomerName,
                                     customerType.CustomerTypeName,
                                     saleOrder.IsCompleted,
                                     saleOrder.IsMakingRequired,
                                     saleOrder.PaidAmount,
                                     saleOrder.Profit,
                                     saleOrder.RemainingAmount,
                                     SaleOrderDate=saleOrder.SaleOrderDate.Value.ToString(Constants.DateFormatDisplay),
                                     saleOrder.TotalCost
                                 };
                rptrSaleOrders.DataSource = datasource.ToList();
                rptrSaleOrders.DataBind();
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
        //    public string SaleBillNo { get; set; }
        //    public decimal? TotalOrderAmount { get; set; }
        //    public string SaleDate { get; set; }
        //    public string AmountPaidDate { get; set; }
        //    public string Remarks { get; set; }
        //}

        protected void rptrSaleOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string saleOrderID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditSaleOrder.aspx?" + Constants.querySaleOrderID + "=" + saleOrderID,false);
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