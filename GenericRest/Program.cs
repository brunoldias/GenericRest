using GenericRest.Domain;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GenericRest
{
    class Program
    {
        private static RestClientService client = new RestClientService();
        static async Task Main(string[] args)
        {
            var response = await GetCep("https://viacep.com.br", "03927050");

            Console.WriteLine(JsonConvert.SerializeObject(response));
            Console.ReadKey();

        }
        private static async Task<CEP> GetCep(string url, string cep)
        {
            return await client.ExecuteAsync<CEP>(url, $"/ws/{cep}/json/", null, RestSharp.Method.GET);
        }
    }
}
