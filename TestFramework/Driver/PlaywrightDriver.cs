using Microsoft.Playwright;
using TestFramework.Config;

namespace TestFramework.Driver
{
    public class PlaywrightDriver : IDisposable, IPlaywrightDriver
    {
        private readonly AsyncTask<IBrowser>? _browser;
        private readonly AsyncTask<IBrowserContext>? _browserContext;
        private TestSettings _testSettings;
        private bool isDisposed;
        private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly AsyncTask<IPage>? _page;

        public PlaywrightDriver(TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = testSettings;
            _playwrightDriverInitializer = playwrightDriverInitializer;
            var driverInitializer = new PlaywrightDriverInitializer();
            _browser = new AsyncTask<IBrowser>(InitializePlaywrightAsync);
            _browserContext = new AsyncTask<IBrowserContext>(CreateBrowserContext);
            _page = new AsyncTask<IPage>(CreatePageAsync);
        }

        public Task<IBrowser> Browser => _browser.Value;
        public Task<IBrowserContext> BrowserContext => _browserContext.Value;
        public Task<IPage> Page => _page.Value;

        private async Task<IBrowser> InitializePlaywrightAsync()
        {
            return _testSettings.DriverType switch
            {
                DriverType.Chromium => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
                DriverType.Edge => await _playwrightDriverInitializer.GetEdgeDriverAsync(_testSettings),
                DriverType.Firefox => await _playwrightDriverInitializer.GetFirefoxDriverAsync(_testSettings),
                _ => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings)
            };
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            return await (await _browser).NewContextAsync();
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await _browserContext).NewPageAsync();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                if (_browser.IsValueCreated)
                {
                    Task.Run(async () =>
                    {
                        var browser = await _browser;
                        await browser.CloseAsync();
                        await browser.DisposeAsync();
                    });
                }
                isDisposed = true;
            }
        }
    }
}
