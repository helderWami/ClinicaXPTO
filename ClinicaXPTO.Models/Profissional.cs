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
        [StringLength(200)]
        public string NomeCompleto { get; set; } = default!;

        [StringLength(100)]
        public string Especialidade { get; set; } = string.Empty;

        [StringLength(50)]
        public string NumeroOrdem { get; set; } = string.Empty; // Número da ordem profissional

        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

        // Propriedades calculadas
        [NotMapped]
        public string NomeComEspecialidade =>
            string.IsNullOrEmpty(Especialidade) ? NomeCompleto : $"{NomeCompleto} ({Especialidade})";
    }
}
