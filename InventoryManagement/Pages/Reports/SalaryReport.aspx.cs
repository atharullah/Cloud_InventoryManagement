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
    public partial class SalaryReport : System.Web.UI.Page
    {
        InventoryManagementEntities context = new InventoryManagementEntities();

        #region Page Method
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                generateReport();
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
        public void generateReport()
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

                var workcollection = from work in context.WorkAssigns.Where(x => x.IsActive == true).AsEnumerable()
                                     join sal in context.Salaries.Where(x => x.IsActive == true && x.AmountPaidDate >= fromDate && x.AmountPaidDate <= toDate).AsEnumerable() on work.WorkAssignID equals sal.WorkAssignID
                                     select work;


                var collection = workcollection.GroupBy(x => x.EmployeeID)
                                               .Select(x => new
                                               {
                                                   EmployeeID = x.Key,
                                                   RemainingCost = x.Sum(k => (k.RemainingCost == null ? 0 : k.RemainingCost)),
                                                   TotalCost = x.Sum(k => (k.TotalCost == null ? 0 : k.TotalCost))
                                               });

                var Datasource = from workassign in workcollection
                                 join emp in context.Employees.Where(x => x.IsActive == true).AsEnumerable() on workassign.EmployeeID equals emp.EmployeeID
                                 select new
                                 {
                                     EmployeeName = emp.EmployeeName,
                                     PaidAmount = workassign.TotalCost - workassign.RemainingCost
                                 };
                var finaldata = Datasource.ToList();

                rptrSalaryReport.DataSource = finaldata;
                rptrSalaryReport.DataBind();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}