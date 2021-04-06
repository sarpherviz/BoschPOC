using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bosch.API.Model.Domain;
using Bosch.API.Model.Response;
using Bosch.Common;

namespace Bosch.API.Services.Interface
{
    public interface IProductService
    {
        Task<BaseResponse<bool>> Save(AddProductRequest productData);
        Task<BaseResponse<List<Product>>> GetProducts();
    }
}
