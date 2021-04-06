using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MassTransit;
using MetroBus;
using MetroBus.Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Retry;
using Bosch.Consumer.Data.Interface;
using Bosch.Consumer.Data;
using Bosch.Consumer.Consumer;

namespace Bosch.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(basePath: Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    string rabbitMqUri = hostContext.Configuration.GetValue<string>("RabbitMqUri");
                    string rabbitMqUserName = hostContext.Configuration.GetValue<string>("RabbitMqUserName");
                    string rabbitMqPassword = hostContext.Configuration.GetValue<string>("RabbitMqPassword");                   

                    services.AddMetroBus(x =>
                    {
                        x.AddConsumer<DataConsumer>();
                    });

                    services.AddHttpClient<IProductHttpClientDataContext, ProductHttpClientDataContext>(c =>
                    {
                        c.BaseAddress = new Uri(hostContext.Configuration.GetSection("TestHttpClient")["BaseAddress"]);
                        c.DefaultRequestHeaders.Add("Accept", "application/json");
                        c.DefaultRequestHeaders.Connection.Add("Keep-Alive");
                        //c.Timeout = TimeSpan.FromSeconds(30);
                    });


                    services.AddSingleton<IBusControl>(provider => MetroBusInitializer.Instance
                            .UseRabbitMq(rabbitMqUri, rabbitMqUserName, rabbitMqPassword)
                            .RegisterConsumer<DataConsumer>("dummy.consumer.service.queue", provider)
                            .UseCircuitBreaker(10, 5, TimeSpan.FromSeconds(10))
                            .UseConcurrentConsumerLimit(1)
                            .SetPrefetchCount(1)
                            .Build())
                        .BuildServiceProvider();

                    services.AddHttpClient();
                    services.AddHostedService<BusService>();
                })
                .RunConsoleAsync().Wait();
        }         
    }
}
