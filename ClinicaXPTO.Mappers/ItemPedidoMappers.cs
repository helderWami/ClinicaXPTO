using ClinicaXPTO.API.DTO;
using ClinicaXPTO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.Mappers
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
