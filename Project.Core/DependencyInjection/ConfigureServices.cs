using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Project.Core.Configurations;
using Project.Core.Driver;
using Project.Core.PageObjects;
using Project.Core.RestApiServices;
using System;

namespace Project.Core.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddConfigureService(this IServiceCollection servicesCollection, IConfiguration configuration) => 
            servicesCollection.AddTestConfiiguration(configuration)
                              .AddTestData(configuration)
                              .AddWebDriver()
                              .AddPageObjects()
                              .AddHttpClient(configuration);
        private static IServiceCollection AddTestConfiiguration(this IServiceCollection servicesCollection, IConfiguration configuration) => 
            servicesCollection.Configure<AppSettings>(configuration.GetSection("TestConfiguration"));

        private static IServiceCollection AddTestData(this IServiceCollection servicesCollection, IConfiguration configuration) => 
            servicesCollection.Configure<ReaderTestData>(configuration.GetSection("TestData"));

        private static IServiceCollection AddWebDriver(this IServiceCollection servicesCollection)
        {
            return servicesCollection.AddScoped(x =>
            {
                var testConfiguration = x.GetService<IOptions<AppSettings>>().Value;
                return DriverFactory.GetWebDriver(testConfiguration.Browser);
            });
        }

        private static IServiceCollection AddPageObjects(this IServiceCollection servicesCollection) => 
            servicesCollection.AddTransient<GoogleMapsPage>();

        private static IServiceCollection AddHttpClient(this IServiceCollection servicesCollection, IConfiguration configuration)
        {
            servicesCollection.AddHttpClient<IRestApiService, RestApiService>(client =>
            {
                client.BaseAddress = new Uri($"{configuration.GetValue<string>("TestConfiguration:HttpSchema")}://{configuration.GetValue<string>("TestConfiguration:Endpoint")}");
                client.Timeout = new TimeSpan(0, 0, 5);
            });

            return servicesCollection;
        }
    }
}
