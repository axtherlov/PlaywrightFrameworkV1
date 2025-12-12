using Microsoft.Playwright;
using PlaywrigthDemo.EApp.Models;
using TestFramework.Driver;

namespace PlaywrigthDemo.EApp.Pages
{
    public class ProductPage
    {
        private IPage _page;

        public ProductPage(IPlaywrightDriver playwrightDriver)
        {
            _page = playwrightDriver.Page.Result;
        }

        private ILocator _txtName => _page.GetByLabel("Name" );
        private ILocator _txtDescription => _page.GetByLabel("Description");
        private ILocator _txtPrice => _page.Locator("#Price");
        private ILocator _selectProduct => _page.GetByRole(AriaRole.Combobox, new PageGetByRoleOptions { Name = "ProductType" });
        private ILocator _linkCreate => _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Create" });

        public async Task CreateProduct(Product product)
        {
            await _txtName.FillAsync(product.Name);
            await _txtDescription.FillAsync(product.Description);
            await _txtPrice.FillAsync(product.Price.ToString());
            await _selectProduct.SelectOptionAsync(new SelectOptionValue { Label = product.ProductType.ToString() });
            await _linkCreate.ClickAsync();
        }

    }
}
