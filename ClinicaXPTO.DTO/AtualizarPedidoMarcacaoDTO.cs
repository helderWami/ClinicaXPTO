using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.DTO
{
    public class AtualizarPedidoMarcacaoDTO
    {
        public DateTime? InicioIntervalo { get; set; }
        public DateTime? FimIntervalo { get; set; }

        [StringLength(1000, ErrorMessage = "As observações não podem exceder 1000 caracteres")]
        public string Observacoes { get; set; } = string.Empty;

        public EstadoPedido? Estado { get; set; }

        public List<AtualizarItemPedidoDTO> Itens { get; set; } = new List<AtualizarItemPedidoDTO>();
    }
}
