using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    /// <summary>
    /// INTERFACE DO REPOSITORY DE UTENTES
    /// ===================================
    /// Esta interface define o contrato para a camada de acesso a dados dos utentes.
    /// 
    /// RESPONSABILIDADES DO REPOSITORY:
    /// - Abstrai o acesso ao banco de dados
    /// - Implementa operações CRUD básicas
    /// - Fornece métodos específicos de consulta
    /// - Trabalha diretamente com as entidades (Models)
    /// - Não contém lógica de negócio
    /// 
    /// PRINCÍPIOS:
    /// - Repository Pattern: Abstrai a persistência de dados
    /// - Interface Segregation: Define apenas operações de utentes
    /// - Dependency Inversion: Depende de abstrações
    /// 
    /// VANTAGENS:
    /// - Facilita testes unitários (mock do repository)
    /// - Permite trocar a implementação de persistência
    /// - Centraliza operações de acesso a dados
    /// </summary>
    public interface IUtenteRepository
    {
        // ============================================================================
        // OPERAÇÕES CRUD BÁSICAS
        // ============================================================================
        
        /// <summary>
        /// Obtém todos os utentes do banco de dados
        /// </summary>
        /// <returns>Lista de entidades Utente</returns>
        Task<IEnumerable<Utente>> GetAllAsync();
        
        /// <summary>
        /// Obtém um utente específico pelo ID
        /// </summary>
        /// <param name="id">ID do utente</param>
        /// <returns>Entidade Utente ou null se não encontrado</returns>
        Task<Utente> GetByIdAsync(int id);
        
        /// <summary>
        /// Adiciona um novo utente ao banco de dados
        /// </summary>
        /// <param name="utente">Entidade Utente para adicionar</param>
        /// <returns>Entidade Utente com ID gerado</returns>
        Task<Utente> AddAsync(Utente utente);
        
        /// <summary>
        /// Atualiza um utente existente no banco de dados
        /// </summary>
        /// <param name="utente">Entidade Utente com dados atualizados</param>
        /// <returns>True se atualizado com sucesso</returns>
        Task<bool> UpdateAsync(Utente utente);
        
        /// <summary>
        /// Remove um utente do banco de dados
        /// </summary>
        /// <param name="id">ID do utente</param>
        /// <returns>True se removido com sucesso</returns>
        Task<bool> DeleteAsync(int id);

        // ============================================================================
        // OPERAÇÕES ESPECÍFICAS DO DOMÍNIO
        // ============================================================================
        
        /// <summary>
        /// Obtém apenas utentes anónimos (sem UtilizadorId)
        /// </summary>
        /// <returns>Lista de utentes anónimos</returns>
        Task<IEnumerable<Utente>> ObterUtentesAnonimosAsync();
        
        /// <summary>
        /// Atualiza o estado de um utente específico
        /// </summary>
        /// <param name="id">ID do utente</param>
        /// <param name="estadoUtente">Novo estado</param>
        /// <returns>True se atualizado com sucesso</returns>
        Task<bool> AtualizarEstadoUtenteAsync(int id, EstadoUtente estadoUtente);

        // ============================================================================
        // OPERAÇÕES DE CONSULTA POR CRITÉRIOS ESPECÍFICOS
        // ============================================================================
        
        /// <summary>
        /// Obtém um utente pelo número de utente
        /// </summary>
        /// <param name="numeroUtente">Número de utente</param>
        /// <returns>Entidade Utente ou null se não encontrado</returns>
        Task<Utente> ObterPorNumeroUtenteAsync(string numeroUtente);
        
        /// <summary>
        /// Obtém um utente pelo email
        /// </summary>
        /// <param name="email">Email do utente</param>
        /// <returns>Entidade Utente ou null se não encontrado</returns>
        Task<Utente> ObterPorEmailAsync(string email);

        // ============================================================================
        // OPERAÇÕES DE VALIDAÇÃO
        // ============================================================================
        
        /// <summary>
        /// Verifica se existe um utente com o número de utente especificado
        /// </summary>
        /// <param name="numeroUtente">Número de utente a verificar</param>
        /// <returns>True se existe, false caso contrário</returns>
        Task<bool> ExisteNumeroUtenteAsync(string numeroUtente);
        
        /// <summary>
        /// Verifica se existe um utente com o email especificado
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <returns>True se existe, false caso contrário</returns>
        Task<bool> ExisteEmailAsync(string email);

        // ============================================================================
        // OPERAÇÕES DE PESQUISA
        // ============================================================================
        
        /// <summary>
        /// Pesquisa utentes por nome (parcial)
        /// </summary>
        /// <param name="nome">Nome ou parte do nome</param>
        /// <returns>Lista de utentes que correspondem ao critério</returns>
        Task<IEnumerable<Utente>> PesquisarPorNomeAsync(string nome);
    }
}
