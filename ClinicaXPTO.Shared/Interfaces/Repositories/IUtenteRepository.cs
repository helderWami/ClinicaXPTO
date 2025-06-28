using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface IUtenteRepository
    {
        Task<IEnumerable<Utente>> GetAllAsync();
        Task<Utente> GetByIdAsync(int id);
        Task<Utente> AddAsync(Utente utente);
        Task<bool> UpdateAsync(Utente utente);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Utente>> ObterUtentesAnonimosAsync();
        Task<bool> AtualizarEstadoUtenteAsync(int id, EstadoUtente estadoUtente);
        Task<Utente> ObterPorNumeroUtenteAsync(string numeroUtente);
        Task<Utente> ObterPorEmailAsync(string email);
        Task<bool> ExisteNumeroUtenteAsync(string numeroUtente);
        Task<bool> ExisteEmailAsync(string email);
        Task<IEnumerable<Utente>> PesquisarPorNomeAsync(string nome);
    }
}
