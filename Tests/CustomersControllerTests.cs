using AcmeCorp.Controllers;
using AcmeCorpS.Models;
using AcmeCorpS.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class CustomersControllerTests
    {
        [Fact]
        public async Task GetAllCustomers_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetAllCustomers())
                .ReturnsAsync(GetSampleCustomers());
            var controller = new CustomersController(mockRepository.Object);

            // Act
            var result = await controller.GetAllCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customers = Assert.IsType<List<Customer>>(okResult.Value);
            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public async Task GetCustomerById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var expectedCustomer = new Customer { Id = 1, Name = "John Doe" };
            mockRepository.Setup(repo => repo.GetCustomerById(1))
                .ReturnsAsync(expectedCustomer);
            var controller = new CustomersController(mockRepository.Object);

            // Act
            var result = await controller.GetCustomerById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(expectedCustomer.Id, customer.Id);
            Assert.Equal(expectedCustomer.Name, customer.Name);
        }

        [Fact]
        public async Task AddCustomer_WithValidCustomer_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var newCustomer = new Customer { Id = 3, Name = "Jane Smith" };
            var controller = new CustomersController(mockRepository.Object);

            // Act
            var result = await controller.AddCustomer(newCustomer);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(CustomersController.GetCustomerById), createdAtActionResult.ActionName);
            Assert.Equal(newCustomer.Id, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateCustomer_WithValidIdAndCustomer_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var existingCustomer = new Customer { Id = 1, Name = "John Doe" };
            var updatedCustomer = new Customer { Id = 1, Name = "Updated Name" };
            mockRepository.Setup(repo => repo.GetCustomerById(1))
                .ReturnsAsync(existingCustomer);
            var controller = new CustomersController(mockRepository.Object);

            // Act
            var result = await controller.UpdateCustomer(1, updatedCustomer);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var existingCustomer = new Customer { Id = 1, Name = "John Doe" };
            mockRepository.Setup(repo => repo.GetCustomerById(1))
                .ReturnsAsync(existingCustomer);
            var controller = new CustomersController(mockRepository.Object);

            // Act
            var result = await controller.DeleteCustomer(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        // Helper method to get sample customers
        private List<Customer> GetSampleCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe" },
                new Customer { Id = 2, Name = "Jane Smith" }
            };
        }
    }

}
