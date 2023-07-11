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
            double min_value = 0;
            double max_value = 0;
            double last_value = 0;
            string quoteDataJson;

            do
            {
                try
                {
                    input = Console.ReadLine();
                    string[] value = input.Split(' ');
                    asset = value[0];
                    min_value = double.Parse(value[1]);
                    max_value = double.Parse(value[2]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }while (string.IsNullOrEmpty(input) || double.IsNegative(min_value) || double.IsNegative(max_value));

            while(true)
            {
                try
                {
                    ApiConnect apiConnect = new ApiConnect();
                    quoteDataJson = await apiConnect.GetQuoteData(asset);
                    JObject quoteData = JObject.Parse(quoteDataJson);
                    double regularMarketPrice = (double)quoteData["results"][0]["regularMarketPrice"];

                    if (regularMarketPrice < min_value && last_value > min_value)
                    {
                        Console.WriteLine($"O valor do ativo {asset} é inferior ao valor mínimo. Sugere-se a compra do ativo.");
                        EmailSender sendMail = new EmailSender(asset, false);
                    }
                    else if (regularMarketPrice > max_value && last_value < max_value)
                    {
                        Console.WriteLine($"O valor do ativo {asset} é superior ao valor máximo. Sugere-se a venda do ativo.");
                        EmailSender sendMail = new EmailSender(asset, true);
                    }

                    last_value = regularMarketPrice;
                    Console.WriteLine($"Valor atual do ativo {asset}: {regularMarketPrice}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Não foi possível conectar-se à API, ocorreu um erro: {ex.Message}");
                }

            Thread.Sleep(TimeSpan.FromMinutes(5));
            }
            
        }



    }


}