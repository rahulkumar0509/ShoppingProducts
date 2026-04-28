using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ShoppingProducts.Domain;

public class ProductControllerTest: IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient httpClient;
    public ProductControllerTest(CustomWebApplicationFactory factory)
    {
        httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllProduct_ShouldReturnOk()
    {
        // ACT
        var response = await httpClient.GetAsync("/Api/v1/product");

        // ASSERT
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();

        products.Should().NotBeNull();
        products.Should().HaveCountGreaterThan(1);
    }

    [Fact]
    public async Task AddProduct_ReturnSuccess()
    {
        // ARRANGE 
        var product = new Product {Name = "laptop", Description = "Macbook Pro Max", Price = 150000};

        // ACT
        var response = await httpClient.PostAsJsonAsync("/API/v1/Product", product);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        string message = await response.Content.ReadAsStringAsync() ?? ""; 

        message.Should().NotBeEmpty();
        string id = message.Split(":")[1];
        id.Should().NotBeEmpty();
    }

}