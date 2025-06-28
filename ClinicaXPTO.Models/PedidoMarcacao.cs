using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Models
{
    [Table("PedidoMarcacoes")]
    public class PedidoMarcacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UtenteId { get; set; }
        [ForeignKey("UtenteId")]
        public Utente Utente { get; set; } = default!;

        [Required]
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        public DateTime? InicioIntervalo { get; set; }
        public DateTime? FimIntervalo { get; set; }

        public DateTime? DataHoraAgendada { get; set; }

        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendente;

        [StringLength(1000)]
        public string Observacoes { get; set; } = string.Empty;


        public int? AgendadoPorId { get; set; }
        [ForeignKey("AgendadoPorId")]
        public Utilizador AgendadoPor { get; set; } = default!;

        public DateTime? DataAgendamento { get; set; }

        public int? RealizadoPorId { get; set; }
        [ForeignKey("RealizadoPorId")]
        public Utilizador RealizadoPor { get; set; } = default!;

        public DateTime? DataRealizacao { get; set; }

        // Lista de atos clínicos que compõem o pedido
        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        // Propriedades calculadas
        [NotMapped]
        public string EstadoDescricao => Estado.ToString();

        [NotMapped]
        public bool PodeSerAlterado => Estado == EstadoPedido.Pendente;

        [NotMapped]
        public bool PodeSerAgendado => Estado == EstadoPedido.Pendente;

        [NotMapped]
        public bool PodeSerRealizado => Estado == EstadoPedido.Agendado;
    }
}
