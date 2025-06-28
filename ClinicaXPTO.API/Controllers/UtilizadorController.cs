using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;
using ClinicaXPTO.Models.Enuns;


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

        [HttpPost("autenticar")]
        public async Task<IActionResult> AutenticarAsync([FromBody] AutenticarRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest("Email e senha são obrigatórios");
            }

            try
            {
                var utilizador = await _utilizadorService.AutenticarAsync(request.Email, request.Senha);
                if (utilizador == null)
                {
                    return Unauthorized("Credenciais inválidas");
                }
                return Ok(utilizador);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na autenticação: {ex.Message}");
            }
        }

        [HttpPost("criar-admin")]
        public async Task<IActionResult> CriarUtilizadorAsync([FromBody] CriarUtilizadorAdminRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest("Email e senha são obrigatórios");
            }

            try
            {
                var utilizador = await _utilizadorService.CriarUtilizadorAsync(request.Email, request.Senha, request.Perfil);
                return Ok(utilizador);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar utilizador: {ex.Message}");
            }
        }

        [HttpPost("validar-credenciais")]
        public async Task<IActionResult> ValidarCredenciaisAsync([FromBody] AutenticarRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest("Email e senha são obrigatórios");
            }

            try
            {
                var validas = await _utilizadorService.ValidarCredenciaisAsync(request.Email, request.Senha);
                return Ok(new { CredenciaisValidas = validas });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao validar credenciais: {ex.Message}");
            }
        }
    }

    public class AutenticarRequest
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }

    public class CriarUtilizadorAdminRequest
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public Perfil Perfil { get; set; }
    }
}
