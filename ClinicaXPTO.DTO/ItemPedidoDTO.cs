using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaXPTO.DTO
{
    public class ItemPedidoDTO
    {
        public int Id { get; set; }
        public int PedidoMarcacaoId { get; set; }
        public int TipoActoClinicoId { get; set; }
        public string TipoActoClinicoDescricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "O subsistema de saúde é obrigatório")]
        public int SubsistemaSaudeId { get; set; }
        public string SubsistemaSaudeNome { get; set; } = string.Empty;
        public int? ProfissionalId { get; set; }
        public string ProfissionalNome { get; set; } = string.Empty;
        public TimeSpan? HorarioSolicitado { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public string DescricaoCompleta => 
            $"{TipoActoClinicoDescricao} - {SubsistemaSaudeNome}" +
            (!string.IsNullOrEmpty(ProfissionalNome) ? $" - {ProfissionalNome}" : "");
    }
}
