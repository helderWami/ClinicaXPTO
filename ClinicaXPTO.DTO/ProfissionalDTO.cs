using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaXPTO.DTO
{
    public class ProfissionalDTO
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public string NumeroOrdem { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string NomeComEspecialidade => 
            string.IsNullOrEmpty(Especialidade) ? NomeCompleto : $"{NomeCompleto} ({Especialidade})";

        public bool TemNumeroOrdem => !string.IsNullOrEmpty(NumeroOrdem);
        public string StatusDescricao => Ativo ? "Ativo" : "Inativo";
    }
}
