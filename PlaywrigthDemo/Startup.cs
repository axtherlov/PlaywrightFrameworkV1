using Microsoft.Extensions.DependencyInjection;
using TestFramework.Config;
using TestFramework.Driver;

namespace PlaywrigthDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the configuration
            services.AddSingleton<TestSettings>(provider => ConfigReader.ReadConfig());

            // Register the playwright driver initializer
            services.AddTransient<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>();

            // Register the playwright driver (both as interface and concrete class)
            services.AddTransient<IPlaywrightDriver, PlaywrightDriver>();
            services.AddTransient<PlaywrightDriver>();

            // Register the test base
            services.AddTransient<ITestBase, TestBase>();
            services.AddTransient<TestBase>();
        }
    }
}
