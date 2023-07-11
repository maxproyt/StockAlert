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
        private string? file_string;
        private List<string> recipient_list = new List<string>();

        public FileReader()
        {
            try
            {
                if (File.Exists("smtp\\SMTP_Config_file.txt"))
                {
                    file_string = File.ReadAllText("smtp\\SMTP_Config_file.txt");
                    string[] substring_array = file_string.Split(' ');
                    smtp_client = substring_array[0];
                    port = substring_array[1];
                    email = substring_array[2];
                    password = substring_array[3];


                    for (int i = 4; i < substring_array.Length; i++)
                    {
                        recipient_list.Add(substring_array[i]);
                    }
                }
                else
                {
                    Console.WriteLine("Não foi possível encontrar o arquivo no caminho especificado: smtp\\SMTP_Config_file.txt");
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
        public List<string> EmailList=> recipient_list;

    }
}
