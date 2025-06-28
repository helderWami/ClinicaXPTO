using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaXPTO.DTO
{
    public class AgendarPedidoDto
    {
        [Required(ErrorMessage = "O ID do pedido é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do pedido deve ser maior que zero")]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "A data/hora de agendamento é obrigatória")]
        public DateTime DataHoraAgendada { get; set; }

        [Required(ErrorMessage = "O ID do administrativo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do administrativo deve ser maior que zero")]
        public int AdministrativoId { get; set; }

        [StringLength(500, ErrorMessage = "As observações não podem exceder 500 caracteres")]
        public string Observacoes { get; set; } = string.Empty;

    }
}