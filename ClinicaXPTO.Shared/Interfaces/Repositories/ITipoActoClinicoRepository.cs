using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface ITipoActoClinicoRepository
    {
        Task<IEnumerable<TipoActoClinico>> GetAllAsync();
        Task<TipoActoClinico> GetByIdAsync(int id);
        Task<TipoActoClinico> AddAsync(TipoActoClinico tipoActoClinico);
        Task<bool> UpdateAsync(TipoActoClinico tipoActoClinico);
        Task<bool> DeleteAsync(int id);
    }
}
