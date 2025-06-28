using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClinicaXPTO.DAL.Repositories
{
    public class UtenteRepository : IUtenteRepository
    {
        private readonly ClinicaXPTODbContext _context;

        public UtenteRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Utente>> GetAllAsync()
        {
            return await _context.Utentes.ToListAsync();
        }

        public async Task<Utente> GetByIdAsync(int id)
        {
            var utente = await _context.Utentes
                .FirstOrDefaultAsync(u => u.Id == id);
            if (utente == null)
            {
                throw new KeyNotFoundException($"Utente com ID {id} nao encontrado.");
            }
            return utente;
        }

        public async Task<Utente> AddAsync(Utente utente)
        {
            _context.Utentes.Add(utente);
            await _context.SaveChangesAsync();
            return utente;
        }

        public async Task<bool> UpdateAsync(Utente utente)
        {
            if (utente == null)
                throw new ArgumentNullException(nameof(utente));

            var existingUtente = await _context.Utentes
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == utente.Id);

            if (existingUtente == null)
                throw new KeyNotFoundException($"Utente com ID {utente.Id} não encontrado para atualização.");

            try
            {
                _context.Entry(utente).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("A entidade foi modificada por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar utente: {ex.Message}", ex);
            }
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
        public async Task<IEnumerable<Utente>> ObterUtentesAnonimosAsync()
        {
            return await _context.Utentes
                .Where(u => u.EstadoUtente == EstadoUtente.UtenteAnonimo)
                .ToListAsync();
        }

        public async Task<bool> AtualizarEstadoUtenteAsync(int id, EstadoUtente estadoUtente)
        {
            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            var utente = await _context.Utentes.FirstOrDefaultAsync(u => u.Id == id);
            if (utente == null)
            {
                throw new KeyNotFoundException($"Utente com ID {id} não encontrado.");
            }

            if (utente.EstadoUtente == estadoUtente)
                return true; // Não há mudança necessária

            try
            {
                utente.EstadoUtente = estadoUtente;
                _context.Entry(utente).Property(u => u.EstadoUtente).IsModified = true;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("A entidade foi modificada por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar estado do utente: {ex.Message}", ex);
            }
        }

        public async Task<Utente> ObterPorNumeroUtenteAsync(string numeroUtente)
        {
            var utente = await _context.Utentes
                .FirstOrDefaultAsync(u => u.NumeroUtente == numeroUtente);

            if (utente == null)
            {
                throw new KeyNotFoundException($"Utente com Numero Utente {numeroUtente} nao encontrado.");
            }

            return utente;
        }

        public async Task<Utente> ObterPorEmailAsync(string email)
        {
            var utente = await _context.Utentes
                .FirstOrDefaultAsync(u => u.EmailContacto == email);

            if (utente == null)
            {
                throw new KeyNotFoundException($"Utente com Email {email} nao encontrado.");
            }

            return utente;
        }

        public async Task<bool> ExisteNumeroUtenteAsync(string numeroUtente)
        {
            return await _context.Utentes
                .AnyAsync(u => u.NumeroUtente == numeroUtente);
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _context.Utentes
                .AnyAsync(u => u.EmailContacto == email);
        }

        public async Task<IEnumerable<Utente>> PesquisarPorNomeAsync(string nome)
        {
            return await _context.Utentes
                .Where(u => u.NomeCompleto.Contains(nome, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

    }
}
