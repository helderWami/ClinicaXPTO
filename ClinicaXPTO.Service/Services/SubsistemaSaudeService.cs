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
            if (subsistemaSaudeDto == null)
                throw new ArgumentNullException(nameof(subsistemaSaudeDto));

            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            // Verificar se o subsistema existe
            var subsistemaExistente = await _subsistemaSaudeRepository.GetByIdAsync(id);

            // Mapear apenas os campos que devem ser atualizados
            subsistemaExistente.Nome = subsistemaSaudeDto.Nome;
            subsistemaExistente.Descricao = subsistemaSaudeDto.Descricao;
            subsistemaExistente.Ativo = subsistemaSaudeDto.Ativo;

            return await _subsistemaSaudeRepository.UpdateAsync(subsistemaExistente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _subsistemaSaudeRepository.DeleteAsync(id);
        }
    }
}
