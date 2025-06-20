using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using Mapster;

namespace ClinicaXPTO.Service.Services
{
    public class SubsistemaSaudeService : ISubsistemaSaudeService
    {
        private readonly ISubsistemaSaudeRepository _subsistemaSaudeRepository;

        public SubsistemaSaudeService(ISubsistemaSaudeRepository subsistemaSaudeRepository)
        {
            _subsistemaSaudeRepository = subsistemaSaudeRepository;
        }

        public async Task<IEnumerable<SubsistemaSaudeDTO>> GetAllAsync()
        {
            var subsistemasSaude = await _subsistemaSaudeRepository.GetAllAsync();
            return subsistemasSaude.Adapt<IEnumerable<SubsistemaSaudeDTO>>();
        }

        public async Task<SubsistemaSaudeDTO> GetByIdAsync(int id)
        {
            var subsistemaSaude = await _subsistemaSaudeRepository.GetByIdAsync(id);
            return subsistemaSaude.Adapt<SubsistemaSaudeDTO>();
        }

        public async Task<SubsistemaSaudeDTO> CreateAsync(CriarSubsistemaSaudeDTO subsistemaSaudeDto)
        {
            var subsistemaSaude = subsistemaSaudeDto.Adapt<SubsistemaSaude>();
            var novoSubsistemaSaude = await _subsistemaSaudeRepository.AddAsync(subsistemaSaude);
            return novoSubsistemaSaude.Adapt<SubsistemaSaudeDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarSubsistemaSaudeDTO subsistemaSaudeDto)
        {
            var subsistemaSaude = subsistemaSaudeDto.Adapt<SubsistemaSaude>();
            return await _subsistemaSaudeRepository.UpdateAsync(subsistemaSaude);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _subsistemaSaudeRepository.DeleteAsync(id);
        }
    }
}
