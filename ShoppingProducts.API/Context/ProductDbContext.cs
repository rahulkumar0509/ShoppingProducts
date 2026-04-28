using Microsoft.EntityFrameworkCore;
using ShoppingProducts.Domain;
using ShoppingProducts.Domain.Entity;

namespace ShoppingProducts.API
{
    public class ProductDbContext: DbContext
    {
        public DbSet<Product> Products {get; set;} 
        public DbSet<Inventory> Inventories {get; set;}
        public DbSet<Category> Categories {get; set;}
        public ProductDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

    }
}