using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Customers
{
    public partial class AllCustomers : System.Web.UI.Page
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
                rptrCustomer.DataSource = context.Customers.Where(x => x.IsActive == true).Select(x => new
                {
                    x.CustomerID,
                    x.CustomerMobile,
                    x.CustomerName,
                    x.CustomerEmail
                }).OrderBy(x => x.CustomerName).ToList();
                rptrCustomer.DataBind();
            }
            catch
            {
                throw;
            }
        }

        //protected void gridCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        rptrCustomer.PageIndex = e.NewPageIndex;
        //        BindGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogError(ex);
        //        lblMessage.Text = "Something went Wrong kindly check log";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void gridCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "Edit")
        //        {
        //            string CustomerID = Convert.ToString(e.CommandArgument);
        //            Response.Redirect("EditCustomer.aspx?" + Constants.queryCustomerID + "=" + CustomerID);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogError(ex);
        //        lblMessage.Text = "Something went Wrong kindly check log";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        protected void rptrCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string CustomerID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditCustomer.aspx?" + Constants.queryCustomerID + "=" + CustomerID, false);
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