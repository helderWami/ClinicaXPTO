using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.DTO
{
    /// <summary>
    /// DTOs PARA AUTENTICAÇÃO E AUTORIZAÇÃO
    /// =====================================
    /// Estas classes são usadas para transferir dados relacionados com autenticação
    /// entre o cliente e a API.
    /// </summary>

    /// <summary>
    /// DTO para requisição de login
    /// </summary>
    public class LoginRequestDTO
    {
        /// <summary>
        /// Email do utilizador
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do utilizador (em texto plano - será hasheada no servidor)
        /// </summary>
        public string Senha { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para requisição de registo de novo utilizador
    /// </summary>
    public class RegistarRequestDTO
    {
        /// <summary>
        /// Email do utilizador (deve ser único)
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do utilizador
        /// </summary>
        public string Senha { get; set; } = string.Empty;

        /// <summary>
        /// Confirmação da senha (deve ser igual à senha)
        /// </summary>
        public string ConfirmarSenha { get; set; } = string.Empty;

        /// <summary>
        /// Perfil do utilizador (padrão: UtenteRegistado)
        /// </summary>
        public Perfil Perfil { get; set; } = Perfil.UtenteRegistado;
    }

    /// <summary>
    /// DTO para resposta de login bem-sucedido
    /// </summary>
    public class LoginResponseDTO
    {
        /// <summary>
        /// Token JWT para autenticação
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Data de expiração do token
        /// </summary>
        public DateTime ExpiraEm { get; set; }

        /// <summary>
        /// Informações do utilizador autenticado
        /// </summary>
        public UtilizadorInfoDTO Utilizador { get; set; } = new();
    }

    /// <summary>
    /// DTO com informações básicas do utilizador (sem senha)
    /// </summary>
    public class UtilizadorInfoDTO
    {
        /// <summary>
        /// ID único do utilizador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Perfil/role do utilizador
        /// </summary>
        public Perfil Perfil { get; set; }

        /// <summary>
        /// Data de criação da conta
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Se a conta está ativa
        /// </summary>
        public bool Ativo { get; set; }
    }

    /// <summary>
    /// DTO para resposta de erro de autenticação
    /// </summary>
    public class AuthErrorResponseDTO
    {
        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public string Mensagem { get; set; } = string.Empty;

        /// <summary>
        /// Código do erro (opcional)
        /// </summary>
        public string? Codigo { get; set; }

        /// <summary>
        /// Data/hora do erro
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO para alteração de senha
    /// </summary>
    public class AlterarSenhaDTO
    {
        /// <summary>
        /// Senha atual
        /// </summary>
        public string SenhaAtual { get; set; } = string.Empty;

        /// <summary>
        /// Nova senha
        /// </summary>
        public string NovaSenha { get; set; } = string.Empty;

        /// <summary>
        /// Confirmação da nova senha
        /// </summary>
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }
} 