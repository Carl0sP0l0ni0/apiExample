using Microsoft.AspNetCore.Mvc;
using ApiExample.Data;
using ApiExample.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ProductController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Devuelve todos los productos en la base de datos
            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            // Si el producto no se encuentra, devuelve un error 404
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Devuelve el producto creado con el código 201 (Creado)
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            // Si el ID del producto no coincide, devuelve un error 400 (Mala solicitud)
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 (Sin contenido)
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // Si el producto no existe, devuelve 404
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent(); // Devuelve 204 (Sin contenido)
        }
    }
}
