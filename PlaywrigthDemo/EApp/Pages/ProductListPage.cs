using Microsoft.Playwright;
using TestFramework.Driver;

namespace PlaywrigthDemo.EApp.Pages
{
    public class ProductListPage
    {
        private readonly IPage _page;

        public ProductListPage(IPlaywrightDriver playwrightDriver)
        {
            _page = playwrightDriver.Page.Result;
        }

        private ILocator _linkProductList => _page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Product" });
        private ILocator _linkCreate => _page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Create" });
       

        public async Task GoToCreateProductForm()
        {
            await _page.GotoAsync("http://ea_webapp:8000/");
            await _linkProductList.ClickAsync();
            await _linkCreate.ClickAsync();
        }

        public async Task SelectProductFromList(string product)
        {
            await _page.GetByRole(AriaRole.Row, new PageGetByRoleOptions { Name = product })
                .GetByRole(AriaRole.Link, new LocatorGetByRoleOptions { Name = "Details" }).ClickAsync();
        }

        public async Task IsProductCreated(string product)
        {
            var _productName = _page.GetByText(product, new PageGetByTextOptions { Exact = true });
            await Assertions.Expect(_productName).ToBeVisibleAsync();
        }
    }
}
