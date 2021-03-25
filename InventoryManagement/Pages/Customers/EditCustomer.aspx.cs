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
    public partial class EditCustomer : System.Web.UI.Page
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
                string CustomerID = hdnCustomerID.Value;
                if(!string.IsNullOrEmpty(CustomerID))
                {
                    int customerID = Convert.ToInt32(CustomerID);
                    if (customerID > 0)
                    {
                        var CustomerObj = context.Customers.Where(x => x.CustomerID == customerID);
                        if (CustomerObj.Count() > 0)
                        {
                            var customerCollection = context.Customers.Where(x =>x.IsActive==true && x.CustomerName == txtCustomerName.Text && x.CustomerMobile == txtCustomerMobile.Text && x.CustomerID != customerID);
                            if (customerCollection.Count() == 0)
                            {
                                Database.Customer customer = CustomerObj.FirstOrDefault();
                                customer.ModifiedBy = currentUserName;
                                customer.ModifiedDate = DateTime.Now.Date;
                                customer.CustomerAddress = txtCustomerAddress.Text;
                                customer.CustomerMobile = txtCustomerMobile.Text;
                                customer.CustomerName = txtCustomerName.Text;
                                customer.CustomerEmail = txtEmail.Text;
                                customer.IsActive = true;
                                context.SaveChanges();

                                lblMessage.Text = "Customer Updated Successfully";
                                lblMessage.ForeColor = Color.Green;
                            }
                            else
                            {
                                lblMessage.Text = "Customer Already Exist Choose Diffrent Name Or Mobile";
                                lblMessage.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Customer Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Customer ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Customer ID Not in proper format";
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
                string CustomerIDObj = Request.QueryString[Constants.queryCustomerID];

                if (!string.IsNullOrEmpty(CustomerIDObj))
                {
                    int CustomerID = Convert.ToInt32(CustomerIDObj);
                    if (CustomerID > 0)
                    {
                        var CustomerCollection = context.Customers.Where(x =>x.IsActive==true && x.CustomerID == CustomerID);
                        if (CustomerCollection.Count() > 0)
                        {
                            Database.Customer customer = CustomerCollection.FirstOrDefault();
                            txtCustomerAddress.Text = customer.CustomerAddress;
                            txtCustomerMobile.Text = customer.CustomerMobile;
                            txtCustomerName.Text = customer.CustomerName;
                            txtEmail.Text = customer.CustomerEmail;
                            hdnCustomerID.Value =Convert.ToString(customer.CustomerID);
                        }
                        else
                        {
                            lblMessage.Text = "Customer Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Customer ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Customer ID Not in right format";
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