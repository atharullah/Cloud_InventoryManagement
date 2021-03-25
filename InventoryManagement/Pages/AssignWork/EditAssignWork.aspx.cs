using InventoryManagement.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryManagement.Common;
using System.Transactions;

namespace InventoryManagement.Pages.AssignWork
{
    public partial class EditAssignWork : System.Web.UI.Page
    {
        #region Page Member
        string currentUserName = string.Empty;
        InventoryManagementEntities context = new InventoryManagementEntities();
        #endregion

        #region Page Method
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentUserName = Convert.ToString(Session[Constants.SessionUserName]);
                if (!IsPostBack)
                {
                    BindDropDown();
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

        protected void btnWorkAssign_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateAssignWork();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void rptrData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    RepeaterItem item = e.Item;
                    ddlInventoryType.SelectedValue = ((HiddenField)item.FindControl("hdnItemID")).Value;
                    txtQuantity.Text = ((Label)item.FindControl("lblQuantity")).Text;
                    hdnItemIndex.Value = Convert.ToString(e.Item.ItemIndex);
                    lblMessage.Text = "";
                }

                if (e.CommandName == "Delete")
                {
                    e.Item.Visible = false;
                    List<rptrClass> data = BindRepeater();
                    rptrData.DataSource = data;
                    rptrData.DataBind();
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            try
            {
                List<rptrClass> data = new List<rptrClass>();
                rptrClass obj = new rptrClass();
                obj.ItemID = ddlInventoryType.SelectedValue;
                obj.ItemName = ddlInventoryType.SelectedItem.Text;
                obj.Quantity = txtQuantity.Text;
                data.Add(obj);
                if (rptrData.Items.Count > 0)
                {
                    data = BindRepeater(data);
                }
                rptrData.DataSource = data;
                rptrData.DataBind();
                ClearRepeater();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
                lblMessage.Text = "Something went Wrong kindly check log";
                lblMessage.ForeColor = Color.Red;
            }
        }
        #endregion

        #region User Method

        public void FillForm()
        {
            try
            {
                string assignWorkIDObj = Request.QueryString[Constants.queryAssignWorkID];

                if (!string.IsNullOrEmpty(assignWorkIDObj))
                {
                    int assignWorkID = Convert.ToInt32(assignWorkIDObj);
                    if (assignWorkID > 0)
                    {
                        var assignWorkCollection = context.WorkAssigns.Where(x => x.IsActive == true && x.WorkAssignID == assignWorkID);
                        if (assignWorkCollection.Count() > 0)
                        {
                            Database.WorkAssign workAssign = assignWorkCollection.FirstOrDefault();

                            txtCompletedCount.Text = Convert.ToString(workAssign.CompletedCount);
                            txtExpectedDeliveryDate.Text = workAssign.ExpectedCompletionDate == null ? "" : workAssign.ExpectedCompletionDate.Value.ToString(Constants.DateFormatDatePicker);
                            txtItemCount.Text = Convert.ToString(workAssign.WorkCount);
                            txtRemarks.Text = workAssign.Remarks;
                            txtTotalCost.Text = Convert.ToString(workAssign.TotalCost);
                            txtWorkAssignDate.Text = workAssign.WorkAssignDate == null ? "" : workAssign.WorkAssignDate.Value.ToString(Constants.DateFormatDatePicker);
                            ddlBillNo.SelectedValue = Convert.ToString(workAssign.BillNo);
                            ddlEmployeeName.SelectedValue = Convert.ToString(workAssign.EmployeeID);
                            ddlWorkType.SelectedValue = Convert.ToString(workAssign.WorkTypeID);

                            decimal? totalCost = workAssign.TotalCost;
                            decimal? remainingCost = workAssign.RemainingCost;
                            if (totalCost != null && remainingCost != null)
                            {
                                txtAmountPaid.Text = Convert.ToString(totalCost - remainingCost);
                            }
                            var date = workAssign.ModifiedDate == null ? workAssign.CreatedDate : workAssign.ModifiedDate;
                            txtPaidDate.Text = date.Value.ToString(Constants.DateFormatDatePicker);
                            chkIsWorkCompleted.Checked = workAssign.IsComplete == null ? false : Convert.ToBoolean(workAssign.IsComplete);

                            hdnAssignWorkID.Value = Convert.ToString(workAssign.WorkAssignID);

                            var inventoryUsed = context.WorkAssignInventoryUseds.Where(x => x.IsActive == true && x.WorkAssignID == workAssign.WorkAssignID).AsEnumerable();
                            var collection = from invUsed in inventoryUsed
                                             join inv in context.InventoryTypes.Where(x => x.IsActive == true).AsEnumerable() on invUsed.InventoryTypeID equals inv.InventoryTypeId
                                             select new rptrClass
                                             {
                                                 ItemID = Convert.ToString(invUsed.InventoryTypeID),
                                                 ItemName = inv.InventoryTypeName,
                                                 Quantity = Convert.ToString(invUsed.InventoryUsedCount),
                                                 WorkAssignInventoryUsedID = Convert.ToString(invUsed.WorkAssignInventoryUsedID)
                                             };

                            rptrData.DataSource = collection.ToList();
                            rptrData.DataBind();
                        }
                        else
                        {
                            lblMessage.Text = "Work ID Not Found";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Work ID Not Valid";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Work ID Not in right format";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch
            {
                throw;
            }
        }

        public void BindDropDown()
        {
            try
            {
                ddlEmployeeName.DataSource = context.Employees.Where(x => x.IsActive == true).Select(x => new { x.EmployeeName, x.EmployeeID }).ToList();
                ddlEmployeeName.DataTextField = "EmployeeName";
                ddlEmployeeName.DataValueField = "EmployeeID";
                ddlEmployeeName.DataBind();
                ddlEmployeeName.Items.Insert(0, new ListItem("Select", "0"));

                ddlBillNo.DataSource = context.SalesOrders.Where(x => x.IsActive == true).Where(x => x.IsMakingRequired == true && x.IsCompleted != true).Select(x => new { x.BillNo }).ToList();
                ddlBillNo.DataTextField = "BillNo";
                ddlBillNo.DataValueField = "BillNo";
                ddlBillNo.DataBind();
                ddlBillNo.Items.Insert(0, new ListItem("Select", "0"));

                ddlWorkType.DataSource = context.WorkTypes.Where(x => x.IsActive == true).Select(x => new { x.WorkTypeID, x.WorkTypeName }).ToList();
                ddlWorkType.DataTextField = "WorkTypeName";
                ddlWorkType.DataValueField = "WorkTypeID";
                ddlWorkType.DataBind();

                var iventoryTypeList = context.InventoryTypes.Where(x => x.IsActive == true).Select(x => new { x.InventoryTypeId, x.InventoryTypeName }).ToList();
                ddlInventoryType.DataTextField = "InventoryTypeName";
                ddlInventoryType.DataValueField = "InventoryTypeId";
                ddlInventoryType.DataSource = iventoryTypeList;
                ddlInventoryType.DataBind();

            }
            catch
            {
                throw;
            }
        }

        public void UpdateAssignWork()
        {
            try
            {
                string workAssignIDObj = hdnAssignWorkID.Value;
                if (!string.IsNullOrEmpty(workAssignIDObj))
                {
                    int workAssignID = Convert.ToInt32(workAssignIDObj);
                    var workCollection = context.WorkAssigns.Where(x => x.IsActive == true && x.WorkAssignID == workAssignID);
                    if (workCollection.Count() > 0)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            Database.WorkAssign workAssign = workCollection.FirstOrDefault();
                            workAssign.BillNo = ddlBillNo.SelectedItem.Text;
                            workAssign.CompletedCount = txtCompletedCount.Text == "" ? 0 : Convert.ToInt32(txtCompletedCount.Text);
                            workAssign.ModifiedBy = currentUserName;
                            workAssign.ModifiedDate = DateTime.Now;
                            workAssign.EmployeeID = Convert.ToInt32(ddlEmployeeName.SelectedValue);
                            if (!string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
                                workAssign.ExpectedCompletionDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text);
                            else
                                workAssign.ExpectedCompletionDate = null;
                            workAssign.IsComplete = chkIsWorkCompleted.Checked;
                            workAssign.Remarks = txtRemarks.Text;
                            if (!string.IsNullOrEmpty(txtWorkAssignDate.Text))
                                workAssign.WorkAssignDate = Convert.ToDateTime(txtWorkAssignDate.Text);
                            else
                                workAssign.WorkAssignDate = null;
                            workAssign.WorkCount = txtItemCount.Text == "" ? 0 : Convert.ToInt32(txtItemCount.Text);
                            workAssign.WorkTypeID = Convert.ToInt32(ddlWorkType.SelectedValue);
                            workAssign.IsActive = true;
                            workAssign.TotalCost = txtTotalCost.Text == "" ? 0 : Convert.ToDecimal(txtTotalCost.Text);
                            var totalAmt = txtTotalCost.Text == "" ? 0 : Convert.ToDecimal(txtTotalCost.Text);
                            var paidAmt = txtAmountPaid.Text == "" ? 0 : Convert.ToDecimal(txtAmountPaid.Text);
                            var remAmount = totalAmt - paidAmt;
                            workAssign.RemainingCost = remAmount;
                            context.SaveChanges();

                            if (rptrData.Items.Count > 0)
                            {
                                foreach (RepeaterItem rptrItem in rptrData.Items)
                                {
                                    if (rptrItem.Visible)
                                    {
                                        int workAssignInventoryUsedID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("hdnWorkassignUsedID")).Value);
                                        var invUsedObj = context.WorkAssignInventoryUseds.Where(x => x.IsActive == true && x.WorkAssignInventoryUsedID == workAssignInventoryUsedID);
                                        if (invUsedObj.Count() > 0)
                                        {
                                            WorkAssignInventoryUsed workAssUID = invUsedObj.SingleOrDefault();
                                            int? oldCount = workAssUID.InventoryUsedCount;
                                            workAssUID.InventoryTypeID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("hdnItemID")).Value);
                                            workAssUID.InventoryUsedCount = Convert.ToInt32(((Label)rptrItem.FindControl("lblQuantity")).Text);
                                            workAssUID.ModifiedBy = currentUserName;
                                            workAssUID.ModifiedDate = DateTime.Now.Date;
                                            workAssUID.IsActive = true;
                                            workAssUID.WorkAssignID = workAssign.WorkAssignID;
                                            context.SaveChanges();

                                            Database.InventoryType currentInventory = context.InventoryTypes.Where(x => x.IsActive == true && x.InventoryTypeId == workAssUID.InventoryTypeID).SingleOrDefault();
                                            if (currentInventory != null)
                                            {
                                                var currentQuantity = currentInventory.InventoryCount == null ? 0 : currentInventory.InventoryCount;
                                                currentInventory.InventoryCount = currentQuantity + (oldCount - workAssUID.InventoryUsedCount);
                                                context.SaveChanges();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int workAssignInventoryUsedID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("hdnWorkassignUsedID")).Value);
                                        var invUsedObj = context.WorkAssignInventoryUseds.Where(x => x.IsActive == true && x.WorkAssignInventoryUsedID == workAssignInventoryUsedID);
                                        if (invUsedObj.Count() > 0)
                                        {
                                            WorkAssignInventoryUsed workAssUID = invUsedObj.SingleOrDefault();
                                            workAssUID.ModifiedBy = currentUserName;
                                            workAssUID.ModifiedDate = DateTime.Now.Date;
                                            workAssUID.IsActive = false;
                                            context.SaveChanges();

                                            Database.InventoryType currentInventory = context.InventoryTypes.Where(x => x.IsActive == true && x.InventoryTypeId == workAssUID.InventoryTypeID).SingleOrDefault();
                                            if (currentInventory != null)
                                            {
                                                var currentQuantity = currentInventory.InventoryCount == null ? 0 : currentInventory.InventoryCount;
                                                currentInventory.InventoryCount = currentQuantity + workAssUID.InventoryUsedCount;
                                                context.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }

                            var salCollection = context.Salaries.Where(x => x.IsActive == true && x.WorkAssignID == workAssign.WorkAssignID);
                            if (salCollection.Count() > 0)
                            {
                                Salary EmpSalary = salCollection.FirstOrDefault();
                                EmpSalary.AmountPaid = txtAmountPaid.Text == "" ? 0 : Convert.ToDecimal(txtAmountPaid.Text);
                                EmpSalary.AmountPaidDate = txtPaidDate.Text == "" ? DateTime.Now.Date : Convert.ToDateTime(txtPaidDate.Text);
                                EmpSalary.ModifiedBy = currentUserName;
                                EmpSalary.ModifiedDate = DateTime.Now;
                                EmpSalary.EmployeeID = Convert.ToInt32(ddlEmployeeName.SelectedValue);
                                EmpSalary.Remarks = txtRemarks.Text;
                                EmpSalary.WorkAssignID = workAssign.WorkAssignID;
                                EmpSalary.IsActive = true;
                                context.SaveChanges();
                            }

                            lblMessage.Text = "Assign Successfully";
                            lblMessage.ForeColor = Color.Green;
                            scope.Complete();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Work Assign ID Not Found ";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Work Assign ID Not in proper format ";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<rptrClass> BindRepeater(List<rptrClass> data = null)
        {
            try
            {
                if (data == null)
                    data = new List<rptrClass>();
                int editItemIndex = hdnItemIndex.Value == "" ? -1 : Convert.ToInt32(hdnItemIndex.Value);
                foreach (RepeaterItem item in rptrData.Items)
                {
                    //for deleting of repeater row
                    if (item.Visible)
                    {
                        //To check edit button press so not add record
                        if (item.ItemIndex != editItemIndex)
                        {
                            rptrClass objNext = new rptrClass();
                            objNext.ItemID = ((HiddenField)item.FindControl("hdnItemID")).Value;
                            objNext.ItemName = ((Label)item.FindControl("lblItemName")).Text;
                            objNext.Quantity = ((Label)item.FindControl("lblQuantity")).Text;
                            objNext.WorkAssignInventoryUsedID = ((HiddenField)item.FindControl("hdnWorkassignUsedID")).Value;
                            data.Add(objNext);
                        }
                    }
                }
                hdnItemIndex.Value = "-1";
                return data;
            }
            catch
            {
                throw;
            }
        }

        public void ClearRepeater()
        {
            try
            {
                txtQuantity.Text = "";
            }
            catch
            {
                throw;
            }
        }

        public class rptrClass
        {
            public string ItemName { get; set; }
            public string ItemID { get; set; }
            public string Quantity { get; set; }
            public string WorkAssignInventoryUsedID { get; set; }
        }

        #endregion
    }
}