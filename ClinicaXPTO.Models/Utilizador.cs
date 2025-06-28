using ClinicaXPTO.Models.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaXPTO.Models
{
    [Table("Utilizadores")]
    public  class Utilizador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(255)]
        public string Senha { get; set; } = default!;

        [Required]
        public Perfil Perfil { get; set; } 

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

    }
}
