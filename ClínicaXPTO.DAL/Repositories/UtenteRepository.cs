using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL.Repositories
{
    public class UtenteRepository : IUtenteRepository
    {
        private readonly ClinicaXPTODbContext _context;

        public UtenteRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Utente>> GetAllAsync()
        {
            return await _context.Utentes.ToListAsync();
        }

        public async Task<Models.Utente> GetByIdAsync(int id)
        {
            var utente = await _context.Utentes
                .FirstOrDefaultAsync(u => u.Id == id);
            if (utente == null)
            {
                throw new KeyNotFoundException($"Utente com ID {id} nao encontrado.");
            }
            return utente;
        }

        public async Task<Models.Utente> AddAsync(Models.Utente utente)
        {
            _context.Utentes.Add(utente);
            await _context.SaveChangesAsync();
            return utente;
        }

        public async Task<bool> UpdateAsync(Models.Utente utente)
        {
            _context.Utentes.Update(utente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var utente = await _context.Utentes.FirstOrDefaultAsync(u => u.Id == id);
            if (utente == null)
            {
                throw new KeyNotFoundException($"Utente com ID {id} nao encontrado.");
            }
            _context.Utentes.Remove(utente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
