using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Notificador{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string input = "";
            string asset = "";
            string min_value = "";
            string max_value = "";

            do
            {
                try
                {
                    input = Console.ReadLine();
                    string[] value = input.Split(' ');
                    asset = value[0];
                    min_value = value[1];
                    max_value = value[2];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }while (string.IsNullOrEmpty(input));

            try
            {
                ApiConnect apiConnect = new ApiConnect();
                string quoteData = await apiConnect.GetQuoteData(asset);
                Console.WriteLine(quoteData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }



        }



    }

}