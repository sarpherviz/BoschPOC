using System;
using System.Threading.Tasks;
using Bosch.Common;
using Bosch.Consumer.Data.Interface;
using MassTransit;

namespace Bosch.Consumer.Consumer
{
    public class DataConsumer : IConsumer<ProductDataCreatedEvent>
    {

        private readonly IProductHttpClientDataContext _httpDataContext;
        public DataConsumer(IProductHttpClientDataContext httpDataContext)
        {
            _httpDataContext = httpDataContext;
        }

        public async Task Consume(ConsumeContext<ProductDataCreatedEvent> context)
        {
            var correlationId = context.CorrelationId.HasValue ? context.CorrelationId.ToString() : Guid.NewGuid().ToString();
            //NLog.MappedDiagnosticsLogicalContext.Set("CorrelationId", correlationId);
            
            var response = await _httpDataContext.PostProductAsync(correlationId,new AddProductRequest
            {
                 Id = context.Message.ProductData.Id,
                 MachineId = context.Message.ProductData.MachineId,
                 ProductBarcode = context.Message.ProductData.ProductBarcode
             });

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
