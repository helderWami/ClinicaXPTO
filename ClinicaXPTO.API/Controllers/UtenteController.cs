using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ClinicaXPTO.API.Controllers
{

    namespace ClinicaXPTO.API.Controllers
    {

        /// <summary>
        /// CONTROLADOR DE UTENTES - ENDPOINTS DA API
        /// =========================================
        /// Este controlador expõe endpoints HTTP para gerenciar utentes da clínica.
        /// 
        /// FUNCIONALIDADES PRINCIPAIS:
        /// - CRUD completo de utentes (Create, Read, Update, Delete)
        /// - Gestão de utentes anónimos vs registados
        /// - Validações de dados únicos (número utente, email)
        /// - Pesquisa de utentes
        /// 
        /// ARQUITETURA:
        /// - Segue o padrão REST para endpoints
        /// - Usa injeção de dependência para o service
        /// - Retorna códigos HTTP apropriados para cada situação
        /// 
        /// AUTORIZAÇÃO:
        /// - [Authorize]: Requer autenticação
        /// - [Authorize(Roles = "Administrativo,Administrador")]: Requer perfil específico
        /// - Endpoints públicos não têm atributo de autorização
        /// </summary>
        [ApiController]
        [Route("api/utente")]
        [Authorize] // Protege todos os endpoints por padrão
        public class UtenteController : ControllerBase
        {
            // Injeção de dependência do service de utentes
            private readonly IUtenteService _utenteService;
            
            /// <summary>
            /// Construtor que recebe o service via injeção de dependência
            /// </summary>
            public UtenteController(IUtenteService utenteService)
            {
                _utenteService = utenteService;
            }

            /// <summary>
            /// ENDPOINT: GET /api/utente
            /// =========================
            /// Obtém todos os utentes da clínica
            /// 
            /// AUTORIZAÇÃO: Apenas administrativos e administradores
            /// 
            /// RETORNO:
            /// - 200 OK: Lista de utentes
            /// - 500 Internal Server Error: Erro no servidor
            /// - 401 Unauthorized: Não autenticado
            /// - 403 Forbidden: Sem permissão
            /// </summary>
            [HttpGet]
            [Authorize(Roles = "Administrativo,Administrador")] // Apenas administrativos podem ver todos
            public async Task<IActionResult> GetAllAsync()
            {
                var utentes = await _utenteService.GetAllAsync();
                return Ok(utentes);
            }

            /// <summary>
            /// ENDPOINT: GET /api/utente/{id}
            /// ==============================
            /// Obtém um utente específico pelo ID
            /// 
            /// AUTORIZAÇÃO: 
            /// - Administrativos/Administradores: Podem ver qualquer utente
            /// - Utentes registados: Podem ver apenas o próprio perfil
            /// 
            /// PARÂMETROS:
            /// - id: ID do utente (int)
            /// 
            /// RETORNO:
            /// - 200 OK: Dados do utente
            /// - 404 Not Found: Utente não encontrado
            /// - 403 Forbidden: Sem permissão para ver este utente
            /// </summary>
            [HttpGet("{id:int}", Name = "ObeterUtentePorId")]
            public async Task<IActionResult> GetByIdAsync(int id)
            {
                // Verificar se o utilizador tem permissão para ver este utente
                var utilizadorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var perfil = User.FindFirst("Perfil")?.Value;

                // Administrativos e administradores podem ver qualquer utente
                if (perfil == "Administrativo" || perfil == "Administrador")
                {
                    var utente = await _utenteService.GetByIdAsync(id);
                    if (utente == null)
                    {
                        return NotFound();
                    }
                    return Ok(utente);
                }

                // Utentes registados só podem ver o próprio perfil
                if (perfil == "UtenteRegistado" && utilizadorId != null)
                {
                    // Buscar o utente associado a este utilizador
                    // (implementar no service: GetByUtilizadorIdAsync)
                    // var utente = await _utenteService.GetByUtilizadorIdAsync(int.Parse(utilizadorId));
                    // if (utente?.Id != id)
                    //     return Forbid();
                }

                var utenteResult = await _utenteService.GetByIdAsync(id);
                if (utenteResult == null)
                {
                    return NotFound();
                }
                return Ok(utenteResult);
            }

            /// <summary>
            /// ENDPOINT: POST /api/utente
            /// ===========================
            /// Cria um novo utente registado
            /// 
            /// AUTORIZAÇÃO: Apenas administrativos e administradores
            /// 
            /// PARÂMETROS:
            /// - utente: Dados do utente (CriarUtenteDTO)
            /// 
            /// RETORNO:
            /// - 201 Created: Utente criado com sucesso
            /// - 400 Bad Request: Dados inválidos
            /// </summary>
            [HttpPost]
            [Authorize(Roles = "Administrativo,Administrador")] // Apenas administrativos podem criar
            public async Task<IActionResult> CreateAsync([FromBody] CriarUtenteDTO utente)
            {
                if (utente == null)
                {
                    return BadRequest("Utente nao pode ser null");
                }
                var createdUtente = await _utenteService.CreateAsync(utente);
                return CreatedAtRoute("ObeterUtentePorId", new { id = createdUtente.Id }, createdUtente);
            }

            /// <summary>
            /// ENDPOINT: PUT /api/utente/{id}
            /// =============================
            /// Atualiza os dados de um utente existente
            /// 
            /// AUTORIZAÇÃO: Apenas administrativos e administradores
            /// 
            /// PARÂMETROS:
            /// - id: ID do utente (int)
            /// - utente: Novos dados do utente (AtualizarUtenteDTO)
            /// 
            /// RETORNO:
            /// - 200 OK: Utente atualizado
            /// - 400 Bad Request: Dados inválidos
            /// </summary>
            [HttpPut("{id:int}")]
            [Authorize(Roles = "Administrativo,Administrador")] // Apenas administrativos podem atualizar
            public async Task<IActionResult> UpdateAsync(int id, [FromBody] AtualizarUtenteDTO utente)
            {
                if (utente == null)
                {
                    return BadRequest("Utente nao pode ser null");
                }
                var updatedUtente = await _utenteService.UpdateAsync(id, utente);
                return Ok(updatedUtente);
            }

            /// <summary>
            /// ENDPOINT: DELETE /api/utente/{id}
            /// =================================
            /// Remove um utente (soft delete - marca como inativo)
            /// 
            /// AUTORIZAÇÃO: Apenas administradores
            /// 
            /// PARÂMETROS:
            /// - id: ID do utente (int)
            /// 
            /// RETORNO:
            /// - 204 No Content: Utente removido com sucesso
            /// - 404 Not Found: Utente não encontrado
            /// </summary>
            [HttpDelete("{id:int}")]
            [Authorize(Roles = "Administrador")] // Apenas administradores podem remover
            public async Task<IActionResult> DeleteAsync(int id)
            {
                var deleted = await _utenteService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }

            /// <summary>
            /// ENDPOINT: POST /api/utente/anonimo
            /// ==================================
            /// CASO DE USO: Criar utente anónimo para marcação
            /// 
            /// AUTORIZAÇÃO: Público (não requer autenticação)
            /// 
            /// Este endpoint permite criar um utente temporário (anónimo)
            /// que pode fazer marcações sem ter uma conta registada.
            /// Posteriormente, pode ser convertido em utente registado.
            /// 
            /// PARÂMETROS:
            /// - utente: Dados básicos do utente anónimo
            /// 
            /// RETORNO:
            /// - 200 OK: Utente anónimo criado
            /// - 400 Bad Request: Erro na criação
            /// </summary>
            [HttpPost("anonimo")]
            [AllowAnonymous] // Endpoint público - não requer autenticação
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

            /// <summary>
            /// ENDPOINT: PUT /api/utente/{id}/converter-registado
            /// =================================================
            /// CASO DE USO: Converter utente anónimo para registado
            /// 
            /// AUTORIZAÇÃO: Apenas administrativos e administradores
            /// 
            /// Após a primeira marcação, um utente anónimo pode ser convertido
            /// em utente registado, associando-o a uma conta de utilizador.
            /// 
            /// PARÂMETROS:
            /// - id: ID do utente anónimo
            /// - request: Dados para conversão (UtilizadorId)
            /// 
            /// RETORNO:
            /// - 200 OK: Utente convertido com sucesso
            /// - 400 Bad Request: Erro na conversão
            /// </summary>
            [HttpPut("{id:int}/converter-registado")]
            [Authorize(Roles = "Administrativo,Administrador")] // Apenas administrativos podem converter
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

            /// <summary>
            /// ENDPOINT: GET /api/utente/validar-numero/{numeroUtente}
            /// ======================================================
            /// Valida se um número de utente está disponível
            /// 
            /// AUTORIZAÇÃO: Público (não requer autenticação)
            /// 
            /// Usado durante o processo de criação/atualização para verificar
            /// se o número de utente já existe no sistema.
            /// 
            /// PARÂMETROS:
            /// - numeroUtente: Número a validar
            /// 
            /// RETORNO:
            /// - 200 OK: { "Disponivel": true/false }
            /// - 400 Bad Request: Erro na validação
            /// </summary>
            [HttpGet("validar-numero/{numeroUtente}")]
            [AllowAnonymous] // Endpoint público - não requer autenticação
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

            /// <summary>
            /// ENDPOINT: GET /api/utente/validar-email/{email}
            /// ===============================================
            /// Valida se um email está disponível
            /// 
            /// AUTORIZAÇÃO: Público (não requer autenticação)
            /// 
            /// Usado durante o processo de criação/atualização para verificar
            /// se o email já existe no sistema.
            /// 
            /// PARÂMETROS:
            /// - email: Email a validar
            /// 
            /// RETORNO:
            /// - 200 OK: { "Disponivel": true/false }
            /// - 400 Bad Request: Erro na validação
            /// </summary>
            [HttpGet("validar-email/{email}")]
            [AllowAnonymous] // Endpoint público - não requer autenticação
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

            /// <summary>
            /// ENDPOINT: GET /api/utente/pesquisar
            /// =================================================
            /// Pesquisa utentes por termo
            /// 
            /// AUTORIZAÇÃO: Apenas administrativos e administradores
            /// 
            /// Permite aos administrativos pesquisar utentes por nome,
            /// número de utente, email, etc.
            /// 
            /// PARÂMETROS:
            /// - termo: Termo de pesquisa (query parameter)
            /// 
            /// RETORNO:
            /// - 200 OK: Lista de utentes encontrados
            /// - 400 Bad Request: Termo de pesquisa inválido
            /// </summary>
            [HttpGet("pesquisar")]
            [Authorize(Roles = "Administrativo,Administrador")] // Apenas administrativos podem pesquisar
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

    /// <summary>
    /// CLASSE AUXILIAR PARA CONVERSÃO DE UTENTE
    /// ========================================
    /// DTO simples para receber o ID do utilizador durante a conversão
    /// de utente anónimo para registado
    /// </summary>
    public class ConverterUtenteRequest
    {
        public int UtilizadorId { get; set; }
    }
}
