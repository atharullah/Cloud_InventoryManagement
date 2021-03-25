using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Common
{
    public class Constants
    {
        public const string SiteUrl = "SiteUrl";
        public const string UserId = "UserId";
        public const string Password = "Password";
        public const string SenderID = "SenderID";

        public const string userId_Hash = "#userId#";
        public const string password_Hash = "#password#";
        public const string senderID_Hash = "#senderID#";
        public const string receipientNo_Hash = "#receipientno#";
        public const string smsContent_Hash = "#receipientno#";

       

        //Seller Query String
        public const string querySellerID = "querySellerID";
        public const string queryCustomerID = "queryCustomerID";
        public const string queryUserID = "queryUserID";
        public const string queryPurchaseOrderID = "queryPurchaseOrderID";
        public const string queryInventoryID = "queryInventoryID";
        public const string queryEmployeeID = "queryEmployeeID";
        public const string queryExpenseID = "queryExpenseID";
        public const string querySaleOrderID = "querySaleOrderID";
        public const string queryAssignWorkID = "queryAssignWorkID";

        //App Setting Keys
        public const string ConfigLogFilePath = "LogFilePath";
        public const string ConfigPurchaseBillStartLabel = "PurchaseBillStartLabel";
        public const string ConfigSellingBillStartLabel = "SellingBillStartLabel";
        public const string ConfigDesignPhotoPath = "DesignPhotoPath";
        public const string ConfigDesignPhotoURL = "DesignPhotoURL";
        public const string ConfigCompanyName = "CompanyName";
        public const string ConfigCompanyAddress = "CompanyAddress";
        

        //After login redirect
        public const string URLSaleOrder = @"/Pages/SaleOrder/SaleOrder.aspx";
        public const string URLLogIn = @"/Pages/Main/LogIn.aspx";

        //Session Variable
        public const string SessionRoleID = "Role";
        public const string SessionUserName = "UserName";

        //Date picker DateFormat
        public const string DateFormatDatePicker = "yyyy-MM-dd";
        public const string DateFormatDisplay = "dd MMM yyyy";
    }
}