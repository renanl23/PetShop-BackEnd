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
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext _context;

        public UsuarioController(UsuarioContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, Usuario usuario)
        {
            if (id != usuario.id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'UsuarioContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.id }, usuario);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] Usuario usuario){

            if (usuario.username is null || usuario.password is null){
                return BadRequest();
            }

            // UsuarioAuth
            (bool retornoAuth, Usuario retornoUsuario) = UsuarioAuth(usuario.username, usuario.password);
            if (!retornoAuth)
                return NotFound(new { message = "Usuário ou senha inválidos" });                        
            
            return Ok(retornoUsuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(long id)
        {
            return (_context.Usuarios?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private (bool, Usuario) UsuarioAuth(string username,string password)
        {
            var usuarioAuth = false;
            Usuario usuarioRetorno = new Usuario {
                id = 0
                };

            var usuarioSelect = _context.Usuarios
            .Where(usuario => usuario.username == username && usuario.password == password)
            .Select(usuario => new Usuario {
                id = usuario.id,
                name = usuario.name,
                username = usuario.username,
                tipo = usuario.tipo
            }).ToList();
            if (usuarioSelect.Count > 0){
                usuarioAuth = true;
                usuarioRetorno = usuarioSelect[0]; 
            }                       
            return (usuarioAuth, usuarioRetorno);
        }
    }
}
