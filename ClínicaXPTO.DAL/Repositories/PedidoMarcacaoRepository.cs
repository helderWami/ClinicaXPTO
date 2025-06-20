using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL.Repositories
{
    public class PedidoMarcacaoRepository : IPedidoMarcacaoRepository
    {
        private readonly ClinicaXPTODbContext _context;

        public PedidoMarcacaoRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoMarcacao>> GetAllAsync()
        {
            return await _context.PedidoMarcacoes.ToListAsync();
        }

        public async Task<PedidoMarcacao> GetByIdAsync(int id)
        {
            var pedidoMarcacao = await _context.PedidoMarcacoes
                .Include(p => p.Utente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedidoMarcacao == null)
            {
                throw new KeyNotFoundException($"PedidoMarcacao com ID {id} nao encontrado.");
            }

            return pedidoMarcacao;
        }

        public async Task<PedidoMarcacao> AddAsync(PedidoMarcacao pedidoMarcacao)
        {
            _context.PedidoMarcacoes.Add(pedidoMarcacao);
            await _context.SaveChangesAsync();
            return pedidoMarcacao;
        }

        public async Task<bool> UpdateAsync(PedidoMarcacao pedidoMarcacao)
        {
            _context.PedidoMarcacoes.Update(pedidoMarcacao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pedidoMarcacao = await _context.PedidoMarcacoes.FirstOrDefaultAsync(p => p.Id == id);
            if (pedidoMarcacao == null)
            {
                throw new KeyNotFoundException($"PedidoMarcacao com ID {id} nao encontrado.");
            }
            _context.PedidoMarcacoes.Remove(pedidoMarcacao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
