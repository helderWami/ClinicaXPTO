using ClinicaXPTO.DAL.Repositories;
using ClinicaXPTO.Service.Services;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using Mapster;

namespace ClinicaXPTO.API
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddClinicaXPTODependencies(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUtenteRepository, UtenteRepository>();
            services.AddScoped<ITipoActoClinicoRepository, TipoActoClinicoRepository>();
            services.AddScoped<ISubsistemaSaudeRepository, SubsistemaSaudeRepository>();
            services.AddScoped<IPedidoMarcacaoRepository, PedidoMarcacaoRepository>();
            services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
            services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
            services.AddScoped<IUtenteRepository, UtenteRepository>();  

            // Services
            services.AddScoped<IUtenteService, UtenteService>();
            services.AddScoped<ITipoActoClinicoService, TipoActoClinicoService>();
            services.AddScoped<ISubsistemaSaudeService, SubsistemaSaudeService>();
            services.AddScoped<IPedidoMarcacaoService, PedidoMarcacaoServico>();
            services.AddScoped<IProfissionalService, ProfissionalService>();
            services.AddScoped<IItemPedidoService, ItemPedidoService>();
            services.AddScoped<IUtilizadorRepository, UtilizadorRepository>();
            

            return services;
        }
    }
}
