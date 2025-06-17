using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.DTO
{
    public class UtenteDTO
    {
        public int Id { get; set; }
        public string NumeroUtente { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Genero { get; set; }
        public string Telemovel { get; set; }
        public string EmailContacto { get; set; }
        public string Morada { get; set; }
    }
}
