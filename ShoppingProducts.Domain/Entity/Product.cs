using System.ComponentModel.DataAnnotations;

namespace ShoppingProducts.Domain
{
    public class Product
    {
        [Key]
        public Guid Id {get; set;}
        public required string Name {get; set;}
        public required string Description {get; set;}
        public required double Price {get; set;}
    }
}