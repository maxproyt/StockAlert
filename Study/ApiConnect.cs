using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Notificador
{
    public class ApiConnect
    {
        private readonly HttpClient httpClient;

        public ApiConnect()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://brapi.dev/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetQuoteData(string symbol)
        {
            string url = $"api/quote/{symbol}?range=1d&interval=1d&fundamental=false&dividends=false";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else if(response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Ação não encontrada, digite novamente:");
                return null;
            }
            else
            {
                throw new Exception($"Falha na solicitação da API: {response.StatusCode} , Reporte o erro!");
            }
        }

    }
}
