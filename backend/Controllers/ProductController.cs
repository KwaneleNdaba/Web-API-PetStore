using backend.Context;
using backend.DTOs;
using backend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]

        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDTO dto)
        {
            var newProduct = new ProductEntity()
            {
                Brand = dto.Brand,
                Title = dto.Title,
            };
            await _context.Product.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return Ok("Product Save Successfully");
        }

        [HttpGet]

        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductbyId([FromRoute] long id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(q => q.Id == id);
            if(product is null)
            {
                return NotFound("Product is not Found");
                
                
            }
            return Ok(product);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromBody] CreateUpdateProductDTO dto)
        {
            var product = await _context.Product.FirstOrDefaultAsync(q => q.Id == id);

            if(product is null)
            {
                return NotFound("Product not found");
            }

            product.Title = dto.Title;
            product.Brand = dto.Brand;
            product.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Product updated Succcessfully");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            
            var product = await _context.Product.FirstOrDefaultAsync(q => q.Id == id);
            
            if(product is null)
            {
                return NotFound("Product not found");
            }

             _context.Product.Remove(product);

            await _context.SaveChangesAsync();

            return Ok("Product deleted successfully");
        }
    }
}
