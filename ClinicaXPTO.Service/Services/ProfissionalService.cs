using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Models;
using Mapster;
using ClinicaXPTO.DTO;

namespace ClinicaXPTO.Service.Services
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository _profissionalRepository;

        public ProfissionalService(IProfissionalRepository profissionalRepository)
        {
            _profissionalRepository = profissionalRepository;
        }

        public async Task<IEnumerable<ProfissionalDTO>> GetAllAsync()
        {
            var professionals = await _profissionalRepository.GetAllAsync();
            return professionals.Adapt<IEnumerable<ProfissionalDTO>>();
        }

        public async Task<ProfissionalDTO> GetByIdAsync(int id)
        {
            var profissional = await _profissionalRepository.GetByIdAsync(id);
            return profissional.Adapt<ProfissionalDTO>();
        }

        public async Task<ProfissionalDTO> CreateAsync(CriarProfissionalDTO profissionalDto)
        {
            var profissional = profissionalDto.Adapt<Profissional>();
            var novoProfessional = await _profissionalRepository.AddAsync(profissional);
            
            return novoProfessional.Adapt<ProfissionalDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarProfissionalDTO profissionalDto)
        {
            if (profissionalDto == null)
                throw new ArgumentNullException(nameof(profissionalDto));

            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            // Verificar se o profissional existe
            var profissionalExistente = await _profissionalRepository.GetByIdAsync(id);

            // Mapear apenas os campos que devem ser atualizados
            profissionalExistente.NomeCompleto = profissionalDto.NomeCompleto;
            profissionalExistente.Especialidade = profissionalDto.Especialidade;
            profissionalExistente.NumeroOrdem = profissionalDto.NumeroOrdem;
            profissionalExistente.Ativo = profissionalDto.Ativo;

            return await _profissionalRepository.UpdateAsync(profissionalExistente);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _profissionalRepository.DeleteAsync(id);
        }
    }
}
