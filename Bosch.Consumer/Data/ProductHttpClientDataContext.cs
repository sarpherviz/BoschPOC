using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bosch.Common;
using Bosch.Consumer.Data.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Bosch.Consumer.Data
{
    public class ProductHttpClientDataContext : IProductHttpClientDataContext
    {

        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _clientConfiguration;
        private readonly HttpClient _client;

        public ProductHttpClientDataContext(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
            _clientConfiguration = _configuration.GetSection("TestHttpClient");
        }

        public async Task<HttpResponseMessage> GetPostCommentsAsync(string correlationId, int id)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(_client.BaseAddress, string.Format(_clientConfiguration.GetValue<string>("PostComments"), id)),
                Method = HttpMethod.Get,
            };
            httpRequestMessage.Headers.Add("CorrelationId", correlationId);

            return await _client.SendAsync(httpRequestMessage);
        }

        public async Task<HttpResponseMessage> PostProductAsync(string correlationId, AddProductRequest addProductRequest)
        {
            var jsonData = JsonConvert.SerializeObject(addProductRequest);
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(_client.BaseAddress, string.Format(_clientConfiguration.GetValue<string>("PostProduct"))),
                Method = HttpMethod.Post,
                Content = new StringContent(jsonData,Encoding.UTF8,"application/json")
            };

            return await _client.SendAsync(httpRequestMessage);
        }
    }
}
