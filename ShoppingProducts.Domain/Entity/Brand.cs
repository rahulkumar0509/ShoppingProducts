using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using ShoppingProducts.Domain;

[Table("Brand", Schema = "ProductSchema")]
public class Brand
{
    [Key]
    public Guid BrandId {get; set;}
    public required string Name{get; set;}
    public virtual ICollection<Product> ProductsList {get; set;} = new List<Product>();
}