using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IUtenteService
    {
        Task<IEnumerable<UtenteDTO>> GetAllAsync();
        Task<UtenteDTO> GetByIdAsync(int id);
        Task<UtenteDTO> CreateAsync(CriarUtenteDTO utente);
        Task<bool> UpdateAsync(int id, AtualizarUtenteDTO utente);
        Task<bool> DeleteAsync(int id);
    }
}
