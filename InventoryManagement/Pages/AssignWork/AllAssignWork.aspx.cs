using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryManagement.Common;

namespace InventoryManagement.Pages.AssignWork
{
    public partial class AllAssignWork : System.Web.UI.Page
    {
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                var datasource = from workassign in context.WorkAssigns.AsEnumerable()
                                 join emp in context.Employees on workassign.EmployeeID equals emp.EmployeeID
                                 join sal in context.Salaries on workassign.WorkAssignID equals sal.WorkAssignID
                                 join workType in context.WorkTypes on workassign.WorkTypeID equals workType.WorkTypeID
                                 select new
                                 {
                                     workassign.BillNo,
                                     workassign.CompletedCount,
                                     workassign.WorkAssignID,
                                     ExpectedCompletionDate = workassign.ExpectedCompletionDate == null ? null : workassign.ExpectedCompletionDate.Value.ToString(Constants.DateFormatDisplay),
                                     workassign.IsComplete,
                                     WorkAssignDate = workassign.WorkAssignDate == null ? null : workassign.WorkAssignDate.Value.ToString(Constants.DateFormatDisplay),
                                     workassign.WorkCount,
                                     workType.WorkTypeName,
                                     emp.EmployeeName,
                                     sal.AmountPaid,
                                     sal.AmountPaidDate,
                                     TotalCost = workassign.TotalCost
                                 };
                rptrWorkAssign.DataSource = datasource.ToList();
                rptrWorkAssign.DataBind();
            }
            catch
            {
                throw;
            }
        }

        protected void rptrWorkAssign_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string AssignWorkID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditAssignWork.aspx?" + Constants.queryAssignWorkID + "=" + AssignWorkID, false);
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