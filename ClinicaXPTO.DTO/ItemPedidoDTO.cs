using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.DTO
{
    public class ItemPedidoDTO
    {
        public int Id { get; set; }
        public int TipoActoClinicoId { get; set; }
        public int SubsistemaSaudeId { get; set; }
        public int? ProfissionalId { get; set; }
        public TimeSpan? HorarioSolicitado { get; set; }
        public string Observacoes { get; set; }
    }
}
