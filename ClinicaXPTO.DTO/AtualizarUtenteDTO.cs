using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.DTO
{
   public class AtualizarUtenteDTO
    {
        public string NomeCompleto { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Genero { get; set; }
        public string Telemovel { get; set; }
        public string EmailContacto { get; set; }
        public string Morada { get; set; }
    }
}
