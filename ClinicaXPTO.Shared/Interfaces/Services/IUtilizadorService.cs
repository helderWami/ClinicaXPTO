using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IUtilizadorService
    {
        Task<IEnumerable<UtilizadorDTO>> GetAllAsync();
        Task<UtilizadorDTO> GetByIdAsync(int id);
        Task<UtilizadorDTO> CreateAsync(CriarUtilizadorDTO utilizador);
        Task<bool> UpdateAsync(int id, AtualizarUtilizadorDTO utilizador);
        Task<bool> DeleteAsync(int id);

        // Use Case: Login de utilizadores registados
        Task<UtilizadorDTO> AutenticarAsync(string email, string senha);

        // Use Case: Administrador criar utilizadores
        Task<UtilizadorDTO> CriarUtilizadorAsync(string email, string senha, Perfil perfil);

        // Validações
        Task<bool> ValidarCredenciaisAsync(string email, string senha);
    }
}
