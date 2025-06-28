using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Models;
using ClinicaXPTO.DTO;
using Mapster;

namespace ClinicaXPTO.Service.Services
{
    public class ItemPedidoService : IItemPedidoService
    {
        public readonly IItemPedidoRepository _itemPedidoRepository;

        public ItemPedidoService(IItemPedidoRepository itemPedidoRepository)
        {
            _itemPedidoRepository = itemPedidoRepository;
        }

        public async Task<IEnumerable<ItemPedidoDTO>> GetAllAsync()
        {
            var itemPedidos = await _itemPedidoRepository.GetAllAsync();

            return itemPedidos.Adapt<IEnumerable<ItemPedidoDTO>>();
        }

        public async Task<ItemPedidoDTO> GetByIdAsync(int id)
        {
            var itemPedido = await _itemPedidoRepository.GetByIdAsync(id);

            return itemPedido.Adapt<ItemPedidoDTO>();
        }

        public async Task<ItemPedidoDTO> CreateAsync(ItemPedidoDTO itemPedidoDto)
        {
            var itemPedido = itemPedidoDto.Adapt<ItemPedido>();
            var novoItemPedido = await _itemPedidoRepository.AddAsync(itemPedido);

            return novoItemPedido.Adapt<ItemPedidoDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarItemPedidoDTO itemPedidoDto)
        {
            if (itemPedidoDto == null)
                throw new ArgumentNullException(nameof(itemPedidoDto));

            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            // Verificar se o item pedido existe
            var itemPedidoExistente = await _itemPedidoRepository.GetByIdAsync(id);

            // Mapear apenas os campos que devem ser atualizados
            itemPedidoExistente.TipoActoClinicoId = itemPedidoDto.TipoActoClinicoId;
            itemPedidoExistente.SubsistemaSaudeId = itemPedidoDto.SubsistemaSaudeId;
            itemPedidoExistente.ProfissionalId = itemPedidoDto.ProfissionalId;
            itemPedidoExistente.HorarioSolicitado = itemPedidoDto.HorarioSolicitado;
            itemPedidoExistente.Observacoes = itemPedidoDto.Observacoes;

            return await _itemPedidoRepository.UpdateAsync(itemPedidoExistente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _itemPedidoRepository.DeleteAsync(id);
        }

    }
}
