using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.DTO
{
    public class AtualizarUtilizadorDTO
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(100, ErrorMessage = "O email não pode exceder 100 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O perfil é obrigatório")]
        public Perfil Perfil { get; set; }

        public bool Ativo { get; set; } = true;

        [StringLength(255, MinimumLength = 8, ErrorMessage = "A nova senha deve ter entre 8 e 255 caracteres")]
        public string NovaSenha { get; set; } = string.Empty;

        [Compare("NovaSenha", ErrorMessage = "A confirmação da senha não confere")]
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }
}
