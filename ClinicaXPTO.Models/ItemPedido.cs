using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.Models
{
    [Table("ItemPedidos")]
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PedidoMarcacaoId { get; set; }
        [ForeignKey("PedidoMarcacaoId")]
        public PedidoMarcacao PedidoMarcacao { get; set; }

        [Required]
        public int TipoActoClinicoId { get; set; }
        [ForeignKey("TipoActoClinicoId")]
        public TipoActoClinico TipoActoClinico { get; set; }

        [Required]
        public int SubsistemaSaudeId { get; set; }
        [ForeignKey("SubsistemaSaudeId")]
        public SubsistemaSaude SubsistemaSaude { get; set; }

        public int? ProfissionalId { get; set; }
        [ForeignKey("ProfissionalId")]
        public Profissional Profissional { get; set; }

        public TimeSpan? HorarioSolicitado { get; set; }

        public string Observacoes { get; set; }
    }
}
