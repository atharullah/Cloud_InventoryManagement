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
    public partial class AllSellers : System.Web.UI.Page
    {
        InventoryManagementEntities context = new InventoryManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                BindGrid();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void BindGrid()
        {
            try
            {
                rptrSeller.DataSource = context.Sellers.Where(x=>x.IsActive==true).Select(x => new
                {
                    AccountHolderName = x.AccountHolderName,
                    AccountNo = x.AccountNo,
                    BankName = x.BankName,
                    IFSCCode = x.IFSCCode,
                    SellerAddress = x.SellerAddress,
                    SellerID = x.SellerID,
                    SellerLandline = x.SellerLandline,
                    SellerMobile = x.SellerMobile,
                    SellerName = x.SellerName,
                    TINNo = x.TINNo,
                    VATNo = x.VATNo,
                    SellerEmail = x.SellerEmail
                }).OrderBy(x=>x.SellerName).ToList();

                rptrSeller.DataBind();
            }
            catch
            {
                throw;
            }
        }

        //protected void gridSeller_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        gridSeller.PageIndex = e.NewPageIndex;
        //        BindGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogError(ex);
        //        lblMessage.Text = "Something went Wrong kindly check log";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void gridSeller_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "Edit")
        //        {
        //            string SellerID = Convert.ToString(e.CommandArgument);
        //            Response.Redirect("EditSeller.aspx?" + Constants.querySellerID + "=" + SellerID);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogError(ex);
        //        lblMessage.Text = "Something went Wrong kindly check log";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        protected void rptrSeller_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string SellerID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditSeller.aspx?" + Constants.querySellerID + "=" + SellerID,false);
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}