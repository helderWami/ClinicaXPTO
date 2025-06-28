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
            if (utilizador == null)
                throw new ArgumentNullException(nameof(utilizador));

            var existingUtilizador = await _context.Utilizadores
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == utilizador.Id);

            if (existingUtilizador == null)
                throw new KeyNotFoundException($"Utilizador com ID {utilizador.Id} não encontrado para atualização.");

            try
            {
                _context.Entry(utilizador).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("A entidade foi modificada por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar utilizador: {ex.Message}", ex);
            }
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

        public async Task<Utilizador> ObterPorEmailAsync(string email)
        {
            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.Email == email);

            if (utilizador == null)
            {
                throw new KeyNotFoundException($"Utilizador com Email {email} nao encontrado.");
            }

            return utilizador;
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _context.Utilizadores
                .AnyAsync(u => u.Email == email);
        }
    }
}
