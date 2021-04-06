using System;
using System.Threading.Tasks;
using Bosch.API.Services.Interface;
using Bosch.Common;
using MassTransit;

namespace Bosch.API.Services
{
    public class PublishMQService: IPublishMQService
    {
        private readonly IBusControl _busControl;

        public PublishMQService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task SendToMQ(ProductData productData)
        {
            await _busControl.Publish(new ProductDataCreatedEvent()
            {
                CorrelationId = Guid.NewGuid().ToString(),
                TracingKeys = null,
                ProductData = productData
            });
        }
    }
}
