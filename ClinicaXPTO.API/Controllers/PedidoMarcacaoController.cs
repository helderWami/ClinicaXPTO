using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.API.Controllers
{
    [ApiController]
    [Route("api/pedido-marcacao")]
    public class PedidoMarcacaoController : ControllerBase
    {
        private readonly IPedidoMarcacaoService _pedidoMarcacaoService;
        public PedidoMarcacaoController(IPedidoMarcacaoService pedidoMarcacaoService)
        {
            _pedidoMarcacaoService = pedidoMarcacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var pedidos = await _pedidoMarcacaoService.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id:int}", Name = "ObeterPedidoMarcacaoPorId")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var pedido = await _pedidoMarcacaoService.GetByIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CriarPedidoMarcacaoDTO pedido)
        {
            if (pedido == null)
            {
                return BadRequest("Pedido nao pode ser null");
            }
            var createdPedido = await _pedidoMarcacaoService.CreateAsync(pedido);
            return CreatedAtRoute("ObeterPedidoMarcacaoPorId", new { id = createdPedido.Id }, createdPedido);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AtualizarPedidoMarcacaoDTO pedido)
        {
            if (pedido == null)
            {
                return BadRequest("Pedido nao pode ser null");
            }
            var updatedPedido = await _pedidoMarcacaoService.UpdateAsync(id, pedido);

            return Ok(updatedPedido);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _pedidoMarcacaoService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("anonimo")]
        public async Task<IActionResult> CriarPedidoAnonimoAsync([FromBody] CriarPedidoAnonimoRequest request)
        {
            if (request?.DadosUtente == null || request?.PedidoMarcacao == null)
            {
                return BadRequest("Dados do utente e pedido são obrigatórios");
            }

            try
            {
                var pedidoCriado = await _pedidoMarcacaoService.CriarPedidoAnonimoAsync(request.DadosUtente, request.PedidoMarcacao);
                return Ok(pedidoCriado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar pedido anônimo: {ex.Message}");
            }
        }

        [HttpPut("{id:int}/agendar")]
        public async Task<IActionResult> AgendarPedidoAsync(int id, [FromBody] AgendarPedidoDto agendarPedidoDto)
        {
            if (agendarPedidoDto == null)
            {
                return BadRequest("Dados de agendamento são obrigatórios");
            }

            if (agendarPedidoDto.PedidoId != id)
            {
                return BadRequest("ID do pedido não confere");
            }

            try
            {
                var resultado = await _pedidoMarcacaoService.AgendarPedidoAsync(agendarPedidoDto);
                if (!resultado)
                {
                    return NotFound("Pedido não encontrado ou não pode ser agendado");
                }
                return Ok("Pedido agendado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao agendar pedido: {ex.Message}");
            }
        }

        [HttpPut("{id:int}/realizar")]
        public async Task<IActionResult> RealizarPedidoAsync(int id, [FromBody] RealizarPedidoRequest request)
        {
            if (request == null)
            {
                return BadRequest("Dados de realização são obrigatórios");
            }

            try
            {
                var resultado = await _pedidoMarcacaoService.RealizarPedidoAsync(id, request.UtilizadorId, request.DataRealizacao);
                if (!resultado)
                {
                    return NotFound("Pedido não encontrado ou não pode ser marcado como realizado");
                }
                return Ok("Pedido marcado como realizado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao realizar pedido: {ex.Message}");
            }
        }

        [HttpGet("utente/{utenteId:int}/historico")]
        public async Task<IActionResult> ObterHistoricoPorUtenteAsync(int utenteId)
        {
            try
            {
                var historico = await _pedidoMarcacaoService.ObterHistoricoPorUtenteAsync(utenteId);
                return Ok(historico);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter histórico: {ex.Message}");
            }
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> PesquisarPedidosAsync([FromQuery] string? numeroUtente, [FromQuery] string? nomeUtente, [FromQuery] EstadoPedido? estado)
        {
            try
            {
                var pedidos = await _pedidoMarcacaoService.PesquisarPedidosAsync(numeroUtente ?? string.Empty, nomeUtente ?? string.Empty, estado);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao pesquisar pedidos: {ex.Message}");
            }
        }

        [HttpGet("pendentes")]
        public async Task<IActionResult> ObterPedidosPendentesAsync()
        {
            try
            {
                var pedidosPendentes = await _pedidoMarcacaoService.ObterPedidosPendentesAsync();
                return Ok(pedidosPendentes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter pedidos pendentes: {ex.Message}");
            }
        }

        [HttpGet("{id:int}/exportar-pdf")]
        public async Task<IActionResult> ExportarPdfAsync(int id)
        {
            try
            {
                var pdfBytes = await _pedidoMarcacaoService.ExportarMarcacaoParaPdfAsync(id);
                return File(pdfBytes, "application/pdf", $"marcacao_{id}.pdf");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao exportar PDF: {ex.Message}");
            }
        }
    }

    // Classes auxiliares para requests
    public class CriarPedidoAnonimoRequest
    {
        public required CriarUtenteDTO DadosUtente { get; set; }
        public required CriarPedidoMarcacaoDTO PedidoMarcacao { get; set; }
    }

    public class RealizarPedidoRequest
    {
        public int UtilizadorId { get; set; }
        public DateTime DataRealizacao { get; set; }
    }
}
