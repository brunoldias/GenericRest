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
        public async Task<T> ExecuteAsync<T>(string url, string resource, string body, Method method) where T : class
        {
            var client = new RestClient(url);
            var request = new RestRequest(resource, method);

            if (!string.IsNullOrEmpty(body))
                request.AddJsonBody(body);

            var response = await client.ExecuteTaskAsync(request);

            if (response.Content is null) throw new ArgumentNullException($"Url: {response.ResponseUri} Status Code: {response.StatusCode} Error Message: Response is null");

            if (response.StatusCode != HttpStatusCode.OK) throw new Exception($"Url: {response.ResponseUri} Response Content: {response.Content} Status Code: {response.StatusCode} Error Message: Occourred an error");

            var data = JsonConvert.DeserializeObject<T>(response.Content);

            return data;
        }

    }
}
