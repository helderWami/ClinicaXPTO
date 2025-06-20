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
            _context.Profissionais.Update(profissional);
            await _context.SaveChangesAsync();
            return true;
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
