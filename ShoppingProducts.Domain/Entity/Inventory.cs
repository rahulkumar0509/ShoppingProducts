using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProducts.Domain.Entity
{
    [Table("Inventory", Schema = "ProductSchema")]
    public class Inventory
    {
        [Key]
        public Guid ProductId {get; set;}
        public required int StockCount {get; set;}
    }
}