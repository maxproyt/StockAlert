using System;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Notificador
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string input = "";
            string asset = "";
            double min_value = 0;
            double max_value = 0;
            double last_value = 0;
            string? quoteDataJson;

            while (true)
            {
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
                } while (string.IsNullOrEmpty(input) || double.IsNegative(min_value) || double.IsNegative(max_value));

                while (true)
                {
                    try
                    {
                        ApiConnect apiConnect = new ApiConnect();
                        quoteDataJson = await apiConnect.GetQuoteData(asset);
                        if (quoteDataJson == null)
                        {
                            break;
                        }
                        JObject quoteData = JObject.Parse(quoteDataJson);
                        double regularMarketPrice = (double)quoteData["results"][0]["regularMarketPrice"];

                        if (regularMarketPrice < min_value && (last_value > min_value || last_value == 0))
                        {
                            Console.WriteLine($"O valor do ativo {asset} é inferior ao valor de compra determinado.({regularMarketPrice}) Enviando E-Mail ao cliente");
                            EmailSender sendMail = new EmailSender(asset, false);
                            sendMail.SendMailSmtp();
                        }
                        else if (regularMarketPrice > max_value && last_value < max_value)
                        {
                            Console.WriteLine($"O valor do ativo {asset} é superior ao valor de venda determinado.({regularMarketPrice}) Enviando E-Mail ao cliente");
                            EmailSender sendMail = new EmailSender(asset, true);
                            sendMail.SendMailSmtp();
                        }
                        else
                        {
                            Console.WriteLine($"Valor atual do ativo {asset}: {regularMarketPrice}");
                        }
                        last_value = regularMarketPrice;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Não foi possível conectar-se à API, ocorreu um erro: {ex.Message}");
                    }

                    await Task.Delay(TimeSpan.FromMinutes(5));
                }
            }
        }
    }
}







