using System.Threading.Tasks;
using ShoppingProducts.API;
using ShoppingProducts.Domain;
using ShoppingProducts.Domain.Entity;

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
            
            // create product
            var NewProduct = _productDb.Products.Add(data); 
            await _productDb.SaveChangesAsync();
            // create category if not available
            foreach(string category in product.Categories)
            {
                if (_productDb.Categories.SingleOrDefault(cat => cat.Name == category) == null)
                {
                    _productDb.Categories.Add(new Category{Name=category});
                }
            }

            // Add inventory with product id.
            _productDb.Inventories.Add(new Inventory { ProductId = NewProduct.Entity.Id, StockCount = product.StockCount});
            await _productDb.SaveChangesAsync();
            return NewProduct.Entity.Id;
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