using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaXPTO.API.Controllers
{
    [ApiController]
    [Route("api/itemPedido")]
    public class ItemPedidoController : ControllerBase
    {
        private readonly IItemPedidoService _itemPedidoService;

        public ItemPedidoController(IItemPedidoService itemPedidoService)
        {
            _itemPedidoService = itemPedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemAsync()
        {
            var items = await _itemPedidoService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var item = await _itemPedidoService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ItemPedidoDTO itemPedido)
        {
            if (itemPedido == null)
            {
                return BadRequest("ItemPedido nao pode ser null");
            }
            var createdItem = await _itemPedidoService.CreateAsync(itemPedido);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AtualizarItemPedidoDTO itemPedido)
        {
            if (itemPedido == null)
            {
                return BadRequest("ItemPedido nao pode ser null");
            }
            var updatedItem = await _itemPedidoService.UpdateAsync(id, itemPedido);
            return Ok(updatedItem);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _itemPedidoService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}