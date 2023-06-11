using AcmeCorp.Controllers;
using AcmeCorpS;
using AcmeCorpS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class CustomersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CustomersControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllCustomers_ReturnsOkResultWithCustomers()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/customers");

            // Assert
            response.EnsureSuccessStatusCode();
            var customers = JsonConvert.DeserializeObject<List<Customer>>(
                await response.Content.ReadAsStringAsync());
            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public async Task GetCustomerById_WithValidId_ReturnsOkResultWithCustomer()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newCustomer = new Customer { Id = 1, Name = "John Doe" };
            var content = new StringContent(JsonConvert.SerializeObject(newCustomer), Encoding.UTF8, "application/json");

            // Create a new customer
            var createResponse = await client.PostAsync("/api/customers", content);
            createResponse.EnsureSuccessStatusCode();

            // Act
            var getResponse = await client.GetAsync("/api/customers/1");

            // Assert
            getResponse.EnsureSuccessStatusCode();
            var customer = JsonConvert.DeserializeObject<Customer>(
                await getResponse.Content.ReadAsStringAsync());
            Assert.Equal(newCustomer.Id, customer.Id);
            Assert.Equal(newCustomer.Name, customer.Name);
        }

        [Fact]
        public async Task AddCustomer_WithValidCustomer_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newCustomer = new Customer { Id = 3, Name = "Jane Smith" };
            var content = new StringContent(JsonConvert.SerializeObject(newCustomer), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/customers", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(
                JsonConvert.DeserializeObject<ObjectResult>(
                    await response.Content.ReadAsStringAsync()));
            Assert.Equal(nameof(CustomersController.GetCustomerById), createdAtActionResult.ActionName);
            Assert.Equal(newCustomer.Id, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateCustomer_WithValidIdAndCustomer_ReturnsNoContent()
        {
            // Arrange
            var client = _factory.CreateClient();
            var existingCustomer = new Customer { Id = 1, Name = "John Doe" };
            var updatedCustomer = new Customer { Id = 1, Name = "Updated Name" };
            var existingContent = new StringContent(
                JsonConvert.SerializeObject(existingCustomer), Encoding.UTF8, "application/json");
            var updatedContent = new StringContent(
                JsonConvert.SerializeObject(updatedCustomer), Encoding.UTF8, "application/json");

            // Create an existing customer
            var createResponse = await client.PostAsync("/api/customers", existingContent);
            createResponse.EnsureSuccessStatusCode();

            // Act
            var response = await client.PutAsync("/api/customers/1", updatedContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.IsType<NoContentResult>(JsonConvert.DeserializeObject<IActionResult>(
                await response.Content.ReadAsStringAsync()));
        }

        [Fact]
        public async Task DeleteCustomer_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var client = _factory.CreateClient();
            var existingCustomer = new Customer { Id = 1, Name = "John Doe" };
            var content = new StringContent(JsonConvert.SerializeObject(existingCustomer), Encoding.UTF8, "application/json");

            // Create an existing customer
            var createResponse = await client.PostAsync("/api/customers", content);
            createResponse.EnsureSuccessStatusCode();

            // Act
            var response = await client.DeleteAsync("/api/customers/1");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.IsType<NoContentResult>(JsonConvert.DeserializeObject<IActionResult>(
                await response.Content.ReadAsStringAsync()));
        }
    }
}
