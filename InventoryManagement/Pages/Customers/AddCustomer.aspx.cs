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
    public partial class AddCustomer : System.Web.UI.Page
    {
        string currentUserName = string.Empty;
         
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
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
                var customerCollection = context.Customers.Where(x => x.CustomerName == txtCustomerName.Text && x.CustomerMobile == txtCustomerMobile.Text && x.IsActive==true);
                if (customerCollection.Count() == 0)
                {
                    Database.Customer customer = new Database.Customer();
                    customer.CreatedBy = currentUserName;
                    customer.CreatedDate = DateTime.Now.Date;
                    customer.CustomerAddress = txtCustomerAddress.Text;
                    customer.CustomerMobile = txtCustomerMobile.Text;
                    customer.CustomerName = txtCustomerName.Text;
                    customer.CustomerEmail = txtEmail.Text;
                    customer.IsActive = true;
                    context.Customers.Add(customer);
                    context.SaveChanges();

                    lblMessage.Text = "Customer Added Successfully";
                    lblMessage.ForeColor = Color.Green;
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Customer Already exist with same name and Mobile";
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
            txtCustomerAddress.Text = "";
            txtCustomerMobile.Text = "";
            txtCustomerName.Text = "";
        }
    }
}