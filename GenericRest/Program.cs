using GenericRest.Concrats;
using GenericRest.Domain;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace GenericRest
{
    class Program
    {
        static void Main(string[] args)
        {
    
            var client = new RestClientService("https://viacep.com.br");

            var response = client.ExecuteAsync<CEP>("/ws/01001000/json/", null, RestSharp.Method.GET).Result;

            Console.WriteLine(JsonConvert.SerializeObject(response));
            Console.ReadKey();

        }
    }
}
