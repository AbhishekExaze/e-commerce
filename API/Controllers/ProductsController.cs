using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> _productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type,string? sort)
        {
            var spec = new ProductSpecification(brand, type, sort);
            var products = await _productRepository.GetAllWithSpecAsync(spec);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _productRepository.Add(product);
            if(await _productRepository.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            return BadRequest("Failed to create product");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest();
            }
            _productRepository.Update(product);
            if(await _productRepository.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Failed to update product");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.Delete(product);
            if (!await _productRepository.SaveChangesAsync())
            {
                return BadRequest("Failed to delete product");
            }
            return NoContent();
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
        {
            var spec=new BrandListSpecification();
            var brands = await _productRepository.GetAllWithSpecAsync(spec);
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
        {
            var spec=new TypeListSpecification();
            var types = await _productRepository.GetAllWithSpecAsync(spec);
            return Ok(types);
        }

        private bool ProductExists(int id)
        {
            return _productRepository.Exists(id);
        }
    }
}
