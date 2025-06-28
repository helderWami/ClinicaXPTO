using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface IPedidoMarcacaoRepository
    {
        Task<IEnumerable<PedidoMarcacao>> GetAllAsync();
        Task<PedidoMarcacao> GetByIdAsync(int id);
        Task<PedidoMarcacao> AddAsync(PedidoMarcacao pedidoMarcacao);
        Task<bool> UpdateAsync(PedidoMarcacao pedidoMarcacao);
        Task<bool> DeleteAsync(int id); 
        Task<bool> AgendarPedidoAsync(AgendarPedidoDto agendarPedidoDto);
        Task<bool> RealizarPedidoAsync(int pedidoId, int utilizadorId, DateTime dataRealizacao);
        Task<IEnumerable<PedidoMarcacao>> ObterPorUtenteAsync(int utenteId);
        Task<IEnumerable<PedidoMarcacao>> ObterPorEstadoAsync(EstadoPedido estado);
        Task<PedidoMarcacao> ObterComItensAsync(int pedidoId);
        Task<IEnumerable<PedidoMarcacao>> PesquisarAsync(string numeroUtente, string nomeUtente, EstadoPedido? estado);
    }
}
