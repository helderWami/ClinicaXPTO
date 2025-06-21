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
        public string Descricao { get; set; }

        public TimeSpan? DuracaoPadrao { get; set; }

        public decimal? Preco { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
