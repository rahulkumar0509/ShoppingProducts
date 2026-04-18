using Microsoft.EntityFrameworkCore;
using ShoppingProducts.Domain;

namespace ShoppingProducts.API
{
    public class ProductDbContext: DbContext
    {
        public DbSet<Product> Products {get; set;} 
        public ProductDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

    }
}