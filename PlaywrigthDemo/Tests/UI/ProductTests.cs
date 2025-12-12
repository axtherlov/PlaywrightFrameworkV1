using AutoFixture.NUnit3;
using PlaywrigthDemo.EApp.Models;
using PlaywrigthDemo.EApp.Pages;

namespace PlaywrigthDemo.Tests.UI
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Tests : TestBase
    {
        [Test, AutoData]
        public async Task CreateProduct1(Product product)
        {
            await NavigateToUrl();
            var productListPage = new ProductListPage(_playwrightDriver);
            await productListPage.GoToCreateProductForm();

            var productPage = new ProductPage(_playwrightDriver);
            await productPage.CreateProduct(product);

            await productListPage.SelectProductFromList(product.Name);

            await productListPage.IsProductCreated(product.Name);
        }

        [Test, AutoData]
        public async Task CreateProduct2(Product product)
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