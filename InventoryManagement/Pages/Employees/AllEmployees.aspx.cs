using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Employees
{
    public partial class AllEmployees : System.Web.UI.Page
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
                rptrEmployee.DataSource = context.Employees.AsEnumerable().Where(x=>x.IsActive==true).Select(x => new
                {
                    x.Email,
                    x.EmployeeID,
                    x.EmployeeName,
                    JoiningDate=x.JoiningDate.Value.ToString(Constants.DateFormatDisplay),
                    x.Mobile,
                }).OrderBy(x=>x.EmployeeName).ToList();
                rptrEmployee.DataBind();
            }
            catch
            {
                throw;
            }
        }

        protected void rptrEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string EmployeeID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditEmployee.aspx?" + Constants.queryEmployeeID + "=" + EmployeeID,false);
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