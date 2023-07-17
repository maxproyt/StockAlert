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
    /// <summary>
    /// Classe responsável pelo envio de emails pelo protocolo SMTP, é dependente da classe FileReader para obter
    /// as informações para o envio do e-mail
    /// </summary>
    internal class EmailSender
    {
        
        private FileReader fileData = new();
        private MailMessage mailMessage = new();

        /// <summary>
        /// Inializa e preenche a mensagem de e-mail com as informações do arquivo de configuração, também adiciona os destinatários
        /// à lista de envio do e-mail.
        /// </summary>
        /// <param name="ativo">O nome do ativo que será utilizado no título do e-mail.</param>
        /// <param name="ação">Flag booleana que determina compra ou venda, sendo true para venda e false para compra.</param>
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

        /// <summary>
        /// Envia o e-mail através do protocolo SMTP.
        /// </summary>
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
