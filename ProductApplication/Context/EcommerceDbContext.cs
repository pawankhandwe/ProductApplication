using Microsoft.EntityFrameworkCore;
using ProductApplication.Models;

namespace ProductApplication.Context
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions options):base(options)
        {


        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}