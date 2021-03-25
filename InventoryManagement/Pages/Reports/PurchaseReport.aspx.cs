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
    public partial class PurchaseReport : System.Web.UI.Page
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

                var pOrderLst = context.InventoryOrders.Where(x => x.IsActive == true && x.PurchaseDate >= fromDate && x.PurchaseDate <= toDate).Select(x => new tempClass
                {
                    POrderID = x.InventoryOrderID
                }).ToList();

                var invOrderDetailLst = from invODetail in context.InventoryOrderDetails.Where(x => x.IsActive == true).AsEnumerable()
                                        join invID in pOrderLst on invODetail.InventoryOrderID equals invID.POrderID
                                        select invODetail;

                var pOrderDetails = invOrderDetailLst.AsEnumerable().GroupBy(x => x.InventoryTypeID).Select(x => new
                {
                    InvTypeID = x.Key,
                    Quantity = x.Sum(y => y.Quantity == null ? 0 : y.Quantity),
                    Vat = x.Sum(k => string.IsNullOrEmpty(k.VatNo) ? 0 : Convert.ToDecimal(k.VatNo)),
                    TotalAmount = x.Sum(j => j.TotalItemsCost == null ? 0 : j.TotalItemsCost),
                });

                var lst = from purchaseOrderDetail in pOrderDetails
                          join invType in context.InventoryTypes.Where(x => x.IsActive == true).AsEnumerable() on purchaseOrderDetail.InvTypeID equals invType.InventoryTypeId
                          select new RptrClass
                          {
                              GSTVAT = purchaseOrderDetail.Vat,
                              ItemName = invType.InventoryTypeName,
                              Quantity = purchaseOrderDetail.Quantity,
                              TotalAmount = purchaseOrderDetail.TotalAmount,
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

        class RptrClass
        {
            public string ItemName { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? TotalAmount { get; set; }
            public decimal? GSTVAT { get; set; }
        }

        class tempClass
        {
            public int? POrderID { get; set; }
        }

        #endregion
    }
}