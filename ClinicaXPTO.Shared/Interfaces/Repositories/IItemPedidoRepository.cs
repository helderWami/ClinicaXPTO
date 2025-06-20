using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface IItemPedidoRepository
    {
        Task<IEnumerable<ItemPedido>> GetAllAsync();
        Task<ItemPedido> GetByIdAsync(int id);
        Task<ItemPedido> AddAsync(ItemPedido itemPedido);
        Task<bool> UpdateAsync(ItemPedido itemPedido);
        Task<bool> DeleteAsync(int id);
    }
}
