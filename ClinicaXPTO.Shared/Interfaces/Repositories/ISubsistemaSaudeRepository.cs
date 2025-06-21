using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface ISubsistemaSaudeRepository
    {
        Task<IEnumerable<SubsistemaSaude>> GetAllAsync();
        Task<SubsistemaSaude> GetByIdAsync(int id);
        Task<SubsistemaSaude> AddAsync(SubsistemaSaude subsistemaSaude);
        Task<bool> UpdateAsync(SubsistemaSaude subsistemaSaude);
        Task<bool> DeleteAsync(int id);
    }
}
