using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL.Repositories
{
    public class SubsistemaSaudeRepository : ISubsistemaSaudeRepository
    {
        private readonly ClinicaXPTODbContext _context;

        public SubsistemaSaudeRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubsistemaSaude>> GetAllAsync()
        {
            return await _context.SubsistemasSaude.ToListAsync();
        }

        public async Task<SubsistemaSaude> GetByIdAsync(int id)
        {
            var subsistemaSaude = await _context.SubsistemasSaude
                .FirstOrDefaultAsync(s => s.Id == id);
            if (subsistemaSaude == null)
            {
                throw new KeyNotFoundException($"SubsistemaSaude com ID {id} nao encontrado.");
            }
            return subsistemaSaude;
        }

        public async Task<SubsistemaSaude> AddAsync(SubsistemaSaude subsistemaSaude)
        {
            _context.SubsistemasSaude.Add(subsistemaSaude);
            await _context.SaveChangesAsync();
            return subsistemaSaude;
        }

        public async Task<bool> UpdateAsync(SubsistemaSaude subsistemaSaude)
        {
            if (subsistemaSaude == null)
                throw new ArgumentNullException(nameof(subsistemaSaude));

            var existingSubsistema = await _context.SubsistemasSaude
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == subsistemaSaude.Id);

            if (existingSubsistema == null)
                throw new KeyNotFoundException($"SubsistemaSaude com ID {subsistemaSaude.Id} não encontrado para atualização.");

            try
            {
                _context.Entry(subsistemaSaude).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("A entidade foi modificada por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar subsistema de saúde: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subsistemaSaude = await _context.SubsistemasSaude.FirstOrDefaultAsync(s => s.Id == id);
            if (subsistemaSaude == null)
            {
                throw new KeyNotFoundException($"SubsistemaSaude com ID {id} nao encontrado.");
            }
            _context.SubsistemasSaude.Remove(subsistemaSaude);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
