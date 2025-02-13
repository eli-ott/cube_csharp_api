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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDTO toUpdateProduct)
        {
            ReturnProductDTO modifiedProduct = await _productService.UpdateAsync(id, toUpdateProduct);
            return Ok(modifiedProduct);
        }

        [HttpPut("{id}/toggle-restock")]
        public async Task<IActionResult> ToggleAutoRestock([FromRoute] int id)
        {
            ReturnProductRestockDTO toggleProduct = await _productService.ToggleRestock(id);
            return Ok(toggleProduct);
        }

        [HttpPut("{id}/toggle-bio")]
        public async Task<IActionResult> ToggleBio([FromRoute] int id)
        {
            ReturnProductBioDTO toggleProduct = await _productService.ToggleIsBio(id);
            return Ok(toggleProduct);
        }
    }
}
