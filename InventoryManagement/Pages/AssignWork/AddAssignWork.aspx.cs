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
    public partial class AddAssignWork : System.Web.UI.Page
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
                    txtWorkAssignDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                    txtPaidDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
            }
        }

        protected void btnWorkAssign_Click(object sender, EventArgs e)
        {
            try
            {
                AssignWork();
                ClearForm();
                txtWorkAssignDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
                txtPaidDate.Text = DateTime.Now.ToString(Constants.DateFormatDatePicker);
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
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
            }
        }
        #endregion

        #region User Method
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

        public void AssignWork()
        {
            try
            {
                if(ddlEmployeeName.SelectedValue!="0" && ddlBillNo.SelectedValue!="0" && ddlWorkType.SelectedValue!="0")
                {
                    int empId =Convert.ToInt32(ddlEmployeeName.SelectedValue);
                    var BillNo = ddlBillNo.SelectedValue;
                    var workTypeID =Convert.ToInt32(ddlWorkType.SelectedValue);

                    //To Check same bill no and same work is not assign to emplyee
                    var workCollection = context.WorkAssigns.Where(x => x.EmployeeID == empId && x.BillNo == BillNo && x.WorkTypeID == workTypeID);
                    
                    if(workCollection.Count()==0)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            WorkAssign workAssign = new WorkAssign();
                            workAssign.BillNo = ddlBillNo.SelectedItem.Text;
                            workAssign.CompletedCount = txtCompletedCount.Text == "" ? 0 : Convert.ToInt32(txtCompletedCount.Text);
                            workAssign.CreatedBy = currentUserName;
                            workAssign.CreatedDate = DateTime.Now;
                            workAssign.EmployeeID = ddlEmployeeName.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlEmployeeName.SelectedValue);
                            if (!string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
                                workAssign.ExpectedCompletionDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text);
                            workAssign.IsComplete = chkIsWorkCompleted.Checked;
                            workAssign.Remarks = txtRemarks.Text;
                            if (!string.IsNullOrEmpty(txtWorkAssignDate.Text))
                                workAssign.WorkAssignDate = Convert.ToDateTime(txtWorkAssignDate.Text);
                            workAssign.WorkCount = txtItemCount.Text == "" ? 0 : Convert.ToInt32(txtItemCount.Text);
                            workAssign.WorkTypeID = ddlWorkType.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlWorkType.SelectedValue);
                            workAssign.IsActive = true;
                            workAssign.TotalCost = txtTotalCost.Text == "" ? 0 : Convert.ToInt32(txtTotalCost.Text);
                            var totalAmt = txtTotalCost.Text == "" ? 0 : Convert.ToInt32(txtTotalCost.Text);
                            var paidAmt = txtAmountPaid.Text == "" ? 0 : Convert.ToInt32(txtAmountPaid.Text);
                            var remAmount = totalAmt - paidAmt;
                            workAssign.RemainingCost = remAmount;
                            context.WorkAssigns.Add(workAssign);
                            context.SaveChanges();

                            Salary EmpSalary = new Salary();
                            EmpSalary.AmountPaid = paidAmt;
                            EmpSalary.AmountPaidDate = txtPaidDate.Text == "" ? DateTime.Now.Date : Convert.ToDateTime(txtPaidDate.Text);
                            EmpSalary.CreatedBy = currentUserName;
                            EmpSalary.CreatedDate = DateTime.Now;
                            EmpSalary.EmployeeID = ddlEmployeeName.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlEmployeeName.SelectedValue);
                            EmpSalary.Remarks = txtRemarks.Text;
                            EmpSalary.WorkAssignID = workAssign.WorkAssignID;
                            EmpSalary.IsActive = true;
                            context.Salaries.Add(EmpSalary);
                            context.SaveChanges();

                            if (rptrData.Items.Count > 0)
                            {
                                foreach (RepeaterItem rptrItem in rptrData.Items)
                                {
                                    if (rptrItem.Visible)
                                    {
                                        WorkAssignInventoryUsed workAssUID = new WorkAssignInventoryUsed();
                                        workAssUID.InventoryTypeID = Convert.ToInt32(((HiddenField)rptrItem.FindControl("hdnItemID")).Value);
                                        workAssUID.InventoryUsedCount = Convert.ToInt32(((Label)rptrItem.FindControl("lblQuantity")).Text);
                                        workAssUID.WorkAssignID = workAssign.WorkAssignID;
                                        workAssUID.CreatedBy = currentUserName;
                                        workAssUID.CreatedDate = DateTime.Now.Date;
                                        workAssUID.IsActive = true;
                                        context.WorkAssignInventoryUseds.Add(workAssUID);
                                        context.SaveChanges();

                                        Database.InventoryType currentInventory = context.InventoryTypes.Where(x => x.InventoryTypeId == workAssUID.InventoryTypeID).SingleOrDefault();
                                        if (currentInventory != null)
                                        {
                                            var currentQuantity = currentInventory.InventoryCount == null ? 0 : currentInventory.InventoryCount;
                                            currentInventory.InventoryCount = currentQuantity - workAssUID.InventoryUsedCount;
                                            currentInventory.ModifiedBy = currentUserName;
                                            currentInventory.ModifiedDate = DateTime.Now.Date;
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }

                            lblMessage.Text = "Assign Successfully";
                            lblMessage.ForeColor = Color.Green;
                            ClearForm();
                            scope.Complete();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Work is already assign to Employee";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Form is not properly filled";
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
                            //objNext.UnitName = ((Label)item.FindControl("lblUnit")).Text;
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
            public string UnitName { get; set; }
            public string Quantity { get; set; }
        }

        public void ClearForm()
        {
            txtAmountPaid.Text = "";
            txtCompletedCount.Text = "";
            txtExpectedDeliveryDate.Text = "";
            txtItemCount.Text = "";
            txtPaidDate.Text = "";
            txtQuantity.Text = "";
            txtRemarks.Text = "";
            txtTotalCost.Text = "";
            txtWorkAssignDate.Text = "";
            chkIsWorkCompleted.Checked = false;
            ddlBillNo.SelectedIndex = 0;
            ddlEmployeeName.SelectedIndex = 0;
            ddlInventoryType.SelectedIndex = 0;
            ddlWorkType.SelectedIndex = 0;
            rptrData.DataSource = null;
            rptrData.DataBind();
        }
        #endregion
    }
}