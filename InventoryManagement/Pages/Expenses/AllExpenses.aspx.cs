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
    public partial class AllExpenses : System.Web.UI.Page
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
                rptrExpense.DataSource = context.Expenses.Where(x => x.IsActive == true).AsEnumerable().Select(x => new
                {
                    ExpenseAmount = x.ExpenseAmount,
                    ExpenseBillNo = x.ExpenseBillNo,
                    ExpenseDate = x.ExpenseDate==null?"":Convert.ToDateTime(x.ExpenseDate).ToString(Constants.DateFormatDisplay),
                    ExpenseID = x.ExpenseID,
                }).ToList();
                rptrExpense.DataBind();
            }
            catch
            {
                throw;
            }
        }

        protected void rptrExpense_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string ExpenseID = Convert.ToString(e.CommandArgument);
                    Response.Redirect("EditExpense.aspx?" + Constants.queryExpenseID + "=" + ExpenseID,false);
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        //protected void gridExpense_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "Edit")
        //        {
        //            string ExpenseID = Convert.ToString(e.CommandArgument);
        //            Response.Redirect("EditExpense.aspx?" + Constants.queryExpenseID + "=" + ExpenseID);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogError(ex);
        //        lblMessage.Text = "Something went Wrong kindly check log";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void gridExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.Header)
        //            e.Row.TableSection = TableRowSection.TableHeader;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //protected void gridExpense_RowEditing(object sender, GridViewEditEventArgs e)
        //{

        //}



        //protected void gridExpense_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        gridExpense.PageIndex = e.NewPageIndex;
        //        BindGrid(e.NewPageIndex);
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogError(ex);
        //        lblMessage.Text = "Something went Wrong kindly check log";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //public void BindGrid(int pageIndex)
        //{
        //    try
        //    {
        //        int skipCount = gridExpense.PageSize * (pageIndex - 1);
        //        gridExpense.DataSource = context.Expenses.Where(x => x.IsActive == true).Skip(skipCount).Take(gridExpense.PageSize).Select(x => new
        //        {
        //            x.ExpenseAmount,
        //            x.ExpenseBillNo,
        //            x.ExpenseDate,
        //            x.ExpenseID,
        //        }).ToList();
        //        gridExpense.DataBind();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}