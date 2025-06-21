using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaXPTO.API.Controllers
{
    [ApiController]
    [Route("api/profissional")]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalService _profissionalService;
        public ProfissionalController(IProfissionalService profissionalService)
        {
            _profissionalService = profissionalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var profissionais = await _profissionalService.GetAllAsync();
            return Ok(profissionais);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var profissional = await _profissionalService.GetByIdAsync(id);
            if (profissional == null)
            {
                return NotFound();
            }
            return Ok(profissional);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CriarProfissionalDTO profissional)
        {
            if (profissional == null)
            {
                return BadRequest("Profissional nao pode ser null");
            }
            var createdProfissional = await _profissionalService.CreateAsync(profissional);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdProfissional.Id }, createdProfissional);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AtualizarProfissionalDTO profissional)
        {
            if (profissional == null)
            {
                return BadRequest("Profissional nao pode ser null");
            }
            var updatedProfissional = await _profissionalService.UpdateAsync(id, profissional);
            if (updatedProfissional == null)
            {
                return NotFound();
            }
            return Ok(updatedProfissional);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _profissionalService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

    }

}

