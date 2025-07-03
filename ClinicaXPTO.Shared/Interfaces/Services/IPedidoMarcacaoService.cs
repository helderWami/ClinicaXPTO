using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IPedidoMarcacaoService
    {
        Task<IEnumerable<PedidoMarcacaoDTO>> GetAllAsync();
        Task<PedidoMarcacaoDTO> GetByIdAsync(int id);
        Task<PedidoMarcacaoDTO> CreateAsync(CriarPedidoMarcacaoDTO pedido);
        Task<bool> UpdateAsync(int id, AtualizarPedidoMarcacaoDTO pedido);
        Task<bool> DeleteAsync(int id);

        // Use Case: Utente Anônimo criar pedido
        Task<PedidoMarcacaoDTO> CriarPedidoAnonimoAsync(CriarUtenteDTO dadosUtente, CriarPedidoMarcacaoDTO pedidoMarcacaoDTO);

        // Use Case: Administrativo agendar pedido (Pendente → Agendado)
        Task<bool> AgendarPedidoAsync(AgendarPedidoDto agendarPedidoDto);

        // Use Case: Marcar como realizado (Agendado → Realizado)
        Task<bool> RealizarPedidoAsync(int pedidoId, int utilizadorId, DateTime dataRealizacao);

        // Use Case: Utente Registado consultar histórico
        Task<IEnumerable<PedidoMarcacaoDTO>> ObterHistoricoPorUtenteAsync(int utenteId);

        // Use Case: Pesquisar pedidos (para administrativos)
        Task<IEnumerable<PedidoMarcacaoDTO>> PesquisarPedidosAsync(string numeroUtente, string nomeUtente, EstadoPedido? estado);

        // Use Case: Obter pedidos pendentes (para administrativos)
        Task<IEnumerable<PedidoMarcacaoDTO>> ObterPedidosPendentesAsync();

        // Exportação de detalhes para PDF
        Task<byte[]> ExportarMarcacaoParaPdfAsync(int pedidoId);
    }
}
