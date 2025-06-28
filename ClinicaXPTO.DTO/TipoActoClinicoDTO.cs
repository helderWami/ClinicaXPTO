using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaXPTO.DTO
{
    public class TipoActoClinicoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public TimeSpan? DuracaoPadrao { get; set; }
        public decimal? Preco { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
