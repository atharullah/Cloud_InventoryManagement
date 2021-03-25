using InventoryManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                object sessionObj = Session[Constants.SessionUserName];
                if (sessionObj == null)
                {
                    Response.Redirect(Constants.URLLogIn, false);
                }
                else
                {
                    string roleTypeID = Convert.ToString(Session[Constants.SessionRoleID]);
                    hdnCurrentUserRole.Value = roleTypeID;
                    lblUserName.Text = Convert.ToString(Session[Constants.SessionUserName]);

                    switch (roleTypeID)
                    {
                        case "1":
                            //hrefAssignWork.Visible = true;
                            //hrefEmployee.Visible = true;
                            //hrefInType.Visible = true;
                            //hrefInventory.Visible = true;
                            //hrefUser.Visible = true;
                            // adminNav.Visible = true;
                            // reportNav.Visible = true;
                            break;
                        case "2":
                            //hrefAssignWork.Visible = true;
                            //hrefEmployee.Visible = true;
                            //hrefInType.Visible = true;
                            //hrefInventory.Visible = true;
                            //hrefUser.Visible = true;
                            // adminNav.Visible = false;
                            // reportNav.Visible = false;
                            break;
                        default:
                            //hrefAssignWork.Visible = true;
                            //hrefEmployee.Visible = true;
                            //hrefInType.Visible = true;
                            //hrefInventory.Visible = true;
                            //hrefUser.Visible = true;
                            // adminNav.Visible = false;
                            // reportNav.Visible = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                throw;
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Abandon();
                Response.Redirect(Constants.URLLogIn, false);
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                throw;
            }
        }
    }
}