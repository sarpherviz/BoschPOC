using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bosch.API.Data.Interface;
using Bosch.API.Model.Domain;
using Bosch.API.Model.Response;
using Bosch.API.Services.Interface;
using Bosch.Common;
using MongoDB.Driver;

namespace Bosch.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoDbDataContext _mongoDbDataContext;

        public ProductService(IMongoDbDataContext mongoDbDataContext)
        {
            _mongoDbDataContext = mongoDbDataContext;
        }

        public async Task<BaseResponse<List<Product>>> GetProducts()
        {
            var response = new BaseResponse<List<Product>>();
            try
            {
                var data = await _mongoDbDataContext.Product.Find(_ => true).ToListAsync();
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }

            return response;

        }

        public async Task<BaseResponse<bool>> Save(AddProductRequest productData)
        {
            var response = new BaseResponse<bool>();
            try
            {
                Product p = new Product();
                p.Id = productData.Id;
                p.MachineId = productData.MachineId;
                p.ProductBarcode = productData.ProductBarcode;
                p.ReceiveDate = DateTime.Now;

                await _mongoDbDataContext.Product.InsertOneAsync(p);
                response.Data = true;
            }
            catch(Exception ex)
            {
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
