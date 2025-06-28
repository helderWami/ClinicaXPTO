using ClinicaXPTO.DTO;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using Mapster;
using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Service.Services
{
    public class PedidoMarcacaoServico : IPedidoMarcacaoService
    {
        private readonly IPedidoMarcacaoRepository _pedidoMarcacao;
        private readonly IUtenteRepository _utenteRepository;

        public PedidoMarcacaoServico(IPedidoMarcacaoRepository pedidoMarcacao, IUtenteRepository utenteRepository)
        {
            _pedidoMarcacao = pedidoMarcacao;
            _utenteRepository = utenteRepository;
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
            if (pedidoMarcacaoDto == null)
                throw new ArgumentNullException(nameof(pedidoMarcacaoDto));

            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            // Verificar se o pedido existe
            var pedidoExistente = await _pedidoMarcacao.GetByIdAsync(id);

            // Mapear apenas os campos que devem ser atualizados
            if (pedidoMarcacaoDto.InicioIntervalo.HasValue)
                pedidoExistente.InicioIntervalo = pedidoMarcacaoDto.InicioIntervalo.Value;
            
            if (pedidoMarcacaoDto.FimIntervalo.HasValue)
                pedidoExistente.FimIntervalo = pedidoMarcacaoDto.FimIntervalo.Value;
            
            if (pedidoMarcacaoDto.Estado.HasValue)
                pedidoExistente.Estado = pedidoMarcacaoDto.Estado.Value;
            
            if (!string.IsNullOrWhiteSpace(pedidoMarcacaoDto.Observacoes))
                pedidoExistente.Observacoes = pedidoMarcacaoDto.Observacoes;

            return await _pedidoMarcacao.UpdateAsync(pedidoExistente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _pedidoMarcacao.DeleteAsync(id);
        }

        public async Task<PedidoMarcacaoDTO> CriarPedidoAnonimoAsync(CriarUtenteDTO dadosUtente, CriarPedidoMarcacaoDTO pedidoMarcacaoDTO)
        {
            var utente = dadosUtente.Adapt<Utente>();
            var pedidoMarcacao = pedidoMarcacaoDTO.Adapt<PedidoMarcacao>();

            utente.EstadoUtente = EstadoUtente.UtenteAnonimo;

            var utenteCriado = await _utenteRepository.AddAsync(utente);

            pedidoMarcacao.UtenteId = utenteCriado.Id;

            var novoPedido = await _pedidoMarcacao.AddAsync(pedidoMarcacao);

            return novoPedido.Adapt<PedidoMarcacaoDTO>();
        }

        public async Task<bool> AgendarPedidoAsync(AgendarPedidoDto agendarPedidoDto)
        {
            return await _pedidoMarcacao.AgendarPedidoAsync(agendarPedidoDto);
        }

        public async Task<bool> RealizarPedidoAsync(int pedidoId, int utilizadorId, DateTime dataRealizacao)
        {
            return await _pedidoMarcacao.RealizarPedidoAsync(pedidoId, utilizadorId, dataRealizacao);
        }

        public async Task<IEnumerable<PedidoMarcacaoDTO>> ObterHistoricoPorUtenteAsync(int utenteId)
        {
            var pedidoMarcacoes = await _pedidoMarcacao.ObterPorUtenteAsync(utenteId); 

            return pedidoMarcacoes.Adapt<IEnumerable<PedidoMarcacaoDTO>>();
        }

        public async Task<IEnumerable<PedidoMarcacaoDTO>> PesquisarPedidosAsync(string numeroUtente, string nomeUtente, EstadoPedido? estado)
        {
            var pedidoMarcacoes = await _pedidoMarcacao.PesquisarAsync(numeroUtente, nomeUtente, estado);

            return pedidoMarcacoes.Adapt<IEnumerable<PedidoMarcacaoDTO>>();
        }

        public async Task<IEnumerable<PedidoMarcacaoDTO>> ObterPedidosPendentesAsync()
        {
            var pedidoMarcacoes = await _pedidoMarcacao.ObterPorEstadoAsync(EstadoPedido.Pendente);

            return pedidoMarcacoes.Adapt<IEnumerable<PedidoMarcacaoDTO>>();
        }
    }
}
