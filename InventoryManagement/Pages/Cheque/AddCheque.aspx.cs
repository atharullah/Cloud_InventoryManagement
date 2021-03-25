using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Cheque
{
    public partial class AddCheque : System.Web.UI.Page
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
                var ChequeCollection = context.Cheques.Where(x => x.IsActive==true && x.ChequeNo == txtChequeNo.Text && x.PaymentBillNo == txtBillNo.Text);
                if (ChequeCollection.Count() == 0)
                {
                    Database.Cheque cheque = new Database.Cheque();
                    cheque.Amount =txtAmount.Text == "" ? 0 : Convert.ToInt32(txtAmount.Text);
                    cheque.CreatedBy = currentUserName;
                    cheque.CreatedDate = DateTime.Now.Date;
                    cheque.ChequeDueDate = Convert.ToDateTime(txtChequeDueDate.Text);
                    cheque.ChequeNo = txtChequeNo.Text;
                    cheque.IsActive = true;
                    cheque.IsChequeClear = chkChequeClear.Checked;
                    cheque.IssueDate =Convert.ToDateTime(txtChequeIssueDate.Text);
                    cheque.PaymentBillNo = txtBillNo.Text;
                    context.Cheques.Add(cheque);
                    context.SaveChanges();

                    lblMessage.Text = "Cheque Added Successfully";
                    lblMessage.ForeColor = Color.Green;
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Cheque Already exist with same name and Mobile";
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
            txtAmount.Text = "";
            txtBillNo.Text = "";
            txtChequeDueDate.Text = "";
            txtChequeIssueDate.Text = "";
            txtChequeNo.Text = "";
            chkChequeClear.Checked = false;
        }
    }
}