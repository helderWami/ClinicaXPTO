using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using Mapster;

namespace ClinicaXPTO.Service.Services
{
    public class TipoActoClinicoService : ITipoActoClinicoService
    {
        private readonly ITipoActoClinicoRepository _tipoActoClinicoRepository;

        public TipoActoClinicoService(ITipoActoClinicoRepository tipoActoClinicoRepository)
        {
            _tipoActoClinicoRepository = tipoActoClinicoRepository;
        }

        public async Task<IEnumerable<TipoActoClinicoDTO>> GetAllAsync()
        {
            var tipoActosClinicos = await _tipoActoClinicoRepository.GetAllAsync();
            return tipoActosClinicos.Adapt<IEnumerable<TipoActoClinicoDTO>>();
        }

        public async Task<TipoActoClinicoDTO> GetByIdAsync(int id)
        {
            var tipoActoClinico = await _tipoActoClinicoRepository.GetByIdAsync(id);
            return tipoActoClinico.Adapt<TipoActoClinicoDTO>();
        }

        public async Task<TipoActoClinicoDTO> CreateAsync(CriarTipoActoClinico tipoActoClinicoDto)
        {
            var tipoActoClinico = tipoActoClinicoDto.Adapt<TipoActoClinico>();
            var novoTipoActoClinico = await _tipoActoClinicoRepository.AddAsync(tipoActoClinico);
            return novoTipoActoClinico.Adapt<TipoActoClinicoDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarTipoActoClinicoDTO tipoActoClinicoDto)
        {
            var tipoActoClinico = tipoActoClinicoDto.Adapt<TipoActoClinico>();
            return await _tipoActoClinicoRepository.UpdateAsync(tipoActoClinico);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _tipoActoClinicoRepository.DeleteAsync(id);
        }
    }
}
