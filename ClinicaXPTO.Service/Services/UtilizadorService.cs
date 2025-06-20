using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using Mapster;

namespace ClinicaXPTO.Service.Services
{
    public class UtilizadorService : IUtilizadorService
    {
        private readonly IUtilizadorRepository _utilizadorRepository;

        public UtilizadorService(IUtilizadorRepository utilizadorRepository)
        {
            _utilizadorRepository = utilizadorRepository;
        }

        public async Task<IEnumerable<UtilizadorDTO>> GetAllAsync()
        {
            var utilizadores = await _utilizadorRepository.GetAllAsync();
            return utilizadores.Adapt<IEnumerable<UtilizadorDTO>>();
        }

        public async Task<UtilizadorDTO> GetByIdAsync(int id)
        {
            var utilizador = await _utilizadorRepository.GetByIdAsync(id);
            return utilizador.Adapt<UtilizadorDTO>();
        }

        public async Task<UtilizadorDTO> CreateAsync(CriarUtilizadorDTO utilizadorDto)
        {
            var utilizador = utilizadorDto.Adapt<Utilizador>();
            var novoUtilizador = await _utilizadorRepository.AddAsync(utilizador);
            return novoUtilizador.Adapt<UtilizadorDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarUtilizadorDTO utilizadorDto)
        {
            var utilizador = utilizadorDto.Adapt<Utilizador>();
            utilizador.Id = id; // Ensure the ID is set for the update
            return await _utilizadorRepository.UpdateAsync(utilizador);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _utilizadorRepository.DeleteAsync(id);
        }
    }
}
