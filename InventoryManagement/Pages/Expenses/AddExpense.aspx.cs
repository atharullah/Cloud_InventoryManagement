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
    public partial class AddExpense : System.Web.UI.Page
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
                    txtExpenseBillNo.Text = GetNextBillNo();
                    txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                    
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
                Database.Expense expense = new Database.Expense();
                expense.CreatedBy = currentUserName;
                expense.CreatedDate = DateTime.Now.Date;
                expense.ExpenseAmount =Convert.ToInt32(txtExpenseAmount.Text);
                expense.ExpenseBillNo = txtExpenseBillNo.Text;
                expense.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text);
                expense.ExpenseDescription = txtExpenseDescription.Text;
                expense.IsActive = true;
                context.Expenses.Add(expense);
                context.SaveChanges();

                lblMessage.Text = "Expense Added Successfully";
                lblMessage.ForeColor = Color.Green;
                ClearForm();
                txtExpenseBillNo.Text = GetNextBillNo();
                txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public string GetNextBillNo()
        {
            try
            {
                string billStartTag = Helper.GetConfigValue(Constants.ConfigSellingBillStartLabel);
                var expenseLst = context.Expenses.ToList();
                if (expenseLst.Count() > 0)
                {
                    var lastExpense = expenseLst.OrderByDescending(x => x.ExpenseID).First();
                    if (lastExpense != null)
                        return billStartTag + lastExpense.ExpenseID;
                    else
                        return billStartTag + "01";
                }
                else
                    return billStartTag + "01";
            }
            catch
            {
                throw;
            }
        }

        public void ClearForm()
        {
            txtExpenseAmount.Text = "";
            txtExpenseBillNo.Text = "";
            txtExpenseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtExpenseDescription.Text = "";
        }
    }
}