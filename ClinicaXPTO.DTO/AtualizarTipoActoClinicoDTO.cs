using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ClinicaXPTO.DTO
{
    public class AtualizarTipoActoClinicoDTO
    {
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 200 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "O código não pode exceder 10 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        public TimeSpan? DuracaoPadrao { get; set; }

        [Range(0, 9999.99, ErrorMessage = "O preço deve estar entre 0 e 9999,99")]
        public decimal? Preco { get; set; }

        [StringLength(500, ErrorMessage = "As observações não podem exceder 500 caracteres")]
        public string Observacoes { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;
    }
}
