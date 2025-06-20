using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.Models;
using ClinicaXPTO.DTO;
using Mapster;

namespace ClinicaXPTO.Service.Services
{
    public class UtenteService : IUtenteService
    {
        private readonly IUtenteRepository _utenteRepository;

        public UtenteService(IUtenteRepository utenteRepository)
        {
            _utenteRepository = utenteRepository;
        }

        public async Task<IEnumerable<UtenteDTO>> GetAllAsync()
        {
            var utentes = await _utenteRepository.GetAllAsync();
            return utentes.Adapt<IEnumerable<UtenteDTO>>();
        }

        public async Task<UtenteDTO> GetByIdAsync(int id)
        {
            var utente = await _utenteRepository.GetByIdAsync(id);
            return utente.Adapt<UtenteDTO>();
        }

        public async Task<UtenteDTO> CreateAsync(CriarUtenteDTO utenteDto)
        {
            var utente = utenteDto.Adapt<Utente>();
            var novoUtente = await _utenteRepository.AddAsync(utente);
            return novoUtente.Adapt<UtenteDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarUtenteDTO utenteDto)
        {
            var utente = utenteDto.Adapt<Utente>();
            utente.Id = id; // Ensure the ID is set for the update
            return await _utenteRepository.UpdateAsync(utente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _utenteRepository.DeleteAsync(id);
        }
    }
}
