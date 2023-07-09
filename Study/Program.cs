using System;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Notificador{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string input = "";
            string asset = "";
            string min_value = "";
            string max_value = "";
            string quoteDataJson = "";

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
            }while (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(min_value) || string.IsNullOrEmpty(max_value));

            try
            {
                ApiConnect apiConnect = new ApiConnect();
                quoteDataJson = await apiConnect.GetQuoteData(asset);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Não foi possível conectar-se à API, ocorreu um erro: {ex.Message}");
            }

            JObject quoteData = JObject.Parse(quoteDataJson);
            double regularMarketPrice = (double)quoteData["results"][0]["regularMarketPrice"];
            Console.WriteLine(regularMarketPrice);
            Console.ReadLine();
            



        }



    }


}