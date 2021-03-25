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
    public partial class EditEmployee : System.Web.UI.Page
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
                string EmpIDObj = hdnEmployeeID.Value;
                if (!string.IsNullOrEmpty(EmpIDObj))
                {
                    int EmpID = Convert.ToInt32(EmpIDObj);

                    if (EmpID > 0)
                    {
                        var EmployeeObj = context.Employees.Where(x => x.IsActive == true && x.EmployeeID == EmpID);
                        if (EmployeeObj.Count() > 0)
                        {
                            var EmployeeCollection = context.Employees.Where(x => x.IsActive == true && x.EmployeeName == txtEmployeeName.Text && x.Mobile == txtEmployeeMobile.Text && x.EmployeeID != EmpID);
                            if (EmployeeCollection.Count() == 0)
                            {
                                Database.Employee employee = EmployeeObj.FirstOrDefault();
                                employee.Address = txtEmployeeAddress.Text;
                                employee.CreatedBy = currentUserName;
                                employee.CreatedDate = DateTime.Now.Date;
                                employee.Detail = txtEmployeeDetail.Text;
                                employee.Email = txtEmail.Text;
                                employee.EmployeeName = txtEmployeeName.Text;
                                employee.IsActive = true;
                                employee.JoiningDate = Convert.ToDateTime(txtEmpJoiningDate.Text);
                                employee.Mobile = txtEmployeeMobile.Text;
                                context.SaveChanges();

                                lblMessage.Text = "Employee Updated Successfully";
                                lblMessage.ForeColor = Color.Green;
                            }
                            else
                            {
                                lblMessage.Text = "Employee Already Exist Choose Diffrent Name Or Mobile";
                                lblMessage.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Employee Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Employee ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Employee ID Not in correct  format";
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
                string EmployeeIDObj = Request.QueryString[Constants.queryEmployeeID];

                if (!string.IsNullOrEmpty(EmployeeIDObj))
                {
                    int EmployeeID = Convert.ToInt32(EmployeeIDObj);
                    if (EmployeeID > 0)
                    {
                        var EmployeeCollection = context.Employees.Where(x => x.IsActive == true && x.EmployeeID == EmployeeID);
                        if (EmployeeCollection.Count() > 0)
                        {
                            Database.Employee employee = EmployeeCollection.FirstOrDefault();
                            txtEmail.Text = employee.Email;
                            txtEmpJoiningDate.Text = employee.JoiningDate == null ? "" : employee.JoiningDate.Value.ToString(Constants.DateFormatDatePicker);
                            txtEmployeeAddress.Text = employee.Address;
                            txtEmployeeDetail.Text = employee.Detail;
                            txtEmployeeMobile.Text = employee.Mobile;
                            txtEmployeeName.Text = employee.EmployeeName;
                            hdnEmployeeID.Value = Convert.ToString(EmployeeID);
                        }
                        else
                        {
                            lblMessage.Text = "Employee Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Employee ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Employee ID Not in right format";
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