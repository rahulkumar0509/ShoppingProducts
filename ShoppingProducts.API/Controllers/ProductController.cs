using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Admin")]
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

        [MapToApiVersion(1)]
        [HttpGet("")]
        [Authorize(Roles ="Admin,User")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var result = _productService.GetProducts();
                // result.Categori
                return Ok(result);
            } catch(Exception ex)
            {
                return StatusCode(500, "Unable to fetch the products: " + ex);
            }
        }

        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,User")]
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

        [HttpGet("/Api/v{version:apiVersion}/category")]
        public IActionResult GetCategories()
        {
            return Ok(_productService.GetCategories());
        }

        [HttpGet("withCategories/{id}")]
        public async Task<IActionResult> GetProductWithCategoryById(string id)
        {
            try
            {
                Console.WriteLine($"input id: {id}");
                Guid prodId = new Guid(id);
                var result = await _productService.GetProductsWithCategory(prodId);
                return Ok(result);
            } catch(Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("Add-Brand/{Name}")]
        public async Task<IActionResult> AddBrand(string Name)
        {
            try
            {
                var id = await _productService.AddNewBrand(Name);
                return Ok($"Brand created with id: {id}");

            } catch(Exception ex)
            {
                return StatusCode(500, $"Failed to add this brand! {ex.Message}");
            }
        }
    }
}