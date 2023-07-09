using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace Notificador
{
    internal static class FileReader
    {
        private static string smtp_server;
        private static string smtp_credentials;
        public static void readTxt()
        {
            try
            {
                StreamReader reader = new StreamReader("smtp\\SMTP_Config_file.txt");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }
}
