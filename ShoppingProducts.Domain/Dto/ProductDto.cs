using System.ComponentModel.DataAnnotations;

namespace ShoppingProducts.Domain
{
    public class ProductDto
    {
        public required string Name {get; set;}
        public required string Description {get; set;}
        public required double Price {get; set;}
    }
}