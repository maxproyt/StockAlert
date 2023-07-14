using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Notificador
{
    internal class FileReader
    {
        private string? smtp_client;
        private string? port;
        private string? email;
        private string? password;
        private string? fileString;
        private string? htmlBodySell;
        private string? htmlBodyBuy;
        private string  htmlSellPath = "..\\..\\..\\files\\Email_body_sell.html";
        private string  htmlBuyPath = "..\\..\\..\\files\\Email_body_buy.html";
        private string  smtpFilePath = "..\\..\\..\\files\\SMTP_Config_file.txt";
        private List<string> recipient_list = new List<string>();

        public FileReader()
        {
            try
            {
                if (File.Exists(smtpFilePath) && File.Exists(htmlSellPath) && File.Exists(htmlBuyPath))
                {
                    fileString = File.ReadAllText(smtpFilePath);
                    htmlBodySell = File.ReadAllText(htmlSellPath);
                    htmlBodyBuy = File.ReadAllText(htmlBuyPath);
                    string[] substring_array = fileString.Split(' ');
                    smtp_client = substring_array[0];
                    port = substring_array[1];
                    email = substring_array[2];
                    password = substring_array[3];


                    for (int i = 4; i < substring_array.Length; i++)
                    {
                        if(!string.IsNullOrEmpty(substring_array[i]))
                        {
                            recipient_list.Add(substring_array[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("A base de arquivos necessárias não está presente no diretório padrão: files\\");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public string SmtpClient => smtp_client;
        public string Port => port;
        public string Email => email;
        public string Password => password;
        public string HtmlBodySell => htmlBodySell;
        public string HtmlBodyBuy => htmlBodyBuy;
        public List<string> EmailList => recipient_list;

    }
}
