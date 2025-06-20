using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IProfissionalService
    {
        Task<IEnumerable<ProfissionalDTO>> GetAllAsync();
        Task<ProfissionalDTO> GetByIdAsync(int id);
        Task<ProfissionalDTO> CreateAsync(CriarProfissionalDTO profissional);
        Task<bool> UpdateAsync(int id, AtualizarProfissionalDTO profissional);
        Task<bool> DeleteAsync(int id);

    }
}
