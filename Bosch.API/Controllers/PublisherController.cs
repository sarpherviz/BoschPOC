using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bosch.API.Services.Interface;
using Bosch.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bosch.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublishMQService _publishMQService;

        public PublisherController(IPublishMQService publishMQService)
        {
            _publishMQService = publishMQService;
        }


        [HttpPost]
        public async Task Post([FromBody] ProductData productData)
        {
            await _publishMQService.SendToMQ(productData);
        }
    }
}
