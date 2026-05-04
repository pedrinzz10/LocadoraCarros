using Microsoft.AspNetCore.Mvc;
using LocadoraCarros.Data;
using LocadoraCarros.DTOs;

namespace LocadoraCarros.Controllers
{
    
    /// Controller responsável pelo cálculo de locação de carros.
   
    [ApiController]
    [Route("api/[controller]")]
    public class LocacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocacoesController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------------------
        // POST /api/locacoes/calcular
        // Calcula o valor total de uma locação (sem salvar no banco)
        // ---------------------------------------------------------------
        
        /// Calcula o valor total de uma locação com base no carro e período informados.
        /// Aplica desconto de 10% para 7+ dias e 5% para 3+ dias.
        
        [HttpPost("calcular")]
        public async Task<ActionResult<LocacaoResponseDTO>> Calcular([FromBody] LocacaoRequestDTO request)
        {
            // Valida o modelo de entrada
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Busca o carro no banco de dados
            var carro = await _context.Carros.FindAsync(request.CarroId);

            if (carro == null)
                return NotFound(new { mensagem = $"Carro com ID {request.CarroId} não encontrado." });

            // Valida as datas: dataFim deve ser posterior a dataInicio
            if (request.DataFim <= request.DataInicio)
                return BadRequest(new { mensagem = "A data de fim deve ser posterior à data de início." });

            // -----------------------------------------------
            // Cálculo dos dias e subtotal
            // -----------------------------------------------
            int dias = (request.DataFim - request.DataInicio).Days;
            double subtotal = dias * carro.ValorDiaria;

            // -----------------------------------------------
            // Lógica de desconto conforme o número de dias
            // -----------------------------------------------
            double percentualDesconto;
            string descontoLabel;

            if (dias >= 7)
            {
                // Desconto de 10% para locações de 7 dias ou mais
                percentualDesconto = 0.10;
                descontoLabel = "10%";
            }
            else if (dias >= 3)
            {
                // Desconto de 5% para locações de 3 a 6 dias
                percentualDesconto = 0.05;
                descontoLabel = "5%";
            }
            else
            {
                // Sem desconto para locações de 1 a 2 dias
                percentualDesconto = 0.0;
                descontoLabel = "0%";
            }

            double valorFinal = subtotal - (subtotal * percentualDesconto);

            // -----------------------------------------------
            // Monta o relatório de resposta
            // -----------------------------------------------
            var response = new LocacaoResponseDTO
            {
                Carro      = carro.Modelo,
                Marca      = carro.Marca,
                DataInicio = request.DataInicio,
                DataFim    = request.DataFim,
                ValorDiaria = carro.ValorDiaria,
                Subtotal   = subtotal,
                Desconto   = descontoLabel,
                ValorFinal = valorFinal
            };

            return Ok(response);
        }
    }
}
