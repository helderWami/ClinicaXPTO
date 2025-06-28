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

            [HttpPost("anonimo")]
            public async Task<IActionResult> CriarUtenteAnonimoAsync([FromBody] UtenteDTO utente)
            {
                if (utente == null)
                {
                    return BadRequest("Dados do utente são obrigatórios");
                }

                try
                {
                    var utenteAnonimo = await _utenteService.CriarUtenteAnonimoAsync(utente);
                    return Ok(utenteAnonimo);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao criar utente anônimo: {ex.Message}");
                }
            }

            [HttpPut("{id:int}/converter-registado")]
            public async Task<IActionResult> ConverterParaRegistadoAsync(int id, [FromBody] ConverterUtenteRequest request)
            {
                if (request == null)
                {
                    return BadRequest("Dados do utilizador são obrigatórios");
                }

                try
                {
                    var utenteConvertido = await _utenteService.ConverterParaRegistadoAsync(id, request.UtilizadorId);
                    return Ok(utenteConvertido);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao converter utente: {ex.Message}");
                }
            }

            [HttpGet("validar-numero/{numeroUtente}")]
            public async Task<IActionResult> ValidarNumeroUtenteDisponivelAsync(string numeroUtente)
            {
                try
                {
                    var disponivel = await _utenteService.ValidarNumeroUtenteDisponivelAsync(numeroUtente);
                    return Ok(new { Disponivel = disponivel });
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao validar número do utente: {ex.Message}");
                }
            }

            [HttpGet("validar-email/{email}")]
            public async Task<IActionResult> ValidarEmailDisponivelAsync(string email)
            {
                try
                {
                    var disponivel = await _utenteService.ValidarEmailDisponivelAsync(email);
                    return Ok(new { Disponivel = disponivel });
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao validar email: {ex.Message}");
                }
            }

            [HttpGet("pesquisar")]
            public async Task<IActionResult> PesquisarUtentesAsync([FromQuery] string termo)
            {
                if (string.IsNullOrWhiteSpace(termo))
                {
                    return BadRequest("Termo de pesquisa é obrigatório");
                }

                try
                {
                    var utentes = await _utenteService.PesquisarUtentesAsync(termo);
                    return Ok(utentes);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao pesquisar utentes: {ex.Message}");
                }
            }
        }
    }

    public class ConverterUtenteRequest
    {
        public int UtilizadorId { get; set; }
    }
}
