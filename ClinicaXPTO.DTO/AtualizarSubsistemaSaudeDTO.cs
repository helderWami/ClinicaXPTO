using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ClinicaXPTO.DTO
{
    public class AtualizarSubsistemaSaudeDTO
    {
        [Required(ErrorMessage = "O nome do subsistema é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "O código não pode exceder 10 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;
    }
}
