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
        private string email;
        private string smtpClientData;
        private string port;
        private string password;
        private FileReader fileData = new();
        private List<string> customerEmail = new();
        private MailMessage mailMessage = new();

        public EmailSender(string asset, bool action) {
            
            email = fileData.Email;
            smtpClientData = fileData.SmtpClient;
            password = fileData.Password;
            port = fileData.Port;
            customerEmail = fileData.EmailList;
            
            try
            {
                mailMessage.From = new MailAddress(email);
                if (action)
                {
                    mailMessage.Subject = "Sua ação " + asset + " atingiu o ponto de VENDA! StockAlert";
                    mailMessage.Body = "<html><body> Hora de vender a sua ação<body><html>";
                    mailMessage.IsBodyHtml = true;
                    for(int i = 0; i < customerEmail.Count; i++)
                    {
                        mailMessage.To.Add(new MailAddress(customerEmail[i]));
                    }
                }
                else 
                {
                    mailMessage.Subject = "Sua ação " + asset + " atingiu o ponto de COMPRA! StockAlert";
                    mailMessage.Body = "<html><body> Hora de comprar a sua ação<body><html>";
                    mailMessage.IsBodyHtml = true;
                    for (int i = 0; i < customerEmail.Count; i++)
                    {
                        mailMessage.To.Add(new MailAddress(customerEmail[i]));
                    }
                }
                var smtpClient = new SmtpClient(smtpClientData)
                {
                    Port = int.Parse(this.port),
                    Credentials = new NetworkCredential(email, password),
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
