using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaXPTO.API.Controllers
{
    [ApiController]
    [Route("api/tipoActoClinico")]
    public class TipoActoClinicoController : ControllerBase
    {
        private readonly ITipoActoClinicoService _tipoActoClinicoService;
        public TipoActoClinicoController(ITipoActoClinicoService tipoActoClinicoService)
        {
            _tipoActoClinicoService = tipoActoClinicoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tipos = await _tipoActoClinicoService.GetAllAsync();
            return Ok(tipos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var tipo = await _tipoActoClinicoService.GetByIdAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }
            return Ok(tipo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CriarTipoActoClinico tipoActoClinico)
        {
            if (tipoActoClinico == null)
            {
                return BadRequest("TipoActoClinico nao pode ser null");
            }
            var createdTipo = await _tipoActoClinicoService.CreateAsync(tipoActoClinico);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdTipo.Id }, createdTipo);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AtualizarTipoActoClinicoDTO tipoActoClinico)
        {
            if (tipoActoClinico == null)
            {
                return BadRequest("TipoActoClinico nao pode ser null");
            }
            var updatedTipo = await _tipoActoClinicoService.UpdateAsync(id, tipoActoClinico);
            return Ok(updatedTipo);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _tipoActoClinicoService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
