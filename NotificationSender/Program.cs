using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSender
{
   public class Program
    {
        static InventoryManagementEntities context = new InventoryManagementEntities();

        static void Main(string[] args)
        {
            try
            {
                var configObj = ConfigurationManager.AppSettings[Constants.DayBeforeDelivery];
                if (configObj != null)
                {
                    int NoOfDaysBefore = Convert.ToInt32(configObj);
                    DateTime futureDeliveryDate = DateTime.Now.AddDays(NoOfDaysBefore);
                    string SmsTemplate = ConfigurationManager.AppSettings[Constants.SentSmsTemplate];
                    var recordCollection = context.SalesOrders.Where(x => x.IsMakingRequired == true && x.IsCompleted == false).ToList();

                    foreach (var record in recordCollection)
                    {
                        if (record.Mobile != "")
                        {
                            int saleOrderID = record.SaleOrderID;
                            var makingCollection = context.DevelopmentWorks.Where(x => x.SaleOrderID == saleOrderID && x.DeliveryDate == futureDeliveryDate);
                            foreach (var makeRecord in makingCollection)
                            {
                                string recipientMobileNo = record.Mobile;
                                SmsTemplate = SmsTemplate.Replace(Constants.BillNo_Hash, makeRecord.BillNo);
                                SmsTemplate = SmsTemplate.Replace(Constants.PaidAmt_Hash, Convert.ToString(record.PaidAmount));
                                SmsTemplate = SmsTemplate.Replace(Constants.TotalAmt_Hash, Convert.ToString(record.TotalCost));

                                sendSMS(recipientMobileNo, SmsTemplate);
                                Console.WriteLine("SMS sent to mobile : " + recipientMobileNo);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Configuration is not set contact admin for support");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogError(ex.Message);
            }
        }

        public static void sendSMS(string PhoneNumber, string content)
        {
            try
            {
                string SiteUrl = ConfigurationManager.AppSettings[Constants.SiteUrl];
                string UserId = ConfigurationManager.AppSettings[Constants.UserId];
                string Password = ConfigurationManager.AppSettings[Constants.Password];
                string SenderID = ConfigurationManager.AppSettings[Constants.SenderID];

                SiteUrl = SiteUrl.Replace(Constants.userId_Hash, UserId);
                SiteUrl = SiteUrl.Replace(Constants.password_Hash, Password);
                SiteUrl = SiteUrl.Replace(Constants.senderID_Hash, SenderID);
                SiteUrl = SiteUrl.Replace(Constants.smsContent_Hash, content);
                SiteUrl = SiteUrl.Replace(Constants.receipientNo_Hash, PhoneNumber);

                WebRequest request = HttpWebRequest.Create(SiteUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readstream = new StreamReader(s);
                string datastring = readstream.ReadToEnd();
                response.Close();
                //response.Dispose();
                s.Close();
                readstream.Close();
            }
            catch (Exception ex)
            {
                //To not break page if email not sent
                LogError(ex.Message);
            }
        }

        public class Constants
        {
            public const string SiteUrl = "SiteUrl";
            public const string UserId = "UserId";
            public const string Password = "Password";
            public const string SenderID = "SenderID";
            public const string SentSmsTemplate = "SentSmsTemplate";
            public const string LogFilePath = "LogFilePath";

            public const string userId_Hash = "#userId#";
            public const string password_Hash = "#password#";
            public const string senderID_Hash = "#senderID#";
            public const string receipientNo_Hash = "#receipientno#";
            public const string smsContent_Hash = "#receipientno#";
            public const string DayBeforeDelivery = "DayBeforeDelivery";

            public const string BillNo_Hash = "#BillNo#";
            public const string PaidAmt_Hash = "#PaidAmount#";
            public const string TotalAmt_Hash = "#TotalAmount#";
        }

        public static void LogError(string errorMsg)
        {
            try
            {
                string logFilePath = ConfigurationManager.AppSettings[Constants.LogFilePath];
                if (!File.Exists(logFilePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                }

                StreamWriter writer = new StreamWriter(logFilePath);
                writer.WriteLine("Date          : " + DateTime.Now.ToShortDateString());
                writer.WriteLine("Time          : " + DateTime.Now.ToLongTimeString());
                writer.WriteLine(errorMsg);
                writer.WriteLine("^^-------------------------------------------------------------------^^");
                writer.Flush();
                writer.Close();
            }
            catch (Exception)
            {
                //Handle
            }
        }
    }
}
