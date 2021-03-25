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
    public partial class AddEmployee : System.Web.UI.Page
    {
        string currentUserName = string.Empty;
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                txtEmpJoiningDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
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
                var EmployeeCollection = context.Employees.Where(x => x.IsActive==true && x.EmployeeName == txtEmployeeName.Text && x.Mobile == txtEmployeeMobile.Text);
                if (EmployeeCollection.Count() == 0)
                {
                    Database.Employee employee = new Database.Employee();
                    employee.Address = txtEmployeeAddress.Text;
                    employee.CreatedBy = currentUserName;
                    employee.CreatedDate = DateTime.Now.Date;
                    employee.Detail = txtEmployeeDetail.Text;
                    employee.Email = txtEmail.Text;
                    employee.EmployeeName = txtEmployeeName.Text;
                    employee.IsActive = true;
                    employee.JoiningDate =Convert.ToDateTime(txtEmpJoiningDate.Text);
                    employee.Mobile = txtEmployeeMobile.Text;
                    context.Employees.Add(employee);
                    context.SaveChanges();

                    lblMessage.Text = "Employee Added Successfully";
                    lblMessage.ForeColor = Color.Green;
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Employee Already exist with same name and Mobile";
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

        public void ClearForm()
        {
            txtEmail.Text = "";
            txtEmployeeAddress.Text = "";
            txtEmployeeMobile.Text = "";
            txtEmployeeName.Text = "";
            txtEmpJoiningDate.Text = "";
            txtEmployeeDetail.Text = "";
        }
    }
}