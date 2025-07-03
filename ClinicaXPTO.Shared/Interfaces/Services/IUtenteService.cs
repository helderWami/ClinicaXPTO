using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    /// <summary>
    /// INTERFACE DO SERVICE DE UTENTES
    /// ===============================
    /// Esta interface define o contrato para a camada de lógica de negócio dos utentes.
    /// 
    /// RESPONSABILIDADES DO SERVICE:
    /// - Implementa a lógica de negócio da aplicação
    /// - Coordena operações entre repositories
    /// - Aplica regras de validação e negócio
    /// - Converte entre Models e DTOs
    /// - Gerencia transações quando necessário
    /// 
    /// PRINCÍPIOS:
    /// - Interface Segregation: Define apenas métodos relacionados a utentes
    /// - Dependency Inversion: Depende de abstrações (interfaces)
    /// - Single Responsibility: Foca apenas na lógica de utentes
    /// 
    /// CASOS DE USO IMPLEMENTADOS:
    /// - CRUD básico de utentes
    /// - Gestão de utentes anónimos vs registados
    /// - Validações de dados únicos
    /// - Pesquisa de utentes
    /// </summary>
    public interface IUtenteService
    {
        // ============================================================================
        // OPERAÇÕES CRUD BÁSICAS
        // ============================================================================
        
        /// <summary>
        /// Obtém todos os utentes ativos
        /// </summary>
        /// <returns>Lista de utentes convertidos para DTO</returns>
        Task<IEnumerable<UtenteDTO>> GetAllAsync();
        
        /// <summary>
        /// Obtém um utente específico pelo ID
        /// </summary>
        /// <param name="id">ID do utente</param>
        /// <returns>Utente convertido para DTO ou null se não encontrado</returns>
        Task<UtenteDTO> GetByIdAsync(int id);
        
        /// <summary>
        /// Cria um novo utente registado
        /// </summary>
        /// <param name="utente">Dados do utente para criar</param>
        /// <returns>Utente criado convertido para DTO</returns>
        Task<UtenteDTO> CreateAsync(CriarUtenteDTO utente);
        
        /// <summary>
        /// Atualiza os dados de um utente existente
        /// </summary>
        /// <param name="id">ID do utente</param>
        /// <param name="utente">Novos dados do utente</param>
        /// <returns>True se atualizado com sucesso</returns>
        Task<bool> UpdateAsync(int id, AtualizarUtenteDTO utente);
        
        /// <summary>
        /// Remove um utente (soft delete - marca como inativo)
        /// </summary>
        /// <param name="id">ID do utente</param>
        /// <returns>True se removido com sucesso</returns>
        Task<bool> DeleteAsync(int id);

        // ============================================================================
        // CASOS DE USO ESPECÍFICOS DO DOMÍNIO
        // ============================================================================
        
        /// <summary>
        /// CASO DE USO: Criar utente anónimo para marcação
        /// ================================================
        /// Permite criar um utente temporário que pode fazer marcações
        /// sem ter uma conta registada no sistema.
        /// 
        /// REGRAS DE NEGÓCIO:
        /// - Utente anónimo não tem UtilizadorId
        /// - Pode fazer marcações normalmente
        /// - Pode ser convertido para registado posteriormente
        /// </summary>
        /// <param name="utente">Dados básicos do utente anónimo</param>
        /// <returns>Utente anónimo criado</returns>
        Task<UtenteDTO> CriarUtenteAnonimoAsync(UtenteDTO utente);

        /// <summary>
        /// CASO DE USO: Converter utente anónimo para registado
        /// ====================================================
        /// Após a primeira marcação, um utente anónimo pode ser convertido
        /// em utente registado, associando-o a uma conta de utilizador.
        /// 
        /// REGRAS DE NEGÓCIO:
        /// - Só pode converter utentes anónimos (sem UtilizadorId)
        /// - O utilizador deve existir no sistema
        /// - Mantém todos os dados e marcações do utente
        /// </summary>
        /// <param name="utenteId">ID do utente anónimo</param>
        /// <param name="utilizadorId">ID do utilizador para associar</param>
        /// <returns>Utente convertido</returns>
        Task<UtenteDTO> ConverterParaRegistadoAsync(int utenteId, int utilizadorId);

        // ============================================================================
        // VALIDAÇÕES DE DADOS ÚNICOS
        // ============================================================================
        
        /// <summary>
        /// Valida se um número de utente está disponível
        /// </summary>
        /// <param name="numeroUtente">Número a validar</param>
        /// <returns>True se disponível, false se já existe</returns>
        Task<bool> ValidarNumeroUtenteDisponivelAsync(string numeroUtente);
        
        /// <summary>
        /// Valida se um email está disponível
        /// </summary>
        /// <param name="email">Email a validar</param>
        /// <returns>True se disponível, false se já existe</returns>
        Task<bool> ValidarEmailDisponivelAsync(string email);

        // ============================================================================
        // FUNCIONALIDADES DE PESQUISA
        // ============================================================================
        
        /// <summary>
        /// CASO DE USO: Pesquisar utentes
        /// ===============================
        /// Permite aos administrativos pesquisar utentes por diferentes critérios.
        /// 
        /// CRITÉRIOS DE PESQUISA:
        /// - Nome completo (parcial)
        /// - Número de utente
        /// - Email
        /// - Telemóvel
        /// 
        /// REGRAS DE NEGÓCIO:
        /// - Pesquisa apenas utentes ativos
        /// - Retorna resultados ordenados por nome
        /// - Suporta pesquisa parcial (contains)
        /// </summary>
        /// <param name="termo">Termo de pesquisa</param>
        /// <returns>Lista de utentes que correspondem ao critério</returns>
        Task<IEnumerable<UtenteDTO>> PesquisarUtentesAsync(string termo);

        /// <summary>
        /// Atualiza a fotografia do utente
        /// </summary>
        /// <param name="utenteId">ID do utente</param>
        /// <param name="caminhoFotografia">Caminho/URL da fotografia</param>
        /// <returns>True se atualizado com sucesso</returns>
        Task<bool> AtualizarFotografiaAsync(int utenteId, string caminhoFotografia);
    }
}
