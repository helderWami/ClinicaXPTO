using System.Threading.Tasks;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string destinatario, string assunto, string mensagem);
    }
} 