using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bosch.WebUI.Models;
using Bosch.WebUI.Services.Interface;
using Bosch.Common;

namespace Bosch.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async  Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetProducts()
        {
            return Json(new { data = await _productService.GetProducts() });
        }

        public IActionResult AddProduct()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductData request)
        {
            await _productService.SaveProduct(request);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
