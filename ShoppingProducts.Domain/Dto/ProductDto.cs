using System.ComponentModel.DataAnnotations;

namespace ShoppingProducts.Domain
{
    public class ProductDto
    {
        public required string Name {get; set;}
        public required string Description {get; set;}
        public required float Price {get; set;}
        public required List<string> Categories {get; set;}
        public required int StockCount {get; set;}
    }
}