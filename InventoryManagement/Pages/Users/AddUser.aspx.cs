using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Users
{
    public partial class AddUser : System.Web.UI.Page
    {
        string currentUserName = string.Empty;
         
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                    BindDropDown();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUserName.Text;
                var users = context.Users.Where(x =>x.IsActive==true && x.UserName == username);
                if (users.Count() > 0)
                {
                    lblMessage.Text = "User Already exist with same username";
                    lblMessage.ForeColor = Color.Red;
                }
                else
                {
                    Database.User user = new Database.User();
                    if (ddlRole.SelectedValue != "0" && ddlEmployee.SelectedValue != "0")
                    {
                        user.CreatedBy = currentUserName;
                        user.CreatedDate = DateTime.Now;
                        user.IsActive = true;
                        user.Password = txtPassword.Text;
                        user.UserName = txtUserName.Text;
                        user.UserRoleTypeID = Convert.ToInt32(ddlRole.SelectedValue);
                        user.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
                        user.IsActive = true;
                        user.Remarks = txtRemarks.Text;
                        context.Users.Add(user);
                        context.SaveChanges();
                        if (CheckInternet())
                        {
                            sendEmail(Convert.ToInt32(ddlEmployee.SelectedValue), user);
                            //Success message is set from email method
                        }
                        else
                        {
                            lblMessage.Text = "User Created But Email is not sent due to no internet";
                            lblMessage.ForeColor = Color.Green;
                        }
                        ClearForm();
                    }
                    else
                    {
                        lblMessage.Text = "Please Select Employee and Role";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void sendEmail(int empId, User user)
        {
            try
            {
                Employee emp = context.Employees.Where(x => x.EmployeeID == empId).SingleOrDefault();
                string toEmail = emp.Email;
                if (!string.IsNullOrEmpty(toEmail))
                {
                    string subject = "User Detail for Inventory Management";
                    string body = "Below are the detail of user <br/> User Name : " + user.UserName + "<br/> Password :" + user.Password + "<br/>Regards, <br/>Inventory";
                    Helper.sendMail(toEmail, body, subject);
                    lblMessage.Text = "User Created Successfully and detail send to " + toEmail;
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "User Created Successfully and Detail is not send to employee as employee dont have email";
                    lblMessage.ForeColor = Color.Green;
                }
            }
            catch(Exception ex)
            {
                Helper.LogError(ex);
            }
        }

        public void BindDropDown()
        {
            try
            {
                ddlRole.DataSource = context.Roles.Where(x=>x.IsActive==true).Where(x=>x.IsActive==true).ToList();
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataValueField = "RoleID";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new ListItem("Select", "0"));

                ddlEmployee.DataSource = context.Employees.Where(x=>x.IsActive==true).Select(x => new { x.EmployeeID, x.EmployeeName }).ToList();
                ddlEmployee.DataTextField = "EmployeeName";
                ddlEmployee.DataValueField = "EmployeeID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch
            {
                throw;
            }
        }

        public bool CheckInternet()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public void ClearForm()
        {
            try
            {
                txtPassword.Text = "";
                txtRemarks.Text = "";
                txtUserName.Text = "";
                ddlEmployee.SelectedValue = "0";
                ddlRole.SelectedValue = "0";
            }
            catch
            {
                throw;
            }
        }
    }
}