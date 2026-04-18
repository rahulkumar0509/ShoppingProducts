using System.Threading.Tasks;
using ShoppingProducts.API;
using ShoppingProducts.Domain;

namespace ShoppingProducts.Service
{
    public class ProductService
    {
        private ProductDbContext _productDb;
        public ProductService(ProductDbContext productDbContext)
        {
            _productDb = productDbContext;
        }

        public async Task<Guid> CreateProduct(ProductDto product)
        {
            var data = new Product{Name=product.Name, Description = product.Description, Price = product.Price};
            _productDb.Add(data);
            await _productDb.SaveChangesAsync();
            return data.Id;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productDb.Products;
        }

        public Product GetProductById(Guid Id)
        {
            return _productDb.Products.SingleOrDefault(p=>p.Id == Id);
        }
    }
}