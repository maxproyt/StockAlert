using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace Notificador
{
    internal class EmailSender
    {
        private string mail;
        private string smtp_server;
        private string subject;
        
        public EmailSender(string mail) {
            this.mail = mail;
        }
    }
}
