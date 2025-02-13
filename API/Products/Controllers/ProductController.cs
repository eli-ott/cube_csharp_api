using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Products.DTOs;
using MonApi.API.Products.Services;
using MonApi.API.Suppliers.Services;
using System.Diagnostics.CodeAnalysis;

namespace MonApi.API.Products.Controllers
{
    [ApiController]
    [Route("products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService ProductService)
        {
            _productService = ProductService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateProductDTO product)
        {
            var returnedProduct = await _productService.AddAsync(product);
            return Ok(returnedProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var returnedProducts = await _productService.GetAll();
            return Ok(returnedProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var returnedProduct = await _productService.GetById(id);
            return Ok(returnedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var test = await _productService.SoftDeleteAsync(id);
            return Ok(test);
        }
    }
}
