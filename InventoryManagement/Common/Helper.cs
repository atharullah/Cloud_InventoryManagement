using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;

namespace InventoryManagement.Common
{
    public class Helper
    {
        public static bool sendMail(string toEmail, string Body, string subject)
        {
            try
            {
                string smtpServer = Convert.ToString(ConfigurationManager.AppSettings["Smtp"]);
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                string UserName = Convert.ToString(ConfigurationManager.AppSettings["UserName"]);
                string Password = Convert.ToString(ConfigurationManager.AppSettings["Password"]);

                MailMessage mail = new MailMessage();
                mail.Body = Body;
                mail.From = new MailAddress(UserName);
                mail.Subject = subject;
                mail.To.Add(toEmail);
                SmtpClient client = new SmtpClient(smtpServer, smtpPort);
                client.Credentials = new NetworkCredential(UserName, Password);
                client.EnableSsl = true;
                client.Send(mail);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void sendSMS(string PhoneNumber, string content)
        {
            try
            {
                string UserId, Password, SenderID, SiteUrl;

                SiteUrl = ConfigurationManager.AppSettings[Constants.SiteUrl];
                UserId = ConfigurationManager.AppSettings[Constants.UserId];
                Password = ConfigurationManager.AppSettings[Constants.Password];
                SenderID = ConfigurationManager.AppSettings[Constants.SenderID];

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
                //Handle Exception
                Helper.LogError(ex);
            }
        }

        public string saveEncFile()
        {
            var macAddr = (
                            from nic in NetworkInterface.GetAllNetworkInterfaces()
                            where nic.OperationalStatus == OperationalStatus.Up
                            select nic.GetPhysicalAddress().ToString()
                          ).FirstOrDefault();

            string mName = Environment.MachineName;
            string finalString = macAddr + mName;
            string filePath = Environment.GetEnvironmentVariable("windir") + "\\systemstarter.txt";

            StreamWriter writer = new StreamWriter(filePath);
            writer.WriteLine(finalString.GetHashCode());
            writer.Flush();
            writer.Close();
            return finalString.GetHashCode().ToString();
        }

        public string GetEncFileString()
        {
            try
            {
                var macAddr = (
                                    from nic in NetworkInterface.GetAllNetworkInterfaces()
                                    where nic.OperationalStatus == OperationalStatus.Up
                                    select nic.GetPhysicalAddress().ToString()
                                  ).FirstOrDefault();

                string mName = Environment.MachineName;
                string finalString = macAddr + mName;
                string filePath = Environment.GetEnvironmentVariable("windir") + "\\systemstarter.txt";

                StreamReader reader = new StreamReader(filePath);
                string readString = reader.ReadLine();
                reader.Close();
                return readString;
            }
            catch
            {                
                throw;
            }
        }

        public static void LogError(Exception ex)
        {
            try
            {
                string logFilePath = ConfigurationManager.AppSettings[Constants.ConfigLogFilePath];
                if (!File.Exists(logFilePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                }

                StreamWriter writer = new StreamWriter(logFilePath, true);
                writer.WriteLine("Date          : " + DateTime.Now.ToShortDateString());
                writer.WriteLine("Time          : " + DateTime.Now.ToLongTimeString());
                writer.WriteLine(ex.ToString());
                writer.WriteLine("^^-------------------------------------------------------------------^^");
                writer.Flush();
                writer.Close();
            }
            catch (Exception)
            {
                //Handle
            }
        }

        public static string GetConfigValue(string key)
        {
            try
            {
                object configObj = ConfigurationManager.AppSettings[key];
                return Convert.ToString(configObj);
            }
            catch
            {
                throw;
            }
        }
    }
}