using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL.Repositories
{
    public class UtilizadorRepository : IUtilizadorRepository
    {
        private readonly ClinicaXPTODbContext _context;
        public UtilizadorRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Utilizador>> GetAllAsync()
        {
            return await _context.Utilizadores.ToListAsync();
        }
        public async Task<Utilizador> GetByIdAsync(int id)
        {
            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.Id == id);
            if (utilizador == null)
            {
                throw new KeyNotFoundException($"Utilizador com ID {id} nao encontrado.");
            }
            return utilizador;
        }
        public async Task<Utilizador> AddAsync(Utilizador utilizador)
        {
            _context.Utilizadores.Add(utilizador);
            await _context.SaveChangesAsync();
            return utilizador;
        }
        public async Task<bool> UpdateAsync(Utilizador utilizador)
        {
            _context.Utilizadores.Update(utilizador);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var utilizador = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Id == id);
            if (utilizador == null)
            {
                throw new KeyNotFoundException($"Utilizador com ID {id} nao encontrado.");
            }
            _context.Utilizadores.Remove(utilizador);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
