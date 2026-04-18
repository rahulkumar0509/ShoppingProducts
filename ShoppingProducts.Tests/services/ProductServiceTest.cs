using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShoppingProducts.API;
using ShoppingProducts.Domain;
using ShoppingProducts.Service;
namespace ShoppingProducts.Tests.services
{
    public class ProductServiceTest
    {
        // Helper method to create a clean In-Memory database for every test
        private ProductDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name per test
                .Options;

            return new ProductDbContext(options);
        }

        [Fact]
        public async Task CreateProduct_ShouldSaveToDatabase_AndReturnValidId()
        {
            // ARRANGE
            var db = GetDbContext();
            var service = new ProductService(db);
            var dto = new ProductDto{Name="iPhone 15 Pro Max", Description = "This is amazing and worlds no. 1 phone", Price = 150000};

            //ACT
            var resultId = await service.CreateProduct(dto);

            //ASSERT
            resultId.Should().NotBeEmpty();

            // Verify it actually exists in DB
            var productFromDb = db.Products.FirstOrDefault(p=> p.Id == resultId);
            productFromDb.Should().NotBeNull();
            productFromDb.Name.Should().Be("iPhone 15 Pro Max");
        }

        [Fact]
        public void GetProducts_ShouldGetAllProducts()
        {
            // ARRANGE
            var db = GetDbContext();
            var service = new ProductService(db);
            db.Products.AddRange(
                new List<Product>
                {
                    new() {Id= Guid.NewGuid(), Name = "p1", Description = "hello", Price = 100},
                    new() {Id = Guid.NewGuid(), Name="p2", Description = "hello", Price = 100}
                }
            );
            db.SaveChanges();
            
            // ACT
            var products = service.GetProducts();

            // ASSERT
            products.Should().NotBeNull();
            products.Should().HaveCountGreaterThan(1);
        }

        [Fact]
        public void GetProductById_ShouldReturnProduct_WhenIdExists()
        {
            // ARRANGE
            var db = GetDbContext();
            var service = new ProductService(db);
            var prodId = Guid.NewGuid();
            var dto = new Product{Name="phone", Description = "new phone", Price = 100, Id = prodId};
            db.Products.Add(dto);
            db.SaveChanges();

            // ACT
            var product = service.GetProductById(prodId);

            // ASSERT
            product.Should().NotBeNull();
            product.Name.Should().Be("phone");
            product.Id.Should().Be(prodId);
        }
    }
}