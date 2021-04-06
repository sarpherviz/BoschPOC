using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bosch.Common;

namespace Bosch.WebUI.Consumer.Data.Interface
{
    public interface IProductHttpClientDataContext
    {
        Task<HttpResponseMessage> GetProducts();
        Task<HttpResponseMessage> PublishProduct(ProductData request);
    }
}
