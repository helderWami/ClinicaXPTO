using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaXPTO.API.Controllers
{

    namespace ClinicaXPTO.API.Controllers
    {

        [ApiController]
        [Route("api/utente")]
        public class UtenteController : ControllerBase
        {
            private readonly IUtenteService _utenteService;
            public UtenteController(IUtenteService utenteService)
            {
                _utenteService = utenteService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllAsync()
            {
                var utentes = await _utenteService.GetAllAsync();
                return Ok(utentes);
            }

            [HttpGet("{id:int}", Name = "ObeterUtentePorId")]
            public async Task<IActionResult> GetByIdAsync(int id)
            {
                var utente = await _utenteService.GetByIdAsync(id);
                if (utente == null)
                {
                    return NotFound();
                }
                return Ok(utente);
            }

            [HttpPost]
            public async Task<IActionResult> CreateAsync([FromBody] CriarUtenteDTO utente)
            {
                if (utente == null)
                {
                    return BadRequest("Utente nao pode ser null");
                }
                var createdUtente = await _utenteService.CreateAsync(utente);
                return CreatedAtRoute("ObeterUtentePorId", new { id = createdUtente.Id }, createdUtente);
            }

            [HttpPut("{id:int}")]
            public async Task<IActionResult> UpdateAsync(int id, [FromBody] AtualizarUtenteDTO utente)
            {
                if (utente == null)
                {
                    return BadRequest("Utente nao pode ser null");
                }
                var updatedUtente = await _utenteService.UpdateAsync(id, utente);
                return Ok(updatedUtente);
            }

            [HttpDelete("{id:int}")]
            public async Task<IActionResult> DeleteAsync(int id)
            {
                var deleted = await _utenteService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }
    }

}
