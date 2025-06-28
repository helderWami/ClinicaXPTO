using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ClinicaXPTO.DTO
{
    public class AtualizarProfissionalDTO
    {
        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 200 caracteres")]
        public string NomeCompleto { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "A especialidade não pode exceder 100 caracteres")]
        public string Especialidade { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "O número da ordem não pode exceder 50 caracteres")]
        public string NumeroOrdem { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

    }
}
