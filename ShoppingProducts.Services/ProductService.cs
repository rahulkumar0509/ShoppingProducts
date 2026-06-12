using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Guid> CreateProduct(ProductDto dto)
        {
            var data = new Product{
                Name=dto.Name, 
                Description = dto.Description, 
                Price = dto.Price,
                Categories = new List<Category>(),
                BrandId = dto.BrandId,
                };
            
            foreach(string categoryName in dto.Categories)
            {
                var existingCategory = await _productDb.Categories.FirstOrDefaultAsync(cat => cat.Name == categoryName);
                if (existingCategory == null)
                {
                    data.Categories.Add(new Category{Name=categoryName});
                    // _productDb.Categories.Add(new Category{Name=categoryName});
                }
                else
                {
                    data.Categories.Add(existingCategory);
                }
            }

            var NewProduct = _productDb.Products.Add(data);

            // Add inventory with product id.
            _productDb.Inventories.Add(new Inventory { ProductId = NewProduct.Entity.Id, StockCount = dto.StockCount});

            await _productDb.SaveChangesAsync();
            return NewProduct.Entity.Id;
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> products = _productDb.Products;
            // Console.WriteLine("length: " + products.Count());
            //if
            // foreach(var product in products)
            // {
            //     Console.WriteLine("length: " + product.Categories.Count());
            //     foreach(var category in product.Categories)
            //     {
            //         Console.WriteLine(category.Name);
            //     }
            // }
            return products;
        }

        public Product? GetProductById(Guid Id)
        {
            return _productDb.Products.SingleOrDefault(p=>p.Id == Id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _productDb.Categories;
        }

        public async Task<ProductResponseDto?> GetProductsWithCategory(Guid productId)
        {
            // return await _productDb.Products.Include(p=>p.Categories).FirstOrDefaultAsync(p=>p.Id == productId);

            // eager loading..
            return await _productDb.Products.Include(p=>p.Categories).Include(b=>b.Brand)
                .Select(p=> new ProductResponseDto{
                    Id = p.Id,
                    Name = p.Name, 
                    Price = p.Price, 
                    CategoryNames = p.Categories.Select(c=>c.Name).ToList(),
                    BrandName = p.Brand.Name
                })
                .FirstOrDefaultAsync(p=>p.Id == productId);
        }

        public async Task<Guid> AddNewBrand(string Name)
        {
            var result = _productDb.Brands.Add(new Brand {Name= Name});
            await _productDb.SaveChangesAsync();
            return result.Entity.BrandId;
        }

        public bool getProductWithBrand()
        {
            return false;
            // _productDb.Brands.Include(b=>b.ProductsList).Select(c=> {c.Name});
        }

    }
}