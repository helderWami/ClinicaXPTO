using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.DTO
{
    public class PedidoMarcacaoDTO
    {
        public int Id { get; set; }
        public int UtenteId { get; set; }
        public string UtenteNome { get; set; } = string.Empty;
        public string UtenteNumero { get; set; } = string.Empty;
        public DateTime DataPedido { get; set; }
        public DateTime? InicioIntervalo { get; set; }
        public DateTime? FimIntervalo { get; set; }
        public DateTime? DataHoraAgendada { get; set; }
        public EstadoPedido Estado { get; set; }
        public string EstadoDescricao { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public int? AgendadoPorId { get; set; }
        public string AgendadoPorNome { get; set; } = string.Empty;
        public DateTime? DataAgendamento { get; set; }
        public int? RealizadoPorId { get; set; }
        public string RealizadoPorNome { get; set; } = string.Empty;
        public DateTime? DataRealizacao { get; set; }
        public List<ItemPedidoDTO> Itens { get; set; } = new List<ItemPedidoDTO>();

        public bool PodeSerAlterado { get; set; }
        public bool PodeSerAgendado { get; set; }
        public bool PodeSerRealizado { get; set; }
        public string ResumoActos =>
            Itens.Count > 0
                ? string.Join(", ", Itens.Select(i => i.TipoActoClinicoDescricao).Take(3)) +
                  (Itens.Count > 3 ? $" e mais {Itens.Count - 3}..." : "")
                : "Nenhum ato clínico especificado";
        public int TotalActos => Itens?.Count ?? 0;
    }
}