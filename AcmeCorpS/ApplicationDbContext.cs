using AcmeCorpS.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorpS
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
