using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bosch.Common;
using Bosch.WebUI.Consumer.Data.Interface;
using Bosch.WebUI.Services.Interface;
using Newtonsoft.Json;

namespace Bosch.WebUI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductHttpClientDataContext _productHttpClientDataContext;
        public ProductService(IProductHttpClientDataContext productHttpClientDataContext)
        {
            _productHttpClientDataContext = productHttpClientDataContext;
        }

        public async Task<List<ProductData>> GetProducts()
        {
            var returnData = new List<ProductData>();
            var response = await _productHttpClientDataContext.GetProducts();
            if(response.IsSuccessStatusCode)
            {
                returnData = JsonConvert.DeserializeObject<List<ProductData>>(await response.Content.ReadAsStringAsync());
            }

            return returnData;
        }

        public async Task SaveProduct(ProductData request)
        {
            var response = await _productHttpClientDataContext.PublishProduct(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
