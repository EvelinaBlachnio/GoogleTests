using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.DependencyInjection;
using System.IO;

namespace Project.UITests
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = GetConfigurationRoot();
            services.AddConfigureService(configuration);

            services.BuildServiceProvider();
        }

        public static IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile("testData.json")
                        .Build();
        }
    }
}
