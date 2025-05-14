using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDB.Data;
using ProjetoDB.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraficosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GraficosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> GetGrafico(int idUsuario)
        {
            var usuario = await _context.Usuario.FindAsync(idUsuario);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var tarefasDoUsuario = await _context.Tarefa
                .Where(t => t.IdUsuario == idUsuario)
                .ToListAsync();

            var total = tarefasDoUsuario.Count;
            var pendentes = tarefasDoUsuario.Count(t => t.Status.ToLower() == "pendente");
            var concluidas = tarefasDoUsuario.Count(t => t.Status.ToLower() == "concluida");
            var atrasadas = tarefasDoUsuario.Count(t =>
                t.Status.ToLower() != "concluida" &&
                t.DataVencimento < DateTime.Now);

            var grafico = new Graficos
            {
                idUsuario = usuario.Id,
                TarefasTotal = total,
                TarefasPendentes = pendentes,
                TarefasConcluidas = concluidas,
                TarefasAtrasadas = atrasadas,
                Usuario = new UsuarioDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                }
            };

            return Ok(grafico);
        }
    }
}
