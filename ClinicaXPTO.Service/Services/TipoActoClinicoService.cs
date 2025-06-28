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
            if (tipoActoClinicoDto == null)
                throw new ArgumentNullException(nameof(tipoActoClinicoDto));

            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            // Verificar se o tipo de acto clínico existe
            var tipoActoExistente = await _tipoActoClinicoRepository.GetByIdAsync(id);

            // Mapear apenas os campos que devem ser atualizados
            tipoActoExistente.Descricao = tipoActoClinicoDto.Descricao;
            tipoActoExistente.Codigo = tipoActoClinicoDto.Codigo;
            tipoActoExistente.DuracaoPadrao = tipoActoClinicoDto.DuracaoPadrao;
            tipoActoExistente.Preco = tipoActoClinicoDto.Preco;
            tipoActoExistente.Observacoes = tipoActoClinicoDto.Observacoes;
            tipoActoExistente.Ativo = tipoActoClinicoDto.Ativo;

            return await _tipoActoClinicoRepository.UpdateAsync(tipoActoExistente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _tipoActoClinicoRepository.DeleteAsync(id);
        }
    }
}
