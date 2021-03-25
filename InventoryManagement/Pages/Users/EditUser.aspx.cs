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
    public partial class EditUser : System.Web.UI.Page
    {
        string currentUserName = string.Empty;
         
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                {
                    FillForm();
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string UserIDObj = hdnUserID.Value;
                if (!string.IsNullOrEmpty(UserIDObj))
                {
                    int UserID = Convert.ToInt32(UserIDObj);
                    if (UserID > 0)
                    {
                        var UserObj = context.Users.Where(x => x.IsActive==true && x.UserID == UserID);
                        if (UserObj.Count() > 0)
                        {
                            var UserCollection = context.Users.Where(x => x.IsActive==true && x.UserName == txtUserName.Text && x.UserID != UserID);
                            if (UserCollection.Count() == 0)
                            {
                                Database.User user = UserObj.FirstOrDefault();

                                user.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
                                user.IsActive = chkIsActive.Checked;
                                user.ModifiedBy = currentUserName;
                                user.ModifiedDate = DateTime.Now.Date;
                                user.Password = txtPassword.Text;
                                user.Remarks = txtRemarks.Text;
                                user.UserName = txtUserName.Text;
                                user.UserRoleTypeID = Convert.ToInt32(ddlRole.SelectedValue);
                                context.SaveChanges();

                                lblMessage.Text = "User Updated Successfully";
                                lblMessage.ForeColor = Color.Green;
                            }
                            else
                            {
                                lblMessage.Text = "User Already Exist Choose Diffrent Name Or Mobile";
                                lblMessage.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "User Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "User ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "User ID Not in right format";
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

        public void FillForm()
        {
            try
            {
                string UserIDObj = Request.QueryString[Constants.queryUserID];

                if (!string.IsNullOrEmpty(UserIDObj))
                {
                    int UserID = Convert.ToInt32(UserIDObj);
                    if (UserID > 0)
                    {
                        var UserCollection = context.Users.Where(x => x.IsActive==true && x.UserID == UserID);
                        if (UserCollection.Count() > 0)
                        {
                            Database.User user = UserCollection.FirstOrDefault();
                            txtUserName.Text = user.UserName;
                            ddlEmployee.SelectedValue =Convert.ToString(user.EmployeeID);
                            ddlRole.SelectedValue = Convert.ToString(user.UserRoleTypeID);
                            hdnUserID.Value =Convert.ToString(user.UserID);
                            txtRemarks.Text = user.Remarks;
                            chkIsActive.Checked =Convert.ToBoolean(user.IsActive);
                        }
                        else
                        {
                            lblMessage.Text = "User Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "User ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "User ID Not in right format";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}