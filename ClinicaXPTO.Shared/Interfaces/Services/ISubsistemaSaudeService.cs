using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface ISubsistemaSaudeService
    {
        Task<IEnumerable<SubsistemaSaudeDTO>> GetAllAsync();
        Task<SubsistemaSaudeDTO> GetByIdAsync(int id);
        Task<SubsistemaSaudeDTO> CreateAsync(CriarSubsistemaSaudeDTO subsistemaSaude);
        Task<bool> UpdateAsync(int id, AtualizarSubsistemaSaudeDTO subsistemaSaude);
        Task<bool> DeleteAsync(int id);
    }
}
