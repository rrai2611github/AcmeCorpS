using AcmeCorpS.Models;
using AcmeCorpS.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AcmeCorp.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while retrieving the customers." + ex.InnerException);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while retrieving the customer." + ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }

                await _customerRepository.AddCustomer(customer);

                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while adding the customer." + ex.InnerException);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                if (customer == null || id != customer.Id)
                {
                    return BadRequest();
                }

                var existingCustomer = await _customerRepository.GetCustomerById(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }

                existingCustomer.Name = customer.Name;
                // Update other properties as needed

                await _customerRepository.UpdateCustomer(existingCustomer);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while updating the customer." + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound();
                }

                await _customerRepository.DeleteCustomer(customer);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while deleting the customer" + ex.InnerException);
            }
        }
    }
}
