using System;
using System.Threading.Tasks;
using Bosch.Common;

namespace Bosch.API.Services.Interface
{
    public interface IPublishMQService
    {
        Task SendToMQ(ProductData productData);
    }
}
