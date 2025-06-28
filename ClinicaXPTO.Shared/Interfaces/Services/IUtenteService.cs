using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Services
{
    public interface IUtenteService
    {
        Task<IEnumerable<UtenteDTO>> GetAllAsync();
        Task<UtenteDTO> GetByIdAsync(int id);
        Task<UtenteDTO> CreateAsync(CriarUtenteDTO utente);
        Task<bool> UpdateAsync(int id, AtualizarUtenteDTO utente);
        Task<bool> DeleteAsync(int id);

        // Use Case: Utente Anônimo criar pedido
        Task<UtenteDTO> CriarUtenteAnonimoAsync(UtenteDTO utente);

        // Use Case: Converter utente anônimo para registado após primeira marcação
        Task<UtenteDTO> ConverterParaRegistadoAsync(int utenteId, int utilizadorId);

        // Validações necessárias
        Task<bool> ValidarNumeroUtenteDisponivelAsync(string numeroUtente);
        Task<bool> ValidarEmailDisponivelAsync(string email);

        // Use Case: Pesquisar utentes (para administrativos)
        Task<IEnumerable<UtenteDTO>> PesquisarUtentesAsync(string termo);
    }
}
