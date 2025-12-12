using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Config;

namespace TestFramework.Driver
{
    public class PlaywrightDriverInitializer : IPlaywrightDriverInitializer
    {
        private const float DEFAULT_TIMEOUT = 30f;

        public async Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings)
        {
            var browserOptions = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
            browserOptions.Channel = "chrome";
            return await GetBrowserAsync(DriverType.Chromium, browserOptions);
        }

        public async Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings)
        {
            var browserOptions = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
            browserOptions.Channel = "firefox";
            return await GetBrowserAsync(DriverType.Firefox, browserOptions);
        }

        public async Task<IBrowser> GetEdgeDriverAsync(TestSettings testSettings)
        {
            var browserOptions = GetParameters(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
            browserOptions.Channel = "msedge";
            return await GetBrowserAsync(DriverType.Chromium, browserOptions);
        }

        private async Task<IBrowser> GetBrowserAsync(DriverType driverType, BrowserTypeLaunchOptions browserOptions)
        {
            var playwrightDriver = await Playwright.CreateAsync();
            return await playwrightDriver[driverType.ToString().ToLower()].LaunchAsync(browserOptions);
        }

        private BrowserTypeLaunchOptions GetParameters(string[] args, float? timeout = DEFAULT_TIMEOUT, bool headless = true, float slowMo = 1500)
        {
            return new BrowserTypeLaunchOptions
            {
                Args = args,
                Timeout = timeout,
                Headless = headless,
                SlowMo = slowMo
            };
        }
    }
}
