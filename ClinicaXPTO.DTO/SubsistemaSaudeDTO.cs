using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaXPTO.DTO
{
    public class SubsistemaSaudeDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public string NomeComCodigo => 
            string.IsNullOrEmpty(Codigo) ? Nome : $"{Nome} ({Codigo})";

        public string StatusDescricao => Ativo ? "Ativo" : "Inativo";
        public bool TemCodigo => !string.IsNullOrEmpty(Codigo);
    }
}
