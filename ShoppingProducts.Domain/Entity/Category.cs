using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProducts.Domain.Entity
{
    [Table("Category", Schema = "ProductSchema")]
    public class Category
    {
        [Key]
        public Guid Id {get; set;}
        public required string Name {get; set;}
    }
}