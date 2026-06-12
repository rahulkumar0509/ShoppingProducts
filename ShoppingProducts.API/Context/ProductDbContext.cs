using Microsoft.EntityFrameworkCore;
using ShoppingProducts.Domain;
using ShoppingProducts.Domain.Entity;

namespace ShoppingProducts.API
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands {get; set;}
        private IConfiguration _configuration;
        public ProductDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.ToTable("ProductCategories")); // Custom join table name

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.ProductsList)
                .HasForeignKey(p => p.BrandId);
        }
    }
}