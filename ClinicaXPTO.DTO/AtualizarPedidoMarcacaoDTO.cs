using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.DTO
{
    public class AtualizarPedidoMarcacaoDTO
    {
        public DateTime? InicioIntervalo { get; set; }
        public DateTime? FimIntervalo { get; set; }
        public string Observacoes { get; set; }
        public string Estado { get; set; }
        public List<AtualizarItemPedidoDTO> Itens { get; set; }
    }
}
