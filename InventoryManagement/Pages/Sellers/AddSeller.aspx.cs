using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Seller
{
    public partial class AddSeller : System.Web.UI.Page
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
                var sellerCollection = context.Sellers.Where(x => x.IsActive==true && x.SellerName == txtSellerName.Text && x.SellerMobile == txtMobile.Text);
                if (sellerCollection.Count() == 0)
                {
                    Database.Seller seller = new Database.Seller();
                    seller.AccountHolderName = txtAccountHolderName.Text;
                    seller.AccountNo = txtAccountNo.Text;
                    seller.BankName = txtBankName.Text;
                    seller.CreatedBy = currentUserName;
                    seller.CreatedDate = DateTime.Now.Date;
                    seller.IFSCCode = txtIFSC.Text;
                    seller.SellerAddress = txtSellerAddress.Text;
                    seller.SellerLandline = txtLandline.Text;
                    seller.SellerMobile = txtMobile.Text;
                    seller.SellerName = txtSellerName.Text;
                    seller.TINNo = txtTinNo.Text;
                    seller.VATNo = txtVatNo.Text;
                    seller.SellerEmail = txtEmail.Text;
                    seller.IsActive = true;
                    context.Sellers.Add(seller);
                    context.SaveChanges();

                    lblMessage.Text = "Seller Added Successfully";
                    lblMessage.ForeColor = Color.Green;
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Seller Already exist with same name or Mobile";
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
            txtAccountHolderName.Text = "";
            txtAccountNo.Text = "";
            txtBankName.Text = "";
            txtIFSC.Text = "";
            txtLandline.Text = "";
            txtMobile.Text = "";
            txtSellerAddress.Text = "";
            txtSellerName.Text = "";
            txtTinNo.Text = "";
            txtVatNo.Text = "";
        }
    }
}