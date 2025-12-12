using AutoFixture.NUnit3;
using Microsoft.Playwright;
using PlaywrigthDemo.EApp.Models;
using PlaywrigthDemo.EApp.Pages;

namespace PlaywrigthDemo
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Tests : TestBase
    {
        [Test]
        public async Task LoginSite()
        {
            var page = await _playwrightDriver.Page;
            await NavigateToUrl();
            await page.ClickAsync("text=Login");
            await page.GetByLabel("Username").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");


            await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" }).ClickAsync();
        }

        [Test]
        public async Task LoginSite2()
        {
            var page = await _playwrightDriver.Page;
            await NavigateToUrl();
            await page.ClickAsync("text=Login");
            await page.GetByLabel("Username").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");


            await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" }).ClickAsync();
        }

        [Test, AutoData]
        public async Task CreateProduct(Product product)
        {
            await NavigateToUrl();
            var productListPage = new ProductListPage(_playwrightDriver);
            await productListPage.GoToCreateProductForm();

            var productPage = new ProductPage(_playwrightDriver);
            await productPage.CreateProduct(product);

            await productListPage.SelectProductFromList(product.Name);

            await productListPage.IsProductCreated(product.Name);
        }
    }
}