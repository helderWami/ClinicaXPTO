using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IPedidoMarcacaoService
    {
        Task<IEnumerable<PedidoMarcacaoDTO>> GetAllAsync();
        Task<PedidoMarcacaoDTO> GetByIdAsync(int id);
        Task<PedidoMarcacaoDTO> CreateAsync(CriarPedidoMarcacaoDTO pedido);
        Task<bool> UpdateAsync(int id, AtualizarPedidoMarcacaoDTO pedido);
        Task<bool> DeleteAsync(int id);
    }
}
