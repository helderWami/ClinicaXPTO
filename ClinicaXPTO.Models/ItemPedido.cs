using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ClinicaXPTO.Models
{
    [Table("ItemPedidos")]
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PedidoMarcacaoId { get; set; }
        [ForeignKey("PedidoMarcacaoId")]
        public PedidoMarcacao PedidoMarcacao { get; set; } = default!;

        [Required]
        public int TipoActoClinicoId { get; set; }
        [ForeignKey("TipoActoClinicoId")]
        public TipoActoClinico TipoActoClinico { get; set; } = default!;

        [Required]
        public int SubsistemaSaudeId { get; set; }
        [ForeignKey("SubsistemaSaudeId")]
        public SubsistemaSaude SubsistemaSaude { get; set; } = default!;

        public int? ProfissionalId { get; set; }
        [ForeignKey("ProfissionalId")]
        public Profissional Profissional { get; set; } = default!;

        public TimeSpan? HorarioSolicitado { get; set; }

        [StringLength(500)]
        public string Observacoes { get; set; } = string.Empty;

        // Propriedades calculadas
        [NotMapped]
        public string DescricaoCompleta =>
            $"{TipoActoClinico?.Descricao} - {SubsistemaSaude?.Nome}" +
            (Profissional != null ? $" - {Profissional.NomeCompleto}" : "");
    }
}
