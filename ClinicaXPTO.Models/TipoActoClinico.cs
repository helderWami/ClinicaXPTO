using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaXPTO.Models
{
    [Table("TipoActoClinicos")]
    public class TipoActoClinico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; } = default!;

        [StringLength(10)]
        public string Codigo { get; set; } = default!; // Código interno

        public TimeSpan? DuracaoPadrao { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Preco { get; set; }

        [StringLength(500)]
        public string Observacoes { get; set; } = default!;

        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

        // Propriedades calculadas
        [NotMapped]
        public string DescricaoComPreco =>
            Preco.HasValue ? $"{Descricao} - €{Preco:F2}" : Descricao;

        [NotMapped]
        public string DuracaoFormatada =>
            DuracaoPadrao?.ToString(@"hh\:mm") ?? "N/A";
    }
}
