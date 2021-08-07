using GenericRest.Concrats;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GenericRest.Domain
{
    public class RestClientService 
    {
        private readonly RestClient _client;
        public RestClientService(string url)
        {
            _client = new RestClient(url);
        }

        public Task<T> ExecuteAsync<T>(string resource, string body, Method method) where T : class
        {
            var request = new RestRequest(resource, method);

            if (!string.IsNullOrEmpty(body))
                request.AddJsonBody(body);

            var response = _client.ExecuteGetAsync(request).Result;

            if (response.Content is null) throw new ArgumentNullException($"Url: {response.ResponseUri} Status Code: {response.StatusCode} Error Message: Response is null");

            if (response.StatusCode != HttpStatusCode.OK) throw new Exception($"Url: {response.ResponseUri} Response Content: {response.Content} Status Code: {response.StatusCode} Error Message: Occourred an error");

            var data = JsonConvert.DeserializeObject<T>(response.Content);

            return Task.FromResult(data);
        }

    }
}
