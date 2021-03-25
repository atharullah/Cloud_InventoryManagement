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
    public partial class EditSeller : System.Web.UI.Page
    {
        string currentUserName = string.Empty;
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                    FillForm();
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
                string sellerIDObj = hdnSellerID.Value;
                if (!string.IsNullOrEmpty(sellerIDObj))
                {
                    int sellerID = Convert.ToInt32(sellerIDObj);
                    if (sellerID > 0)
                    {
                        var sellerCollection = context.Sellers.Where(x => x.IsActive == true && x.SellerID == sellerID);
                        if (sellerCollection.Count() > 0)
                        {
                            var customerCollection = context.Sellers.Where(x => x.IsActive==true && x.SellerName == txtSellerName.Text && x.SellerMobile == txtMobile.Text && x.SellerID != sellerID);
                            if (customerCollection.Count() == 0)
                            {
                                Database.Seller seller = sellerCollection.FirstOrDefault();
                                seller.AccountHolderName = txtAccountHolderName.Text;
                                seller.AccountNo = txtAccountNo.Text;
                                seller.BankName = txtBankName.Text;
                                seller.ModifiedBy = currentUserName;
                                seller.ModifiedDate = DateTime.Now.Date;
                                seller.IFSCCode = txtIFSC.Text;
                                seller.SellerAddress = txtSellerAddress.Text;
                                seller.SellerLandline = txtLandline.Text;
                                seller.SellerMobile = txtMobile.Text;
                                seller.SellerName = txtSellerName.Text;
                                seller.TINNo = txtTinNo.Text;
                                seller.VATNo = txtVatNo.Text;
                                seller.SellerEmail = txtEmail.Text;
                                seller.IsActive = true;
                                context.SaveChanges();
                                lblMessage.Text = "Seller Updated Successfully";
                                lblMessage.ForeColor = Color.Green;
                            }
                            else
                            {
                                lblMessage.Text = "Seller Already Exist Choose Diffrent Name Or Mobile";
                                lblMessage.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Seller Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Seller ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Seller ID Not in right format";
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
                string sellerIDObj = Request.QueryString[Constants.querySellerID];

                if (!string.IsNullOrEmpty(sellerIDObj))
                {
                    int sellerID = Convert.ToInt32(sellerIDObj);
                    if (sellerID > 0)
                    {
                        var sellerCollection = context.Sellers.Where(x => x.IsActive==true && x.SellerID == sellerID);
                        if (sellerCollection.Count() > 0)
                        {
                            Database.Seller seller = sellerCollection.FirstOrDefault();
                            txtAccountHolderName.Text = seller.AccountHolderName;
                            txtAccountNo.Text = seller.AccountNo;
                            txtBankName.Text = seller.BankName;
                            txtIFSC.Text = seller.IFSCCode;
                            txtLandline.Text = seller.SellerLandline;
                            txtMobile.Text = seller.SellerMobile;
                            txtSellerAddress.Text = seller.SellerAddress;
                            txtSellerName.Text = seller.SellerName;
                            txtTinNo.Text = seller.TINNo;
                            txtVatNo.Text = seller.VATNo;
                            txtEmail.Text = seller.SellerEmail;

                            hdnSellerID.Value = Convert.ToString(seller.SellerID);
                        }
                        else
                        {
                            lblMessage.Text = "Seller Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Seller ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Seller ID Not in right format";
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