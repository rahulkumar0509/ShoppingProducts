using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProducts.Domain
{
    [Table("Product", Schema = "ProductSchema")]
    public class Product
    {
        [Key]
        public Guid Id {get; set;}
        public required string Name {get; set;}
        public required string Description {get; set;}
        public required float Price {get; set;}
    }
}