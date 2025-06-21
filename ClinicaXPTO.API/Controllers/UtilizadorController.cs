using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;


namespace ClinicaXPTO.API.Controllers
{
    [ApiController]
    [Route("api/utilizador")]

    public class UtilizadorController : ControllerBase
    {
        private readonly IUtilizadorService _utilizadorService;
        public UtilizadorController(IUtilizadorService utilizadorService)
        {
            _utilizadorService = utilizadorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var utilizadores = await _utilizadorService.GetAllAsync();
            return Ok(utilizadores);
        }

        [HttpGet("{id:int}", Name = "ObeterUtilizadorPorId")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var utilizador = await _utilizadorService.GetByIdAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            return Ok(utilizador);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CriarUtilizadorDTO utilizador)
        {
            if (utilizador == null)
            {
                return BadRequest("Utilizador nao pode ser null");
            }
            var createdUtilizador = await _utilizadorService.CreateAsync(utilizador);
            return CreatedAtRoute("ObeterUtilizadorPorId", new { id = createdUtilizador.Id }, createdUtilizador);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AtualizarUtilizadorDTO utilizador)
        {
            if (utilizador == null)
            {
                return BadRequest("Utilizador nao pode ser null");
            }
            var updatedUtilizador = await _utilizadorService.UpdateAsync(id, utilizador);
            return Ok(updatedUtilizador);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await _utilizadorService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
