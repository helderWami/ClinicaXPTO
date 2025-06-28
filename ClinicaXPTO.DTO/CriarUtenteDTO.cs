using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.DTO
{
    public class CriarUtenteDTO
    {
        public int? UtilizadorId { get; set; }

        [Required(ErrorMessage = "O número do utente é obrigatório")]
        [StringLength(20, ErrorMessage = "O número do utente não pode exceder 20 caracteres")]
        public string NumeroUtente { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "O caminho da fotografia não pode exceder 500 caracteres")]
        public string Fotografia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 200 caracteres")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O género é obrigatório")]
        public Genero Genero { get; set; }

        [Phone(ErrorMessage = "Formato de telemóvel inválido")]
        [StringLength(15, ErrorMessage = "O telemóvel não pode exceder 15 caracteres")]
        public string Telemovel { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email de contacto é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(100, ErrorMessage = "O email não pode exceder 100 caracteres")]
        public string EmailContacto { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A morada não pode exceder 500 caracteres")]
        public string Morada { get; set; } = string.Empty;

        public bool Ativo { get; set; } = false;
    }
}
