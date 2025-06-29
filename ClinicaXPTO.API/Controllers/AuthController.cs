using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.DTO;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.API.Controllers
{
    /// <summary>
    /// CONTROLADOR DE AUTENTICAÇÃO - LOGIN/LOGOUT
    /// ==========================================
    /// Este controlador gerencia a autenticação de utilizadores.
    /// 
    /// FUNCIONALIDADES:
    /// - Login com email/senha
    /// - Geração de JWT tokens
    /// - Logout (invalidação de token)
    /// - Registo de novos utilizadores
    /// - Alteração de senha
    /// 
    /// SEGURANÇA:
    /// - Senhas são hasheadas (não armazenadas em texto plano)
    /// - Tokens JWT com expiração
    /// - Validação de credenciais
    /// - Validação de força de senha
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUtilizadorService _utilizadorService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IUtilizadorService utilizadorService, IConfiguration configuration)
        {
            _authService = authService;
            _utilizadorService = utilizadorService;
            _configuration = configuration;
        }

        /// <summary>
        /// ENDPOINT: POST /api/auth/login
        /// ==============================
        /// Autentica um utilizador e retorna um token JWT
        /// 
        /// PARÂMETROS:
        /// - loginRequest: Email e senha do utilizador
        /// 
        /// RETORNO:
        /// - 200 OK: Token JWT e informações do utilizador
        /// - 401 Unauthorized: Credenciais inválidas
        /// - 400 Bad Request: Dados inválidos
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            if (loginRequest == null)
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = "Dados de login são obrigatórios",
                    Codigo = "INVALID_REQUEST"
                });

            try
            {
                // Validar credenciais
                var utilizador = await _authService.ValidarCredenciaisAsync(
                    loginRequest.Email, 
                    loginRequest.Senha
                );

                if (utilizador == null)
                    return Unauthorized(new AuthErrorResponseDTO 
                    { 
                        Mensagem = "Email ou senha inválidos",
                        Codigo = "INVALID_CREDENTIALS"
                    });

                // Gerar token JWT
                var token = _authService.GerarJwtToken(utilizador);
                var expiryHours = int.Parse(_configuration["Jwt:ExpiryInHours"] ?? "24");

                return Ok(new LoginResponseDTO
                {
                    Token = token,
                    ExpiraEm = DateTime.UtcNow.AddHours(expiryHours),
                    Utilizador = utilizador
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = $"Erro no login: {ex.Message}",
                    Codigo = "LOGIN_ERROR"
                });
            }
        }

        /// <summary>
        /// ENDPOINT: POST /api/auth/registar
        /// =================================
        /// Regista um novo utilizador
        /// 
        /// PARÂMETROS:
        /// - registarRequest: Dados do novo utilizador
        /// 
        /// RETORNO:
        /// - 201 Created: Utilizador criado com sucesso
        /// - 400 Bad Request: Dados inválidos
        /// </summary>
        [HttpPost("registar")]
        public async Task<IActionResult> Registar([FromBody] RegistarRequestDTO registarRequest)
        {
            if (registarRequest == null)
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = "Dados de registo são obrigatórios",
                    Codigo = "INVALID_REQUEST"
                });

            try
            {
                var novoUtilizador = await _authService.RegistarAsync(registarRequest);
                
                // Gerar token JWT para o novo utilizador
                var token = _authService.GerarJwtToken(novoUtilizador);
                var expiryHours = int.Parse(_configuration["Jwt:ExpiryInHours"] ?? "24");

                return CreatedAtAction(nameof(Login), new { id = novoUtilizador.Id }, new LoginResponseDTO
                {
                    Token = token,
                    ExpiraEm = DateTime.UtcNow.AddHours(expiryHours),
                    Utilizador = novoUtilizador
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = ex.Message,
                    Codigo = "VALIDATION_ERROR"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = $"Erro no registo: {ex.Message}",
                    Codigo = "REGISTRATION_ERROR"
                });
            }
        }

        /// <summary>
        /// ENDPOINT: POST /api/auth/logout
        /// ===============================
        /// Faz logout do utilizador (invalida o token)
        /// 
        /// NOTA: Em uma implementação real, você pode:
        /// - Adicionar o token a uma blacklist
        /// - Implementar refresh tokens
        /// - Limpar cookies do cliente
        /// 
        /// RETORNO:
        /// - 200 OK: Logout realizado com sucesso
        /// - 401 Unauthorized: Não autenticado
        /// </summary>
        [HttpPost("logout")]
        [Authorize] // Requer autenticação
        public IActionResult Logout()
        {
            // Em uma implementação real, você pode invalidar o token
            // Por exemplo, adicionando-o a uma blacklist no Redis
            
            return Ok(new { 
                Message = "Logout realizado com sucesso",
                Timestamp = DateTime.UtcNow
            });
        }

        /// <summary>
        /// ENDPOINT: GET /api/auth/perfil
        /// ===============================
        /// Obtém o perfil do utilizador autenticado
        /// 
        /// RETORNO:
        /// - 200 OK: Dados do utilizador
        /// - 401 Unauthorized: Não autenticado
        /// </summary>
        [HttpGet("perfil")]
        [Authorize] // Requer autenticação
        public async Task<IActionResult> ObterPerfil()
        {
            // Obtém o ID do utilizador do token JWT
            var utilizadorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(utilizadorId))
                return Unauthorized(new AuthErrorResponseDTO 
                { 
                    Mensagem = "Token inválido",
                    Codigo = "INVALID_TOKEN"
                });

            try
            {
                // Buscar utilizador por ID (implementar no service)
                // var utilizador = await _utilizadorService.GetByIdAsync(int.Parse(utilizadorId));
                
                // Por enquanto, retornar informações do token
                var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                var perfil = User.FindFirst("Perfil")?.Value;
                var ativo = User.FindFirst("Ativo")?.Value;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(perfil))
                    return Unauthorized(new AuthErrorResponseDTO 
                    { 
                        Mensagem = "Token inválido",
                        Codigo = "INVALID_TOKEN"
                    });

                var utilizadorInfo = new UtilizadorInfoDTO
                {
                    Id = int.Parse(utilizadorId),
                    Email = email,
                    Perfil = Enum.Parse<Perfil>(perfil),
                    Ativo = bool.Parse(ativo ?? "false")
                };

                return Ok(utilizadorInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = $"Erro ao obter perfil: {ex.Message}",
                    Codigo = "PROFILE_ERROR"
                });
            }
        }

        /// <summary>
        /// ENDPOINT: POST /api/auth/alterar-senha
        /// ======================================
        /// Altera a senha do utilizador autenticado
        /// 
        /// PARÂMETROS:
        /// - alterarSenhaRequest: Dados para alteração de senha
        /// 
        /// RETORNO:
        /// - 200 OK: Senha alterada com sucesso
        /// - 400 Bad Request: Dados inválidos
        /// - 401 Unauthorized: Não autenticado
        /// </summary>
        [HttpPost("alterar-senha")]
        [Authorize] // Requer autenticação
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO alterarSenhaRequest)
        {
            if (alterarSenhaRequest == null)
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = "Dados para alteração de senha são obrigatórios",
                    Codigo = "INVALID_REQUEST"
                });

            try
            {
                var utilizadorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(utilizadorId))
                    return Unauthorized(new AuthErrorResponseDTO 
                    { 
                        Mensagem = "Token inválido",
                        Codigo = "INVALID_TOKEN"
                    });

                var sucesso = await _authService.AlterarSenhaAsync(int.Parse(utilizadorId), alterarSenhaRequest);

                if (sucesso)
                    return Ok(new { 
                        Message = "Senha alterada com sucesso",
                        Timestamp = DateTime.UtcNow
                    });
                else
                    return BadRequest(new AuthErrorResponseDTO 
                    { 
                        Mensagem = "Erro ao alterar senha",
                        Codigo = "PASSWORD_CHANGE_ERROR"
                    });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = ex.Message,
                    Codigo = "VALIDATION_ERROR"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = $"Erro ao alterar senha: {ex.Message}",
                    Codigo = "PASSWORD_CHANGE_ERROR"
                });
            }
        }

        /// <summary>
        /// ENDPOINT: GET /api/auth/validar-email/{email}
        /// ============================================
        /// Valida se um email está disponível para registo
        /// 
        /// PARÂMETROS:
        /// - email: Email a validar
        /// 
        /// RETORNO:
        /// - 200 OK: { "Disponivel": true/false }
        /// - 400 Bad Request: Email inválido
        /// </summary>
        [HttpGet("validar-email/{email}")]
        [AllowAnonymous] // Endpoint público
        public async Task<IActionResult> ValidarEmailDisponivel(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = "Email é obrigatório",
                    Codigo = "INVALID_EMAIL"
                });

            try
            {
                var disponivel = await _authService.EmailDisponivelAsync(email);
                return Ok(new { Disponivel = disponivel });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthErrorResponseDTO 
                { 
                    Mensagem = $"Erro ao validar email: {ex.Message}",
                    Codigo = "EMAIL_VALIDATION_ERROR"
                });
            }
        }
    }
} 