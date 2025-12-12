using Microsoft.Playwright;
using TestFramework.Config;

namespace TestFramework.Driver
{
    public interface IPlaywrightDriverInitializer
    {
        Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetEdgeDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings);
    }
}