using ClinicaXPTO.DAL.Repositories;
using ClinicaXPTO.Service.Services;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using Mapster;

namespace ClinicaXPTO.API
{
    /// <summary>
    /// CLASSE RESPONSÁVEL PELA INJEÇÃO DE DEPENDÊNCIAS
    /// ================================================
    /// Esta classe registra todos os serviços, repositories e dependências
    /// no container de injeção de dependências do ASP.NET Core.
    /// 
    /// PRINCÍPIO: Dependency Injection (DI) - Inversão de Controle
    /// - Permite que as classes não criem suas dependências diretamente
    /// - Facilita testes unitários (mock das dependências)
    /// - Melhora a manutenibilidade e flexibilidade do código
    /// </summary>
    public static class DependencyInjections
    {
        /// <summary>
        /// MÉTODO PRINCIPAL DE REGISTO DE DEPENDÊNCIAS
        /// ===========================================
        /// Este método é chamado no Program.cs para registrar todas as dependências
        /// necessárias para o funcionamento da aplicação.
        /// 
        /// CICLO DE VIDA DOS SERVIÇOS:
        /// - AddScoped: Uma instância por requisição HTTP
        /// - AddTransient: Nova instância a cada injeção
        /// - AddSingleton: Uma única instância para toda a aplicação
        /// </summary>
        public static IServiceCollection AddClinicaXPTODependencies(this IServiceCollection services)
        {
            // ============================================================================
            // REGISTO DOS REPOSITORIES (CAMADA DE ACESSO A DADOS)
            // ============================================================================
            // Repositories implementam a lógica de acesso ao banco de dados
            // Cada repository é responsável por uma entidade específica
            
            services.AddScoped<IUtenteRepository, UtenteRepository>();
            services.AddScoped<ITipoActoClinicoRepository, TipoActoClinicoRepository>();
            services.AddScoped<ISubsistemaSaudeRepository, SubsistemaSaudeRepository>();
            services.AddScoped<IPedidoMarcacaoRepository, PedidoMarcacaoRepository>();
            services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
            services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
            services.AddScoped<IUtilizadorRepository, UtilizadorRepository>();

            // ============================================================================
            // REGISTO DOS SERVICES (CAMADA DE LÓGICA DE NEGÓCIO)
            // ============================================================================
            // Services contêm a lógica de negócio da aplicação
            // Coordenam as operações entre repositories e aplicam regras de negócio
            
            services.AddScoped<IUtenteService, UtenteService>();
            services.AddScoped<ITipoActoClinicoService, TipoActoClinicoService>();
            services.AddScoped<ISubsistemaSaudeService, SubsistemaSaudeService>();
            services.AddScoped<IPedidoMarcacaoService, PedidoMarcacaoServico>();
            services.AddScoped<IProfissionalService, ProfissionalService>();
            services.AddScoped<IItemPedidoService, ItemPedidoService>();
            services.AddScoped<IUtilizadorService, UtilizadorService>();
            
            // ============================================================================
            // REGISTO DO SERVICE DE AUTENTICAÇÃO
            // ============================================================================
            // Service responsável por toda a lógica de autenticação e autorização
            // Inclui validação de credenciais, geração de JWT, hash de senhas, etc.
            
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
