using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShoppingProducts.Domain.Entity
{
    [Table("Category", Schema = "ProductSchema")]
    public class Category
    {
        [Key]
        public Guid Id {get; set;}
        public required string Name {get; set;}
        [JsonIgnore]
        public virtual ICollection<Product> Products {get; set;} = new List<Product>();
    }
}