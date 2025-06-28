using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaXPTO.DTO
{
    public class CriarItemPedidoDTO
    {
       
        [Required(ErrorMessage = "O tipo de ato clínico é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do tipo de ato clínico deve ser maior que zero")]
        public int TipoActoClinicoId { get; set; }

        [Required(ErrorMessage = "O subsistema de saúde é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do subsistema de saúde deve ser maior que zero")]
        public int SubsistemaSaudeId { get; set; }

        
        [Range(1, int.MaxValue, ErrorMessage = "O ID do profissional deve ser maior que zero quando especificado")]
        public int? ProfissionalId { get; set; }

        public TimeSpan? HorarioSolicitado { get; set; }

        [StringLength(500, ErrorMessage = "As observações não podem exceder 500 caracteres")]
        public string Observacoes { get; set; } = string.Empty;

    }
}
