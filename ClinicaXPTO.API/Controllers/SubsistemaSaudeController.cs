using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaXPTO.API.Controllers
{
    [ApiController]
    [Route("api/subsistema-saude")]
    public class SubsistemaSaudeController : ControllerBase
    {
        private readonly ISubsistemaSaudeService _subsistemaSaudeService;
        public SubsistemaSaudeController(ISubsistemaSaudeService subsistemaSaudeService)
        {
            _subsistemaSaudeService = subsistemaSaudeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var subsistemas = await _subsistemaSaudeService.GetAllAsync();
            return Ok(subsistemas);
        }

        [HttpGet("{id:int}", Name = "ObeterSubsistemaSaudePorId")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var subsistema = await _subsistemaSaudeService.GetByIdAsync(id);
            if (subsistema == null)
            {
                return NotFound();
            }
            return Ok(subsistema);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CriarSubsistemaSaudeDTO subsistemaSaude)
        {
            if (subsistemaSaude == null)
            {
                return BadRequest("SubsistemaSaude nao pode ser null");
            }
            var createdSubsistema = await _subsistemaSaudeService.CreateAsync(subsistemaSaude);
            return CreatedAtRoute("ObeterSubsistemaSaudePorId", new { id = createdSubsistema.Id }, createdSubsistema);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AtualizarSubsistemaSaudeDTO subsistemaSaude)
        {
            if (subsistemaSaude == null)
            {
                return BadRequest("SubsistemaSaude nao pode ser null");
            }
            var updatedSubsistema = await _subsistemaSaudeService.UpdateAsync(id, subsistemaSaude);
            if (updatedSubsistema == null)
            {
                return NotFound();
            }
            return Ok(updatedSubsistema);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _subsistemaSaudeService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}
