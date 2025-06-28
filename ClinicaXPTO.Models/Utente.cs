using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ClinicaXPTO.Models.Enuns;
using System.ComponentModel;

namespace ClinicaXPTO.Models
{
    [Table("Utentes")]
    public class Utente
    {
        [Key]
        public int Id { get; set; }

        // NULL para utentes anónimos, preenchido para registados
        public int? UtilizadorId { get; set; }
        [ForeignKey("UtilizadorId")]
        public Utilizador Utilizador { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroUtente { get; set; } = default!;

        [StringLength(500)]
        public string Fotografia { get; set; } = default!;

        [Required]
        [StringLength(200)]
        public string NomeCompleto { get; set; } = default!;

        [Required] 
        public DateTime DataNascimento { get; set; }

        [Required] 
        public Genero Genero { get; set; }

        [Phone]
        [StringLength(15)]
        public string Telemovel { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailContacto { get; set; } = default!;

        [StringLength(500)]
        public string Morada { get; set; } = default!;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public EstadoUtente EstadoUtente { get; set; }
        public bool Ativo { get; set; } = false;

        // Relacionamentos
        public ICollection<PedidoMarcacao> Pedidos { get; set; } = new List<PedidoMarcacao>();

        // Propriedades calculadas
        [NotMapped]
        public bool EhRegistado => UtilizadorId.HasValue;

        [NotMapped]
        public int Idade => DateTime.Today.Year - DataNascimento.Year -
                           (DateTime.Today.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);
    }
}
