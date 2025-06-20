using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IItemPedidoService
    {
        Task<ItemPedidoDTO> GetByIdAsync(int id);
        Task<IEnumerable<ItemPedidoDTO>> GetAllAsync();
        Task<ItemPedidoDTO> CreateAsync(ItemPedidoDTO itemPedido);
        Task<bool> UpdateAsync(int id, AtualizarItemPedidoDTO itemPedido);
        Task<bool> DeleteAsync(int id);
    }
}
