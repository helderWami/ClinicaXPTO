using System.ComponentModel;

namespace ClinicaXPTO.Models.Enuns
{
    public enum Perfil
    {
        [Description("Utente Registado")]
        UtenteRegistado = 0,

        [Description("Administrativo")]
        Administrativo = 1,

        [Description("Administrador da Aplicação")]
        Administrador = 2
    }
}
