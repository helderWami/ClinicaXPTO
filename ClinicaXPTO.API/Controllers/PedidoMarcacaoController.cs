using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaXPTO.API.Controllers
{
    [ApiController]
    [Route("api/pedidoMarcacao")]
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

        [HttpGet("{id:int}")]
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
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdPedido.Id }, createdPedido);
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
    }
}
