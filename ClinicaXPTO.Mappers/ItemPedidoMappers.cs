using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;

namespace ClinicaXPTO.Mappers
{
    public static class ItemPedidoMappers
    {

        public static ItemPedidoDTO ToItemPedido( this ItemPedido item)
        {
            return new ItemPedidoDTO
            {
                Id = item.Id,
                TipoActoClinicoId = item.TipoActoClinicoId,
                SubsistemaSaudeId = item.SubsistemaSaudeId,
                ProfissionalId = item.ProfissionalId,
                HorarioSolicitado = item.HorarioSolicitado, 
                Observacoes = item.Observacoes
            };
        }

    }
}
