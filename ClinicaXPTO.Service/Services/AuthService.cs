using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Service.Services
{
    /// <summary>
    /// SERVICE DE AUTENTICAÇÃO - IMPLEMENTAÇÃO
    /// =======================================
    /// Esta classe implementa toda a lógica de autenticação e autorização.
    /// 
    /// FUNCIONALIDADES:
    /// - Validação de credenciais com hash de senhas
    /// - Geração e validação de tokens JWT
    /// - Registo de novos utilizadores
    /// - Gestão de senhas seguras
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUtilizadorRepository _utilizadorRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUtilizadorRepository utilizadorRepository, IConfiguration configuration)
        {
            _utilizadorRepository = utilizadorRepository;
            _configuration = configuration;
        }

        // ============================================================================
        // OPERAÇÕES DE AUTENTICAÇÃO
        // ============================================================================
        
        public async Task<UtilizadorInfoDTO?> ValidarCredenciaisAsync(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                return null;

            try
            {
                // Buscar utilizador por email
                var utilizador = await _utilizadorRepository.ObterPorEmailAsync(email);
                
                if (utilizador == null || !utilizador.Ativo)
                    return null;

                // Verificar se a senha está correta
                if (!VerificarSenha(senha, utilizador.Senha))
                    return null;

                // Retornar informações do utilizador (sem senha)
                return new UtilizadorInfoDTO
                {
                    Id = utilizador.Id,
                    Email = utilizador.Email,
                    Perfil = utilizador.Perfil,
                    DataCriacao = utilizador.DataCriacao,
                    Ativo = utilizador.Ativo
                };
            }
            catch (Exception)
            {
                // Em caso de erro, não expor informações sensíveis
                return null;
            }
        }

        public async Task<UtilizadorInfoDTO> RegistarAsync(RegistarRequestDTO request)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
                throw new ArgumentException("Email e senha são obrigatórios");

            if (request.Senha != request.ConfirmarSenha)
                throw new ArgumentException("As senhas não coincidem");

            if (!ValidarForcaSenha(request.Senha))
                throw new ArgumentException("A senha não é forte o suficiente");

            // Verificar se o email já existe
            var emailExiste = await EmailDisponivelAsync(request.Email);
            if (!emailExiste)
                throw new ArgumentException("Este email já está registado");

            // Criar novo utilizador
            var novoUtilizador = new Utilizador
            {
                Email = request.Email.ToLower().Trim(),
                Senha = HashSenha(request.Senha),
                Perfil = request.Perfil,
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            // Salvar no banco de dados
            var utilizadorSalvo = await _utilizadorRepository.AddAsync(novoUtilizador);

            // Retornar informações (sem senha)
            return new UtilizadorInfoDTO
            {
                Id = utilizadorSalvo.Id,
                Email = utilizadorSalvo.Email,
                Perfil = utilizadorSalvo.Perfil,
                DataCriacao = utilizadorSalvo.DataCriacao,
                Ativo = utilizadorSalvo.Ativo
            };
        }

        public string GerarJwtToken(UtilizadorInfoDTO utilizador)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            var expiryHours = int.Parse(_configuration["Jwt:ExpiryInHours"] ?? "24");

            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("Chave JWT não configurada");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Claims do token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, utilizador.Id.ToString()),
                new Claim(ClaimTypes.Email, utilizador.Email),
                new Claim(ClaimTypes.Role, utilizador.Perfil.ToString()),
                new Claim("Perfil", utilizador.Perfil.ToString()),
                new Claim("Ativo", utilizador.Ativo.ToString())
            };

            // Criar token
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiryHours),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UtilizadorInfoDTO? ValidarToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            try
            {
                var jwtKey = _configuration["Jwt:Key"];
                var jwtIssuer = _configuration["Jwt:Issuer"];
                var jwtAudience = _configuration["Jwt:Audience"];

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var tokenHandler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = jwtAudience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                // Validar token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // Extrair informações do utilizador
                var utilizadorId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var perfil = principal.FindFirst("Perfil")?.Value;
                var ativo = principal.FindFirst("Ativo")?.Value;

                if (string.IsNullOrEmpty(utilizadorId) || string.IsNullOrEmpty(email) || 
                    string.IsNullOrEmpty(perfil) || string.IsNullOrEmpty(ativo))
                    return null;

                // Verificar se o utilizador ainda está ativo
                if (ativo != "True")
                    return null;

                return new UtilizadorInfoDTO
                {
                    Id = int.Parse(utilizadorId),
                    Email = email,
                    Perfil = Enum.Parse<Perfil>(perfil),
                    Ativo = bool.Parse(ativo)
                };
            }
            catch (Exception)
            {
                // Token inválido
                return null;
            }
        }

        // ============================================================================
        // OPERAÇÕES DE GESTÃO DE SENHAS
        // ============================================================================
        
        public async Task<bool> AlterarSenhaAsync(int utilizadorId, AlterarSenhaDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.SenhaAtual) || 
                string.IsNullOrWhiteSpace(request.NovaSenha) || 
                string.IsNullOrWhiteSpace(request.ConfirmarNovaSenha))
                throw new ArgumentException("Todos os campos são obrigatórios");

            if (request.NovaSenha != request.ConfirmarNovaSenha)
                throw new ArgumentException("As senhas não coincidem");

            if (!ValidarForcaSenha(request.NovaSenha))
                throw new ArgumentException("A nova senha não é forte o suficiente");

            // Buscar utilizador
            var utilizador = await _utilizadorRepository.GetByIdAsync(utilizadorId);
            if (utilizador == null)
                throw new ArgumentException("Utilizador não encontrado");

            // Verificar senha atual
            if (!VerificarSenha(request.SenhaAtual, utilizador.Senha))
                throw new ArgumentException("Senha atual incorreta");

            // Atualizar senha
            utilizador.Senha = HashSenha(request.NovaSenha);
            return await _utilizadorRepository.UpdateAsync(utilizador);
        }

        public string HashSenha(string senha)
        {
            // Usar BCrypt para hash seguro
            // Em produção, instale o pacote: BCrypt.Net-Next
            // return BCrypt.Net.BCrypt.HashPassword(senha);
            
            // Implementação simples com SHA256 (não recomendado para produção)
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(hashedBytes);
        }

        public bool VerificarSenha(string senha, string hash)
        {
            // Usar BCrypt para verificação segura
            // return BCrypt.Net.BCrypt.Verify(senha, hash);
            
            // Implementação simples com SHA256
            var senhaHash = HashSenha(senha);
            return senhaHash == hash;
        }

        // ============================================================================
        // OPERAÇÕES DE VALIDAÇÃO
        // ============================================================================
        
        public async Task<bool> EmailDisponivelAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var utilizador = await _utilizadorRepository.ObterPorEmailAsync(email);
                return utilizador == null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ValidarForcaSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                return false;

            // Regras de força da senha
            var temMinimo8Caracteres = senha.Length >= 8;
            var temMaiuscula = Regex.IsMatch(senha, @"[A-Z]");
            var temMinuscula = Regex.IsMatch(senha, @"[a-z]");
            var temNumero = Regex.IsMatch(senha, @"\d");
            var temCaracterEspecial = Regex.IsMatch(senha, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]");

            // Senha deve ter pelo menos 3 destas regras
            var regrasAtendidas = new[] { temMinimo8Caracteres, temMaiuscula, temMinuscula, temNumero, temCaracterEspecial }
                .Count(regra => regra);

            return regrasAtendidas >= 3;
        }
    }
} 