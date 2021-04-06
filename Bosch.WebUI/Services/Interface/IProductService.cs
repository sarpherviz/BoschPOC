using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bosch.Common;

namespace Bosch.WebUI.Services.Interface
{
    public interface IProductService
    {
        Task<List<ProductData>> GetProducts();
        Task SaveProduct(ProductData request);
    }
}
