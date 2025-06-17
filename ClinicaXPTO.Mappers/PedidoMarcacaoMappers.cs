using ClinicaXPTO.API.DTO;
using ClinicaXPTO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.Mappers
{
    public static class PedidoMarcacaoMappers
    {

        public static PedidoMarcacaoDTO ToPedidoMarcacao (this PedidoMarcacao pedidoMarcacaoModel)
        {
            return new PedidoMarcacaoDTO
            {
                Id = pedidoMarcacaoModel.Id,
                UtenteId = pedidoMarcacaoModel.UtenteId,
                DataPedido = pedidoMarcacaoModel.DataPedido,
                InicioIntervalo = pedidoMarcacaoModel.InicioIntervalo,  
                FimIntervalo  = pedidoMarcacaoModel.FimIntervalo,
                Estado = pedidoMarcacaoModel.Estado.ToString(),
                Observacoes = pedidoMarcacaoModel.Observacoes,
                Itens = pedidoMarcacaoModel.Itens?.Select( i => i.ToItemPedido()).ToList() 

            };
        }

    }
}
