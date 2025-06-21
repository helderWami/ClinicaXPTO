using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.DTO
{
    public class AtualizarItemPedidoDTO
    {
        public int TipoAtoClinicoId { get; set; }
        public int SubsistemaSaudeId { get; set; }
        public int? ProfissionalId { get; set; }
        public TimeSpan? HorarioSolicitado { get; set; }
        public string Observacoes { get; set; }
    }
}
