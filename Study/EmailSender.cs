using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit;
using MimeKit;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Globalization;
using System.Net;

namespace Notificador
{
    internal class EmailSender
    {
        
        private FileReader fileData = new();
        private MailMessage mailMessage = new();

        public EmailSender(string asset, bool action) {
            try
            {
                mailMessage.From = new MailAddress(fileData.Email);
                if (action)
                {
                    mailMessage.Subject = "Sua ação " + asset + " atingiu o ponto de VENDA! StockAlert";
                    mailMessage.Body = fileData.HtmlBodySell;
                    mailMessage.IsBodyHtml = true;
                    for(int i = 0; i < fileData.EmailList.Count; i++)
                    {
                        mailMessage.To.Add(new MailAddress(fileData.EmailList[i]));
                    }
                }
                else 
                {
                    mailMessage.Subject = "Sua ação " + asset + " atingiu o ponto de COMPRA! StockAlert";
                    mailMessage.Body = fileData.HtmlBodyBuy;
                    mailMessage.IsBodyHtml = true;
                    for (int i = 0; i < fileData.EmailList.Count; i++)
                    {
                        mailMessage.To.Add(new MailAddress(fileData.EmailList[i]));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }  
        }
        public void SendMailSmtp()
        {
            try
            {
                var smtpClient = new SmtpClient(fileData.SmtpClient)
                {
                    Port = int.Parse(fileData.Port),
                    Credentials = new NetworkCredential(fileData.Email, fileData.Password),
                    EnableSsl = true,
                };
                smtpClient.Send(mailMessage);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
