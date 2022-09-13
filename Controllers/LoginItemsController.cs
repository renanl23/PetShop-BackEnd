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
    public class LoginItemsController : ControllerBase
    {
        private readonly LoginContext _context;

        public LoginItemsController(LoginContext context)
        {
            _context = context;
        }

        // GET: api/LoginItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginItem>>> GetLoginItems()
        {
          if (_context.LoginItems == null)
          {
              return NotFound();
          }
            return await _context.LoginItems.ToListAsync();
        }

        // GET: api/LoginItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginItem>> GetLoginItem(long id)
        {
          if (_context.LoginItems == null)
          {
              return NotFound();
          }
            var loginItem = await _context.LoginItems.FindAsync(id);

            if (loginItem == null)
            {
                return NotFound();
            }

            return loginItem;
        }

        // PUT: api/LoginItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginItem(long id, LoginItem loginItem)
        {
            if (id != loginItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(loginItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginItemExists(id))
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

        // POST: api/LoginItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginItem>> PostLoginItem(LoginItem loginItem)
        {
          if (_context.LoginItems == null)
          {
              return Problem("Entity set 'LoginContext.LoginItems'  is null.");
          }
            _context.LoginItems.Add(loginItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoginItem", new { id = loginItem.Id }, loginItem);
        }

        // DELETE: api/LoginItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoginItem(long id)
        {
            if (_context.LoginItems == null)
            {
                return NotFound();
            }
            var loginItem = await _context.LoginItems.FindAsync(id);
            if (loginItem == null)
            {
                return NotFound();
            }

            _context.LoginItems.Remove(loginItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginItemExists(long id)
        {
            return (_context.LoginItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
