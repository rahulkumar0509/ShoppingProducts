using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShoppingProducts.Domain;
using ShoppingProducts.Service;

namespace ShoppingProducts.API
{
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [ApiController]
    [Route("Api/v{version:apiVersion}/Product")]
    public class ProductController: ControllerBase
    {
        private ProductService _productService; // reference
        public ProductController(ProductService productService) // instance
        {
            _productService = productService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            // var Service = new ProductService();
            try
            {
                var id = await _productService.CreateProduct(product);
                return Ok("Product Added with Id: " + id);
            } catch(Exception ex)
            {
                return StatusCode(500, "Unable to Add Product: " + ex);
            }
        }

        [MapToApiVersion(1)]
        [HttpGet("")]
        public IActionResult GetProducts()
        {
            try
            {
                var result = _productService.GetProducts();
                return Ok(result);
            } catch(Exception ex)
            {
                return StatusCode(500, "Unable to fetch the products: " + ex);
            }
        }

        [MapToApiVersion(2)]
        [HttpGet("{id}")]
        public IActionResult GetProductById(string id)
        {
            try
            {
                // Guid newId = Guid.Parse(id);
                Console.WriteLine(id);
                var result = _productService.GetProductById(new Guid(id));
                return Ok(result);
            } catch(Exception ex)
            {
                return StatusCode(500, "Unable to fetch the products: " + ex);
            }
        }
    }
}