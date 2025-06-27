using System.ComponentModel;

namespace ClinicaXPTO.Models.Enuns
{
    public enum EstadoPedido
    {
        [Description("Pendente")]
        Pendente = 0,

        [Description("Agendado")]
        Agendado = 1,

        [Description("Realizado")]
        Realizado = 2
    }
}
