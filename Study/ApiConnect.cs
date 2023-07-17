using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Notificador
{
    /// <summary>
    /// Classe responsável por fazer uma requisição à API de cotação de açoões da brapi.
    /// </summary>
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


        /// <summary>
        /// Faz a requisição GET à API de cotação de mercado, guarda em uma string os dados em formato JSON.
        /// </summary>
        /// <param name="symbol">A cotação desejada(symbol)</param>
        /// <returns>uma string com o conteúdo da requisição à API em formato JSON.</returns>
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
