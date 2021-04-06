using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bosch.Common;
using Bosch.WebUI.Consumer.Data.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Bosch.WebUI.Consumer.Data
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
            _clientConfiguration = _configuration.GetSection("ProductHttpClient");
        }

        public async Task<HttpResponseMessage> GetProducts()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(_client.BaseAddress, string.Format(_clientConfiguration.GetValue<string>("GetProduct"))),
                Method = HttpMethod.Get,
            };

            return await _client.SendAsync(httpRequestMessage);
        }

        public async Task<HttpResponseMessage> PublishProduct(ProductData request)
        {
            var json = JsonConvert.SerializeObject(request);

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(_client.BaseAddress, string.Format(_clientConfiguration.GetValue<string>("PublishProduct"))),
                Method = HttpMethod.Post,
                Content = new StringContent(json,Encoding.UTF8,"application/json")
            };

            return await _client.SendAsync(httpRequestMessage);
        }


    }
}
