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
            _context.SubsistemasSaude.Update(subsistemaSaude);
            await _context.SaveChangesAsync();
            return true;
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
