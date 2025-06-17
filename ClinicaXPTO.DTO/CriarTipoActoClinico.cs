using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.DTO
{
    public class CriarTipoActoClinico
    {
        public string Descricao { get; set; }
        public TimeSpan? DuracaoPadrao { get; set; }
        public decimal? Preco { get; set; }
    }
}
