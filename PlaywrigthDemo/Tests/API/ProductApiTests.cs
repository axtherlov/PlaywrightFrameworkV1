using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using PlaywrigthDemo.EApp.Models;
using System.Text.Json;

namespace PlaywrigthDemo.Tests.API
{
    public class ProductApiTests: TestBase
    {

        [Test]
        public async Task GetFirstProduct()
        {
            var getProductRespose = await (await _playwrightDriver.ApiRequestContext).GetAsync("Product/GetProductById/1");
            var data = await getProductRespose.JsonAsync();
            var product = data.Value.Deserialize<Product>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            using(new AssertionScope())
            {
                getProductRespose.Status.Should().Be(200);
                product.Name.Should().Be("Keyboard");
            }
        }

        [Test]
        public async Task CreateProduct()
        {
            var productName = $"Product-{Random.Shared.Next(1111, 9999)}";
            var productData = new Product()
            {
                Id = Random.Shared.Next(1111, 9999),
                Name = productName,
                Description = productName,
                Price = Random.Shared.Next(10, 99),
                ProductType = ProductType.CPU
            };

            var createProductRespose = await (await _playwrightDriver.ApiRequestContext).PostAsync("Product/Create",
                new APIRequestContextOptions()
                {
                    DataObject = productData,
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" }
                    }
                });

            var getProductRespose = await (await _playwrightDriver.ApiRequestContext).GetAsync($"Product/GetProductById/{productData.Id}");
            var data = await getProductRespose.JsonAsync();
            var product = data.Value.Deserialize<Product>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            using (new AssertionScope())
            {
                createProductRespose.Status.Should().Be(200);
                product.Name.Should().Be(productData.Name);
            }
        }

        [Test]
        public async Task DeleteProducts()
        {
            var deleteProductRespose = await (await _playwrightDriver.ApiRequestContext).DeleteAsync("Product/Delete", 
                new APIRequestContextOptions()
                {
                    Params = new Dictionary<string, object>
                    {
                        { "id", "1" }
                    }
                });

            var getProductRespose = await (await _playwrightDriver.ApiRequestContext).GetAsync("Product/GetProductById/1");

            using (new AssertionScope())
            {
                deleteProductRespose.Status.Should().Be(200);
                getProductRespose.Status.Should().Be(202);
            }
        }
    }
}