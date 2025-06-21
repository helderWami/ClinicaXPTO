using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public Utente Utente { get; set; }

        [Required]
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        public DateTime? InicioIntervalo { get; set; }
        public DateTime? FimIntervalo { get; set; }

        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendente;

        public string Observacoes { get; set; }

        // lista de atos clinicos que compõem o pedido
        public ICollection<ItemPedido> Itens { get; set; } 
    }
}
