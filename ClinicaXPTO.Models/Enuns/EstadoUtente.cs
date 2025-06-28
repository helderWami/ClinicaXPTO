using System.ComponentModel;

namespace ClinicaXPTO.Models.Enuns
{
    public enum EstadoUtente
    {
        [Description("Utente Registado")]
        UtenteRegistado = 0,

        [Description("Utente Anónimo")]
        UtenteAnonimo = 1, // Anónimo, não registado
    }
}
