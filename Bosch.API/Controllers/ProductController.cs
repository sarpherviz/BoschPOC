using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bosch.API.Model.Response;
using Bosch.API.Services.Interface;
using Bosch.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bosch.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _productService.GetProducts();
            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductRequest productData)
        {
            var response = await _productService.Save(productData);
            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }
    }
}
