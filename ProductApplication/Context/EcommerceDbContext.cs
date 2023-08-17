using Microsoft.EntityFrameworkCore;
using ProductApplication.Models;

namespace ProductApplication.Context
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions options):base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define initial data
            var categories = new[]
            {
        new Category { Id = 1, Name = "Sports" },
        new Category { Id = 2, Name = "Clothes" },
        new Category { Id = 3, Name="Electronics"},
                
        new Category { Id = 4, Name="Sweets"}
                
        // Add more categories as needed
    };

            var products = new[]
            {
        new Product { Id = 1, Name = "Product 1", Price = 100, CategoryId = 1 },
        new Product { Id = 2, Name = "Product 2", Price = 200, CategoryId = 2 }
        // Add more products as needed
    };

            // Seed the data
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}

