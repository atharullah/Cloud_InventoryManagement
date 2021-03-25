using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Users
{
    public partial class AllUsers : System.Web.UI.Page
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
                var rptrData = from user in context.Users.Where(x=>x.IsActive==true).AsEnumerable()
                               join emp in context.Employees.Where(x => x.IsActive == true).AsEnumerable() on user.EmployeeID equals emp.EmployeeID
                               join role in context.Roles.Where(x => x.IsActive == true).AsEnumerable() on user.UserRoleTypeID equals role.RoleID
                               select new
                               {
                                   Remarks=user.Remarks,
                                   UserName=user.UserName,
                                   UserID=user.UserID,
                                   EmployeeName=emp.EmployeeName,
                                   RoleName=role.RoleName,
                                   Mobile=emp.Mobile,
                                   Email=emp.Email
                               };
                rptrUser.DataSource = rptrData.ToList();
                rptrUser.DataBind();
            }
            catch
            {
                throw;
            }
        }

        //protected void gridUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        gridUser.PageIndex = e.NewPageIndex;
        //        BindGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogError(ex);
        //        lblMessage.Text = "Something went Wrong kindly check log";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void gridUser_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "Edit")
        //        {
        //            string UserID = Convert.ToString(e.CommandArgument);
        //            Response.Redirect("EditUser.aspx?" + Constants.queryUserID + "=" + UserID);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogError(ex);
        //        lblMessage.Text = "Something went Wrong kindly check log";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        protected void rptrUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string UserID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditUser.aspx?" + Constants.queryUserID + "=" + UserID,false);
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