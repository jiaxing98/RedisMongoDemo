using Microsoft.AspNetCore.Mvc;
using RedisMongoDemo.Dto;
using RedisMongoDemo.Models;
using RedisMongoDemo.Services;

namespace RedisMongoDemo.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var result = await _productService.GetProductById(productId);
            if (result == null) return NotFound("No item found!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductDto dto)
        {
            await _productService.CreateProduct(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody]Product product)
        {
            try
            {
                await _productService.UpdateProduct(product);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody]Guid productId)
        {
            try
            {
                await _productService.DeleteProduct(productId);
                return Ok();
            } 
            catch(Exception ex)
            {
                return NotFound(ex);
            }

        }
    }
}
