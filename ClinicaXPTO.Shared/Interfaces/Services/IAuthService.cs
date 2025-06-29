using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    /// <summary>
    /// INTERFACE DO SERVICE DE AUTENTICAÇÃO
    /// ====================================
    /// Esta interface define o contrato para a lógica de autenticação e autorização.
    /// 
    /// RESPONSABILIDADES:
    /// - Validação de credenciais
    /// - Geração de tokens JWT
    /// - Hash e verificação de senhas
    /// - Registo de novos utilizadores
    /// - Gestão de sessões
    /// </summary>
    public interface IAuthService
    {
        // ============================================================================
        // OPERAÇÕES DE AUTENTICAÇÃO
        // ============================================================================
        
        /// <summary>
        /// Valida as credenciais de um utilizador
        /// </summary>
        /// <param name="email">Email do utilizador</param>
        /// <param name="senha">Senha em texto plano</param>
        /// <returns>Informações do utilizador se válido, null caso contrário</returns>
        Task<UtilizadorInfoDTO?> ValidarCredenciaisAsync(string email, string senha);

        /// <summary>
        /// Regista um novo utilizador no sistema
        /// </summary>
        /// <param name="request">Dados do novo utilizador</param>
        /// <returns>Informações do utilizador criado</returns>
        Task<UtilizadorInfoDTO> RegistarAsync(RegistarRequestDTO request);

        /// <summary>
        /// Gera um token JWT para um utilizador
        /// </summary>
        /// <param name="utilizador">Informações do utilizador</param>
        /// <returns>Token JWT</returns>
        string GerarJwtToken(UtilizadorInfoDTO utilizador);

        /// <summary>
        /// Valida um token JWT
        /// </summary>
        /// <param name="token">Token JWT</param>
        /// <returns>Informações do utilizador se válido, null caso contrário</returns>
        UtilizadorInfoDTO? ValidarToken(string token);

        // ============================================================================
        // OPERAÇÕES DE GESTÃO DE SENHAS
        // ============================================================================
        
        /// <summary>
        /// Altera a senha de um utilizador
        /// </summary>
        /// <param name="utilizadorId">ID do utilizador</param>
        /// <param name="request">Dados para alteração de senha</param>
        /// <returns>True se alterado com sucesso</returns>
        Task<bool> AlterarSenhaAsync(int utilizadorId, AlterarSenhaDTO request);

        /// <summary>
        /// Gera um hash seguro para uma senha
        /// </summary>
        /// <param name="senha">Senha em texto plano</param>
        /// <returns>Hash da senha</returns>
        string HashSenha(string senha);

        /// <summary>
        /// Verifica se uma senha corresponde a um hash
        /// </summary>
        /// <param name="senha">Senha em texto plano</param>
        /// <param name="hash">Hash armazenado</param>
        /// <returns>True se a senha corresponde ao hash</returns>
        bool VerificarSenha(string senha, string hash);

        // ============================================================================
        // OPERAÇÕES DE VALIDAÇÃO
        // ============================================================================
        
        /// <summary>
        /// Verifica se um email está disponível para registo
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <returns>True se disponível, false se já existe</returns>
        Task<bool> EmailDisponivelAsync(string email);

        /// <summary>
        /// Valida a força de uma senha
        /// </summary>
        /// <param name="senha">Senha a validar</param>
        /// <returns>True se a senha é forte o suficiente</returns>
        bool ValidarForcaSenha(string senha);
    }
} 