using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingProducts.Domain.Entity;

namespace ShoppingProducts.Domain
{
    [Table("Product", Schema = "ProductSchema")]
    public class Product
    {
        [Key]
        public Guid Id {get; set;}
        public required string Name {get; set;}
        public required string Description {get; set;}
        public required float Price {get; set;} // Entity Framework Core requires properties (with { get; set; }) to 
        // track changes, create proxies, and map data from the database.
        public virtual ICollection<Category> Categories {get; set;} = new List<Category>();
        // virtual : This is optional, but highly recommended if you ever decide to enable Lazy Loading, 
        // as the proxies require navigation properties to be virtual to override them.
        // [Uni]
        public required Guid BrandId {get; set;}
        public virtual Brand? Brand {get; set;} // Navigation Propertys
        // It enables "Eager Loading", Without that property, you can never "join" the Brand data when fetching a Product.
        // You can do _context.Products.Include(p => p.Brand).ToList(). This allows you to access product.Brand.Name directly in your code.
    }
}