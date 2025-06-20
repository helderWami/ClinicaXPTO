using ClinicaXPTO.DTO;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using Mapster;
using ClinicaXPTO.Models;

namespace ClinicaXPTO.Service.Services
{
    public class PedidoMarcacaoServico : IPedidoMarcacaoService
    {
        private readonly IPedidoMarcacaoRepository _pedidoMarcacao;

        public PedidoMarcacaoServico(IPedidoMarcacaoRepository pedidoMarcacao)
        {
            _pedidoMarcacao = pedidoMarcacao;
        }

        public async Task<IEnumerable<PedidoMarcacaoDTO>> GetAllAsync()
        {
            var pedidoMercacao = await _pedidoMarcacao.GetAllAsync();
            return pedidoMercacao.Adapt<IEnumerable<PedidoMarcacaoDTO>>();
        }

        public async Task<PedidoMarcacaoDTO> GetByIdAsync(int id)
        {
            var pedidoMercacao = await _pedidoMarcacao.GetByIdAsync(id);
            return pedidoMercacao.Adapt<PedidoMarcacaoDTO>();
        }

        public async Task<PedidoMarcacaoDTO> CreateAsync(CriarPedidoMarcacaoDTO pedidoMarcacaoDto)
        {
            var pedidoMarcacao = pedidoMarcacaoDto.Adapt<PedidoMarcacao>();
            var novoPedido = await _pedidoMarcacao.AddAsync(pedidoMarcacao);
            return novoPedido.Adapt<PedidoMarcacaoDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarPedidoMarcacaoDTO pedidoMarcacaoDto)
        {
            var pedidoMarcacao = pedidoMarcacaoDto.Adapt<PedidoMarcacao>();
            return await _pedidoMarcacao.UpdateAsync(pedidoMarcacao);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _pedidoMarcacao.DeleteAsync(id);
        }
    }
}
