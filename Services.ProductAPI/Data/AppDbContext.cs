using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.Models;

namespace Services.ProductAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product
            {
               ProductId = 1,
               ProductName="Product 1",
               Price=1000,
               ProductDescription="Description 1",
               ProductCategory = "Category 1",
               ImageUrl =""
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                ProductName = "Product 2",
                Price = 2000,
                ProductDescription = "Description 2",
                ProductCategory = "Category 2",
                ImageUrl = ""
            });
        }
    }
}
