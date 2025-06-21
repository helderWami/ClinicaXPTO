using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaXPTO.Models
{
    [Table("Profissionais")]
    public class Profissional
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        public string Especialidade { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
