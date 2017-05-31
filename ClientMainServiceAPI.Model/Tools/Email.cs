using System;
using System.Configuration;
using System.Net.Mail;

namespace ClientMainServiceAPI.Model.Tools
{
    public static class Email
    {
        public static void SendEmail(string to, string diplayNameTo, string subject, string body, int aplication)
        {
            SmtpClient client = null;
            MailMessage mail = null;
            try
            {
                client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(Environment.GetEnvironmentVariable(ConfigurationManager.AppSettings["email-send-app" + aplication]),
                    Environment.GetEnvironmentVariable(ConfigurationManager.AppSettings["password-send-app" + aplication]));
                mail = new MailMessage();
                mail.Sender = new MailAddress(Environment.GetEnvironmentVariable(ConfigurationManager.AppSettings["email-send-app" + aplication]), 
                    Environment.GetEnvironmentVariable(ConfigurationManager.AppSettings["display-email-send-app" + aplication]));
                mail.From = new MailAddress(Environment.GetEnvironmentVariable(ConfigurationManager.AppSettings["email-send-app" + aplication]), 
                    Environment.GetEnvironmentVariable(ConfigurationManager.AppSettings["display-email-send-app" + aplication]));
                mail.To.Add(new MailAddress(to, diplayNameTo));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                client.Send(mail);
            }
            finally
            {
                mail = null;
                client = null;
            }
        }
    }
}
