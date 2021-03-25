using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Reports
{
    public partial class SellingReport : System.Web.UI.Page
    {
        #region Page Member
        string currentUserName = string.Empty;
        InventoryManagementEntities context = new InventoryManagementEntities();
        #endregion

        #region Page Method
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = new DateTime();
                DateTime toDate = DateTime.Now.AddDays(1);

                var fromDateText = txtFromMonth.Text;
                if (fromDateText != "")
                {
                    fromDate = Convert.ToDateTime(fromDateText);
                }
                var toDateText = txtToMonth.Text;
                if (toDateText != "")
                {
                    toDate = Convert.ToDateTime(toDateText);
                }

                var saleOrderLst = context.SalesOrders.Where(x => x.IsActive == true && x.SaleOrderDate >= fromDate && x.SaleOrderDate <= toDate).Select(x => new tempClass
                {
                    saleID = x.SaleOrderID
                }).ToList();

                var saleOrderDetailLst = from sorderDetails in context.SaleOrderDetails.Where(x => x.IsActive == true).AsEnumerable()
                                         join sOrderIDLst in saleOrderLst on sorderDetails.SaleOrderID equals sOrderIDLst.saleID
                                         select sorderDetails;
                var saleDetails = saleOrderDetailLst.AsEnumerable().GroupBy(x => x.InventoryTypeID).Select(x => new
                {
                    InvTypeID = x.Key,
                    Quantity = x.Sum(y => y.Quantity == null ? 0 : y.Quantity),
                    TotalAmount = x.Sum(k => k.TotalAmount == null ? 0 : k.TotalAmount),
                    Vat = x.Sum(h => h.VAT == "" ? 0 : Convert.ToDecimal(h.VAT))
                });

                var lst = from Inv in context.InventoryTypes.Where(x => x.IsActive == true).AsEnumerable()
                          join saleOrderDetail in saleDetails on Inv.InventoryTypeId equals saleOrderDetail.InvTypeID
                          select new
                          {
                              GSTVAT = saleOrderDetail.Vat,
                              ItemName = Inv.InventoryTypeName,
                              Quantity = saleOrderDetail.Quantity,
                              TotalAmount = saleOrderDetail.TotalAmount
                          };
                rptrInventoryReport.DataSource = lst.ToList();
                rptrInventoryReport.DataBind();
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

        class tempClass
        {
            public int? saleID { get; set; }
        }

        #endregion
    }
}