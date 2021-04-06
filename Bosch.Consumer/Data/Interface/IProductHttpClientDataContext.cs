using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bosch.Common;

namespace Bosch.Consumer.Data.Interface
{
    public interface IProductHttpClientDataContext
    {
        Task<HttpResponseMessage> GetPostCommentsAsync(string correlationId, int id);
        Task<HttpResponseMessage> PostProductAsync(string correlationId, AddProductRequest addProductRequest);
    }
}
