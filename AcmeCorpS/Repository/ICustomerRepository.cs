using AcmeCorpS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorpS.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(int id);
        Task<List<Customer>> GetAllCustomers();
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(Customer customer);
    }
}
