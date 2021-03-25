using InventoryManagement.Common;
using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagement.Pages.Expenses
{
    public partial class EditExpense : System.Web.UI.Page
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
                string ExpenseIDObj = hdnExpenseID.Value;
                if (!string.IsNullOrEmpty(ExpenseIDObj))
                {
                    int ExpenseID = Convert.ToInt32(ExpenseIDObj);
                    if (ExpenseID > 0)
                    {
                        var ExpenseObj = context.Expenses.Where(x =>x.IsActive==true && x.ExpenseID == ExpenseID);
                        if (ExpenseObj.Count() > 0)
                        {
                            Database.Expense expense = ExpenseObj.FirstOrDefault();

                            string amount = txtExpenseAmount.Text;
                            expense.ExpenseAmount = amount == "" ? 0 : Convert.ToInt32(amount);
                            expense.ExpenseBillNo = txtExpenseBillNo.Text;
                            expense.ExpenseDate = txtExpenseDate.Text == "" ? DateTime.Now.Date : Convert.ToDateTime(txtExpenseDate.Text);
                            expense.ExpenseDescription = txtExpenseDescription.Text;
                            expense.ModifiedBy = currentUserName;
                            expense.ModifiedDate = DateTime.Now.Date;
                            context.SaveChanges();

                            lblMessage.Text = "Expense Updated Successfully";
                            lblMessage.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblMessage.Text = "Expense Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Expense ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Expense ID Not in right format";
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
                string ExpenseIDObj = Request.QueryString[Constants.queryExpenseID];

                if (!string.IsNullOrEmpty(ExpenseIDObj))
                {
                    int ExpenseID = Convert.ToInt32(ExpenseIDObj);
                    if (ExpenseID > 0)
                    {
                        var ExpenseCollection = context.Expenses.Where(x => x.IsActive==true && x.ExpenseID == ExpenseID);
                        if (ExpenseCollection.Count() > 0)
                        {
                            Database.Expense expense = ExpenseCollection.FirstOrDefault();
                            txtExpenseAmount.Text = Convert.ToString(expense.ExpenseAmount);
                            txtExpenseBillNo.Text = expense.ExpenseBillNo;
                            txtExpenseDate.Text = expense.ExpenseDate.GetValueOrDefault(DateTime.Now.Date).ToString("yyyy-MM-dd");
                            txtExpenseDescription.Text = expense.ExpenseDescription;
                            hdnExpenseID.Value = Convert.ToString(expense.ExpenseID);
                        }
                        else
                        {
                            lblMessage.Text = "Expense Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Expense ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Expense ID Not in right format";
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