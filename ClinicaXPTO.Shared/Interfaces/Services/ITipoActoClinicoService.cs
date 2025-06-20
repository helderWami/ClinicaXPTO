using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface ITipoActoClinicoService
    {
        Task<IEnumerable<TipoActoClinicoDTO>> GetAllAsync();
        Task<TipoActoClinicoDTO> GetByIdAsync(int id);
        Task<TipoActoClinicoDTO> CreateAsync(CriarTipoActoClinico tipoActoClinico);
        Task<bool> UpdateAsync(int id, AtualizarTipoActoClinicoDTO tipoActoClinico);
        Task<bool> DeleteAsync(int id);
    }
}
