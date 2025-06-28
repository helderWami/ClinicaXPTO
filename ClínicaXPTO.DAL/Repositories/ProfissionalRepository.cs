using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly ClinicaXPTODbContext _context;

        public ProfissionalRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profissional>> GetAllAsync()
        {
            return await _context.Profissionais.ToListAsync();
        }

        public async Task<Profissional> GetByIdAsync(int id)
        {
            var profissional = await _context.Profissionais
                .FirstOrDefaultAsync(p => p.Id == id);

            if (profissional == null)
            {
                throw new KeyNotFoundException($"Profissional com ID {id} nao encontrado.");
            }
            return profissional;
        }

        public async Task<Profissional> AddAsync(Profissional profissional)
        {
            _context.Profissionais.Add(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }

        public async Task<bool> UpdateAsync(Profissional profissional)
        {
            if (profissional == null)
                throw new ArgumentNullException(nameof(profissional));

            var existingProfissional = await _context.Profissionais
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == profissional.Id);

            if (existingProfissional == null)
                throw new KeyNotFoundException($"Profissional com ID {profissional.Id} não encontrado para atualização.");

            try
            {
                _context.Entry(profissional).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("A entidade foi modificada por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar profissional: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profissional = await _context.Profissionais.FirstOrDefaultAsync(p => p.Id == id);
            if (profissional == null)
            {
                throw new KeyNotFoundException($"Profissional com ID {id} nao encontrado.");
            }
            _context.Profissionais.Remove(profissional);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
