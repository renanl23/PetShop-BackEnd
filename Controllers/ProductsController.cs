using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop_BackEnd.Models;

namespace PetShop_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly LogContext _logcontext;

        public ProductsController(ProductContext context, LogContext logContext)
        {
            _context = context;
            _logcontext = logContext;

        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromHeader] UsuarioHeaders usuarioHeaders)
        {
        
        if (usuarioHeaders.username == null) {
            await LogProduct("GetProducts","Anonimo", true);
        } else {
            await LogProduct("GetProducts",usuarioHeaders.username, true);
        }

          if (_context.Products == null)
          {
              return NotFound();
          }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long id, [FromHeader] UsuarioHeaders usuarioHeaders)
        {
        
        if (usuarioHeaders.username == null) {
            await LogProduct("GetProduct","Anonimo", true);
        } else {
            await LogProduct("GetProduct",usuarioHeaders.username, true);
        }
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            if (usuarioHeaders.username == null) {
                        await LogProduct("GetProduct"+"/"+product.id,"Anonimo", true);
            } else {
                await LogProduct("GetProduct"+"/"+product.id,usuarioHeaders.username, true);
            }
            
            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, Product product, [FromHeader] UsuarioHeaders usuarioHeaders)
        {
            if (usuarioHeaders.username == null) {
                await LogProduct("PutProduct","Anonimo", false);
                return Unauthorized();
            } else if (usuarioHeaders.tipo == 0){
                await LogProduct("PutProduct",usuarioHeaders.username, false);
                return Forbid();            
            } else {
                await LogProduct("PutProduct",usuarioHeaders.username, true);
            }
            if (id != product.id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product, [FromHeader] UsuarioHeaders usuarioHeaders)
        {
            if (usuarioHeaders.username == null) {
                await LogProduct("PostProduct","Anonimo", false);
                return Unauthorized();
            } else if (usuarioHeaders.tipo == 0){
                await LogProduct("PostProduct",usuarioHeaders.username, false);
                return Forbid();            
            } else {
                await LogProduct("PostProduct",usuarioHeaders.username, true);
            }

          if (_context.Products == null)
          {
              return Problem("Entity set 'ProductContext.Products'  is null.");
          }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id, [FromHeader] UsuarioHeaders usuarioHeaders)
        {
            if (usuarioHeaders.username == null) {
                await LogProduct("DeleteProduct","Anonimo", false);
                return Unauthorized();
            } else if (usuarioHeaders.tipo == 0){
                await LogProduct("DeleteProduct",usuarioHeaders.username, false);
                return Forbid();            
            } else {
                await LogProduct("DeleteProduct",usuarioHeaders.username, true);
            }

            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return (_context.Products?.Any(e => e.id == id)).GetValueOrDefault();
        }

        public async Task LogProduct(string acao, string username, bool permitido)
        {
            _logcontext.Logs.Add(
                new Log { acao = acao, username = username, dateTime = DateTime.Now, endPoint = "/products", permitido = permitido });
            await _logcontext.SaveChangesAsync();
        }
    }
}
