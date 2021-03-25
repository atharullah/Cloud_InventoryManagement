using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryManagement.Common;
using System.Web.Services;

namespace InventoryManagement.Pages.Main
{
    public partial class LogIn : System.Web.UI.Page
    {
        #region Page Member
        InventoryManagementEntities context = new InventoryManagementEntities();
        #endregion

        #region Page Method
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var user = context.Users.Where(x =>x.IsActive==true && x.UserName.ToUpper() == txtUserName.Text.ToUpper() && x.Password == txtPassword.Text);
                if (user.Count() > 0)
                {
                    InventoryManagement.Database.User currentUser = user.SingleOrDefault();
                    Session[Constants.SessionUserName] = currentUser.UserName;
                    Session[Constants.SessionRoleID] = currentUser.UserRoleTypeID;
                    Response.Redirect(Constants.URLSaleOrder,false);
                }
                else
                {
                    lblMessage.Text = "Invalid Username Or Password";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }
        #endregion
    }
}