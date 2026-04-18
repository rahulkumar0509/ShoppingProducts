using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingProducts.Domain;
using ShoppingProducts.Service;

namespace ShoppingProducts.API
{
    [ApiController]
    [Route("Api/Product")]
    public class ProductController: ControllerBase
    {
        private ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            try
            {
                var id = await _productService.CreateProduct(product);
                return Ok("Product Added with Id: " + id);
            } catch(Exception ex)
            {
                return StatusCode(500, "Unable to Add Product: " + ex);
            }
        }

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