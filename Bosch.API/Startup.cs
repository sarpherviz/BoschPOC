using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bosch.API.Configuration;
using Bosch.API.Data;
using Bosch.API.Data.Interface;
using Bosch.API.Services;
using Bosch.API.Services.Interface;
using MetroBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Polly;
using Polly.Retry;

namespace Bosch.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = ApiConfigurationConsts.ApiName, Version = ApiConfigurationConsts.ApiVersionV1, Description = ApiConfigurationConsts.ApiName });
                c.EnableAnnotations();
            });

            AsyncRetryPolicy asyncRetryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(3, duration => TimeSpan.FromSeconds(10));
            services.AddSingleton<AsyncRetryPolicy>(asyncRetryPolicy);

            //DataContext
            var mongoClientSettings = MongoClientSettings.FromConnectionString(Configuration.GetConnectionString("MongoDB"));
            mongoClientSettings.ReadConcern = ReadConcern.Majority;
            mongoClientSettings.ReadPreference = ReadPreference.SecondaryPreferred;
            mongoClientSettings.WriteConcern = WriteConcern.WMajority;
            services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));
            services.AddSingleton<IMongoDbDataContext, MongoDbDataContext>();

            string rabbitMqUri = Configuration.GetValue<string>("RabbitMqUri");
            string rabbitMqUserName = Configuration.GetValue<string>("RabbitMqUserName");
            string rabbitMqPassword = Configuration.GetValue<string>("RabbitMqPassword");

            services.AddSingleton(MetroBusInitializer.Instance.UseRabbitMq(rabbitMqUri, rabbitMqUserName, rabbitMqPassword).Build());

            AsyncRetryPolicy retryPolicy =
                Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, duration => TimeSpan.FromSeconds(5));

            services.AddSingleton<AsyncRetryPolicy>(retryPolicy);
            services.AddScoped<IPublishMQService, PublishMQService>();
            services.AddScoped<IProductService, ProductService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";  
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", $"{ApiConfigurationConsts.ApiName} {ApiConfigurationConsts.ApiVersionV1}");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = ApiConfigurationConsts.ApiName;
                c.EnableFilter();
                c.DefaultModelsExpandDepth(-1); // Hide models in Swagger UI
                c.DisplayRequestDuration();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
