using System;
using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface IUtenteRepository
    {
        Task<IEnumerable<Utente>> GetAllAsync();
        Task<Utente> GetByIdAsync(int id);
        Task<Utente> AddAsync(Utente utente);
        Task<bool> UpdateAsync(Utente utente);
        Task<bool> DeleteAsync(int id);
    }
}
