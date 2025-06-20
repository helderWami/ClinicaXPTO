using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL.Repositories 
{
    public class TipoActoClinicoRepository : ITipoActoClinicoRepository
    {
        private readonly ClinicaXPTODbContext _context;

        public TipoActoClinicoRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoActoClinico>> GetAllAsync()
        {
            return await _context.TipoActoClinicos.ToListAsync();
        }

        public async Task<TipoActoClinico> GetByIdAsync(int id)
        {
            var tipoActoClinico = await _context.TipoActoClinicos
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tipoActoClinico == null)
            {
                throw new KeyNotFoundException($"TipoActoClinico com ID {id} nao encontrado.");
            }
            return tipoActoClinico;
        }

        public async Task<TipoActoClinico> AddAsync(TipoActoClinico tipoActoClinico)
        {
            _context.TipoActoClinicos.Add(tipoActoClinico);
            await _context.SaveChangesAsync();
            return tipoActoClinico;
        }

        public async Task<bool> UpdateAsync(TipoActoClinico tipoActoClinico)
        {
            _context.TipoActoClinicos.Update(tipoActoClinico);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tipoActoClinico = await _context.TipoActoClinicos.FirstOrDefaultAsync(t => t.Id == id);
            if (tipoActoClinico == null)
            {
                throw new KeyNotFoundException($"TipoActoClinico com ID {id} nao encontrado.");
            }
            _context.TipoActoClinicos.Remove(tipoActoClinico);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
