using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL.Repositories
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly ClinicaXPTODbContext _context;

        public ItemPedidoRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemPedido>> GetAllAsync()
        {
            return await _context.ItemPedidos.ToListAsync();
        }

        public async Task<ItemPedido> GetByIdAsync(int id)
        {
            var itemPedido = await _context.ItemPedidos.FirstOrDefaultAsync(i => i.Id == id);

            if (itemPedido == null)
            {
                throw new KeyNotFoundException($"ItemPedido com ID {id} nao encontrado.");
            }

            return itemPedido;
        }

        public async Task<ItemPedido> AddAsync(ItemPedido itemPedido)
        {
            _context.ItemPedidos.Add(itemPedido);
            await _context.SaveChangesAsync();
            return itemPedido;
        }

        public async Task<bool> UpdateAsync(ItemPedido itemPedido)
        {
            if (itemPedido == null)
                throw new ArgumentNullException(nameof(itemPedido));

            var existingItemPedido = await _context.ItemPedidos
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == itemPedido.Id);

            if (existingItemPedido == null)
                throw new KeyNotFoundException($"ItemPedido com ID {itemPedido.Id} não encontrado para atualização.");

            try
            {
                _context.Entry(itemPedido).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("A entidade foi modificada por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar item do pedido: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var itemPedido = await _context.ItemPedidos.FirstOrDefaultAsync(i => i.Id == id);
            if (itemPedido == null)
            {
                throw new KeyNotFoundException($"ItemPedido com ID {id} nao encontrado.");
            }
            _context.ItemPedidos.Remove(itemPedido);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
