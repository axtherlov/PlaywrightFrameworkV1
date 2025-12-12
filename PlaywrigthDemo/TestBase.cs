using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using TestFramework.Config;
using TestFramework.Driver;
using static System.Net.Mime.MediaTypeNames;

namespace PlaywrigthDemo
{
    [Parallelizable(ParallelScope.Children)]
    public class TestBase : ITestBase
    {
        protected IPlaywrightDriver _playwrightDriver;
        protected TestSettings _testSettings;
        protected Task<IPage> _currentPage; // Changed from Task<IPage> to IPage
        private static ServiceProvider _provider;
        private static readonly object _lock = new object();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            lock (_lock)
            {
                if (_provider == null)
                {
                    InitializeDI();
                }
            }
        }

        [SetUp]
        public async Task Setup()
        {
            // Get fresh instances for each test
            _playwrightDriver = _provider.GetRequiredService<IPlaywrightDriver>();
            _testSettings = _provider.GetRequiredService<TestSettings>();
            _currentPage = _playwrightDriver.Page;
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            if (_playwrightDriver != null)
            {
                var browser = await _playwrightDriver.Browser;
                await browser.CloseAsync();
                await browser.DisposeAsync();
            }

            if (_provider != null)
            {
                _provider.Dispose();
                _provider = null;
            }
        }

        [TearDown]
        public async Task TearDown()
        {
            // Close the page and context after each test
            _playwrightDriver.Dispose();
        }

        private void InitializeDI()
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            _provider = services.BuildServiceProvider();
        }

        public async Task NavigateToUrl()
        {
            await (await _currentPage).GotoAsync(_testSettings.ApplicationUrl);
        }

        public async Task TakeScreenshotAsync(string fileName)
        {
            await (await _currentPage).ScreenshotAsync(new PageScreenshotOptions() { Path = fileName, FullPage = true });
        }
    }
}