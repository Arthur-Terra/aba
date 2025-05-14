using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDB.Data;
using ProjetoDB.Models;

namespace ProjetoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UsuariosController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]

        public async Task<IActionResult> AddUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _appDbContext.Usuario.Add(usuario);
            await _appDbContext.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _appDbContext.Usuario.ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _appDbContext.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("O usuario não existe");
            }

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario
        (int id, [FromBody] Usuario usuarioAtualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioExistente = await _appDbContext.Usuario.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound("O usuario não existe");
            }

            _appDbContext.Entry(usuarioExistente).CurrentValues.SetValues(usuarioAtualizado);

            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario
        (int id)
        {
            var usuario = await _appDbContext.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("O usuario não existe");
            }

            _appDbContext.Usuario.Remove(usuario);

            await _appDbContext.SaveChangesAsync();

            return Ok("O usuario foi deletado");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = await _appDbContext.Usuario
                .FirstOrDefaultAsync(u => u.Email == usuarioDTO.Email);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos" });
            }

            // Aqui você pode retornar um token no futuro, por enquanto só retorna os dados
            return Ok(new
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            });
        }


    }
}