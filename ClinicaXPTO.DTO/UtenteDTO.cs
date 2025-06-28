using System;
using System.ComponentModel.DataAnnotations;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.DTO
{
    public class UtenteDTO
    {
        public int Id { get; set; }
        public int? UtilizadorId { get; set; }
        public string NumeroUtente { get; set; } = string.Empty;
        public string Fotografia { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public Genero Genero { get; set; }
        public string Telemovel { get; set; } = string.Empty;
        public string EmailContacto { get; set; } = string.Empty;
        public string Morada { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

        public bool EhRegistado => UtilizadorId.HasValue;
        
        public int Idade => DateTime.Today.Year - DataNascimento.Year -
                           (DateTime.Today.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);

        public string GeneroDescricao => Genero == Genero.Masculino ? "Masculino" : "Feminino";

        public string StatusDescricao => Ativo ? "Ativo" : "Inativo";

        public string TipoUtente => EhRegistado ? "Registado" : "Anónimo";

        public bool TemFotografia => !string.IsNullOrEmpty(Fotografia);
    }
}
