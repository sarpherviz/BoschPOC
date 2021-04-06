using System.Collections.Generic;

namespace Bosch.Common
{
    public class ProductDataCreatedEvent
    {
        public string CorrelationId { get; set; }
        public Dictionary<string, string> TracingKeys { get; set; }
        public ProductData ProductData { get; set; }
    }
}
