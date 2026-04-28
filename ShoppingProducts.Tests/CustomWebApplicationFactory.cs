using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingProducts.API;
using ShoppingProducts.Domain;

public class CustomWebApplicationFactory: WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove real DbContext
            var descriptor = services.SingleOrDefault(d=>d.ServiceType == typeof(ProductDbContext));
            if(descriptor != null)
            {
                services.Remove(descriptor);
            }

             // Add In-Memory database
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseInMemoryDatabase("test db");
            });

            // now build the provider
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

            db.Database.EnsureCreated();
            var prod = new Product {Name="phone", Description="Iphone 20", Price = 1000};
            db.Products.Add(prod);
            db.SaveChanges();
        });
       
    }
}