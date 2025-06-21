using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.DTO
{
    public class CriarPedidoMarcacaoDTO
    {
        public int UtenteId { get; set; }
        public DateTime? InicioIntervalo { get; set; }
        public DateTime? FimIntervalo { get; set; }
        public string Observacoes { get; set; }
        public List<ItemPedidoDTO> Itens { get; set; }
    }
}
