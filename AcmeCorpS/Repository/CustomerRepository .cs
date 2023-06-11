using AcmeCorpS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorpS.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbContext _dbContext;

        public CustomerRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _dbContext.Set<Customer>().FindAsync(id);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _dbContext.Set<Customer>().ToListAsync();
        }

        public async Task AddCustomer(Customer customer)
        {
            await _dbContext.Set<Customer>().AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _dbContext.Set<Customer>().Update(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Customer customer)
        {
            _dbContext.Set<Customer>().Remove(customer);
            await _dbContext.SaveChangesAsync();
        }

    }
}
