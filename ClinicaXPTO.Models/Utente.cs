using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicaXPTO.Models
{
    [Table("Utentes")]
    public class Utente
    {
            [Key]
            public int Id { get; set; }

            public int? UtilizadorId { get; set; }
            [ForeignKey("UtilizadorId")]
            public Utilizador Utilizador { get; set; }

            [Required]
            public string NumeroUtente { get; set; }

            public string Fotografia { get; set; }

            [Required]
            public string NomeCompleto { get; set; }

            public DateTime? DataNascimento { get; set; }

            public Genero? Genero { get; set; }

            public string Telemovel { get; set; }

            [EmailAddress]
            public string EmailContacto { get; set; }

            public string Morada { get; set; }
           
            public ICollection<PedidoMarcacao> Pedidos { get; set; }
    }
}
