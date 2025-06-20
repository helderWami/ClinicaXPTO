using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IUtilizadorService
    {
        Task<IEnumerable<UtilizadorDTO>> GetAllAsync();
        Task<UtilizadorDTO> GetByIdAsync(int id);
        Task<UtilizadorDTO> CreateAsync(CriarUtilizadorDTO utilizador);
        Task<bool> UpdateAsync(int id, AtualizarUtilizadorDTO utilizador);
        Task<bool> DeleteAsync(int id);
    }
}
