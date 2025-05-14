using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDB.Data;
using ProjetoDB.Models;
using ProjetoDB.Models.DTOs;

namespace ProjetoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public TarefasController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddTarefa([FromBody] Tarefas tarefas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _appDbContext.Tarefa.Add(tarefas);
            await _appDbContext.SaveChangesAsync();

            return Ok(tarefas);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefas>>> GetTarefas()
        {
            var tarefas = await _appDbContext.Tarefa.ToListAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefas>> GetTarefa(int id)
        {
            var tarefa = await _appDbContext.Tarefa.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada");
            }

            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarefa(int id, [FromBody] Tarefas tarefaAtualizada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tarefaExistente = await _appDbContext.Tarefa.FindAsync(id);

            if (tarefaExistente == null)
            {
                return NotFound("Tarefa não encontrada");
            }

            _appDbContext.Entry(tarefaExistente).CurrentValues.SetValues(tarefaAtualizada);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _appDbContext.Tarefa.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada");
            }

            _appDbContext.Tarefa.Remove(tarefa);
            await _appDbContext.SaveChangesAsync();

            return Ok("A tarefa foi deletada");
        }

        [HttpPost("custom")]
        public async Task<ActionResult<Tarefas>> CriarTarefaCustomizada([FromBody] TarefaCreateDTO dto)
        {
            var usuario = await _appDbContext.Usuario.FirstOrDefaultAsync(u => u.Nome == dto.nomeUsuario);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Validação básica dos componentes de data
            if (dto.Mes < 1 || dto.Mes > 12 || dto.Dia < 1 || dto.Dia > 31 || dto.Hora < 0 || dto.Hora > 23)
            {
                return BadRequest("Componentes de data/hora inválidos.");
            }

            DateTime dataTarefa;
            try
            {
                dataTarefa = new DateTime(dto.Ano, dto.Mes, dto.Dia, dto.Hora, 0, 0);
            }
            catch
            {
                return BadRequest("Data inválida.");
            }

            var tarefa = new Tarefas
            {
                Titulo = dto.Titulo,
                DataVencimento = dataTarefa,
                IdUsuario = usuario.Id
            };

            _appDbContext.Tarefa.Add(tarefa);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.IdTarefa }, tarefa);
        }
    }
}
