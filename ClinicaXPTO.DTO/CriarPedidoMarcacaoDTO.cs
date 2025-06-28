using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaXPTO.DTO
{
    public class CriarPedidoMarcacaoDTO 
    {
        [Required(ErrorMessage = "O ID do utente é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do utente deve ser maior que zero")]
        public int UtenteId { get; set; }

        public DateTime? InicioIntervalo { get; set; }

        public DateTime? FimIntervalo { get; set; }

        [StringLength(1000, ErrorMessage = "As observações não podem exceder 1000 caracteres")]
        public string Observacoes { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário especificar pelo menos um ato clínico")]
        [MinLength(1, ErrorMessage = "É necessário especificar pelo menos um ato clínico")]
        public List<CriarItemPedidoDTO> Itens { get; set; } = new List<CriarItemPedidoDTO>();
    }
}
