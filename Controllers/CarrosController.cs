using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraCarros.Data;
using LocadoraCarros.Models;

namespace LocadoraCarros.Controllers
{

    /// Controller responsável pelo CRUD completo de Carros.

    [ApiController]
    [Route("api/[controller]")]
    public class CarrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarrosController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------------------
        // GET /api/carros
        // Retorna todos os carros cadastrados
        // ---------------------------------------------------------------

        /// Lista todos os carros.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carro>>> GetAll()
        {
            var carros = await _context.Carros.ToListAsync();
            return Ok(carros);
        }

        // ---------------------------------------------------------------
        // GET /api/carros/{id}
        // Retorna um carro específico pelo ID
        // ---------------------------------------------------------------

        /// Busca um carro pelo ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> GetById(int id)
        {
            var carro = await _context.Carros.FindAsync(id);

            if (carro == null)
                return NotFound(new { mensagem = $"Carro com ID {id} não encontrado." });

            return Ok(carro);
        }

        // ---------------------------------------------------------------
        // POST /api/carros
        // Cria um novo carro
        // ---------------------------------------------------------------

        /// Cadastra um novo carro.
        [HttpPost]
        public async Task<ActionResult<Carro>> Create([FromBody] Carro carro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Carros.Add(carro);
            await _context.SaveChangesAsync();

            // Retorna 201 Created com o recurso criado e sua URL
            return CreatedAtAction(nameof(GetById), new { id = carro.Id }, carro);
        }

        // ---------------------------------------------------------------
        // PUT /api/carros/{id}
        // Atualiza um carro existente
        // ---------------------------------------------------------------

        /// Atualiza os dados de um carro existente.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Carro carroAtualizado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carro = await _context.Carros.FindAsync(id);

            if (carro == null)
                return NotFound(new { mensagem = $"Carro com ID {id} não encontrado." });

            // Atualiza apenas os campos permitidos
            carro.Modelo      = carroAtualizado.Modelo;
            carro.Marca       = carroAtualizado.Marca;
            carro.Ano         = carroAtualizado.Ano;
            carro.ValorDiaria = carroAtualizado.ValorDiaria;

            await _context.SaveChangesAsync();

            return Ok(carro);
        }

        // ---------------------------------------------------------------
        // DELETE /api/carros/{id}
        // Remove um carro do sistema
        // ---------------------------------------------------------------
        
        /// Remove um carro pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carro = await _context.Carros.FindAsync(id);

            if (carro == null)
                return NotFound(new { mensagem = $"Carro com ID {id} não encontrado." });

            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
