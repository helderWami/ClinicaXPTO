using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using ClinicaXPTO.Models;
using ClinicaXPTO.DTO;
using Mapster;
using ClinicaXPTO.Models.Enuns;

namespace ClinicaXPTO.Service.Services
{
    /// <summary>
    /// SERVICE DE UTENTES - LÓGICA DE NEGÓCIO
    /// ======================================
    /// Esta classe implementa a lógica de negócio para utentes.
    /// 
    /// CONVERSÃO DE DADOS:
    /// - Atualmente usa Mapster (.Adapt<>)
    /// - Pode ser feita manualmente sem mappers
    /// - Exemplo de conversão manual comentado abaixo
    /// </summary>
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
            
            // ATUAL: Usando Mapster
            return utentes.Adapt<IEnumerable<UtenteDTO>>();
            
            // ALTERNATIVA: Conversão manual (sem mappers)
            /*
            return utentes.Select(utente => new UtenteDTO
            {
                Id = utente.Id,
                UtilizadorId = utente.UtilizadorId,
                NumeroUtente = utente.NumeroUtente,
                Fotografia = utente.Fotografia,
                NomeCompleto = utente.NomeCompleto,
                DataNascimento = utente.DataNascimento,
                Genero = utente.Genero,
                Telemovel = utente.Telemovel,
                EmailContacto = utente.EmailContacto,
                Morada = utente.Morada,
                DataCriacao = utente.DataCriacao,
                Ativo = utente.Ativo
            });
            */
        }

        public async Task<UtenteDTO> GetByIdAsync(int id)
        {
            var utente = await _utenteRepository.GetByIdAsync(id);
            
            // ATUAL: Usando Mapster
            return utente.Adapt<UtenteDTO>();
            
            // ALTERNATIVA: Conversão manual
            /*
            if (utente == null) return null;
            
            return new UtenteDTO
            {
                Id = utente.Id,
                UtilizadorId = utente.UtilizadorId,
                NumeroUtente = utente.NumeroUtente,
                Fotografia = utente.Fotografia,
                NomeCompleto = utente.NomeCompleto,
                DataNascimento = utente.DataNascimento,
                Genero = utente.Genero,
                Telemovel = utente.Telemovel,
                EmailContacto = utente.EmailContacto,
                Morada = utente.Morada,
                DataCriacao = utente.DataCriacao,
                Ativo = utente.Ativo
            };
            */
        }

        public async Task<UtenteDTO> CreateAsync(CriarUtenteDTO utenteDto)
        {
            // ATUAL: Usando Mapster
            var utente = utenteDto.Adapt<Utente>();
            
            // ALTERNATIVA: Conversão manual
            /*
            var utente = new Utente
            {
                NumeroUtente = utenteDto.NumeroUtente,
                NomeCompleto = utenteDto.NomeCompleto,
                DataNascimento = utenteDto.DataNascimento,
                Genero = utenteDto.Genero,
                Telemovel = utenteDto.Telemovel,
                EmailContacto = utenteDto.EmailContacto,
                Morada = utenteDto.Morada,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };
            */
            
            var novoUtente = await _utenteRepository.AddAsync(utente);
            return novoUtente.Adapt<UtenteDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarUtenteDTO utenteDto)
        {
            if (utenteDto == null)
                throw new ArgumentNullException(nameof(utenteDto));

            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            // Verificar se o utente existe
            var utenteExistente = await _utenteRepository.GetByIdAsync(id);

            // Verificar se o email já existe para outro utente
            if (!string.IsNullOrWhiteSpace(utenteDto.EmailContacto))
            {
                var emailExiste = await _utenteRepository.ExisteEmailAsync(utenteDto.EmailContacto);
                if (emailExiste)
                {
                    var utenteComEmail = await _utenteRepository.ObterPorEmailAsync(utenteDto.EmailContacto);
                    if (utenteComEmail.Id != id)
                    {
                        throw new InvalidOperationException("Este email já está sendo usado por outro utente.");
                    }
                }
            }

            // Mapear apenas os campos que devem ser atualizados
            utenteExistente.NomeCompleto = utenteDto.NomeCompleto;
            utenteExistente.EmailContacto = utenteDto.EmailContacto;
            utenteExistente.Telemovel = utenteDto.Telemovel;
            utenteExistente.DataNascimento = utenteDto.DataNascimento;
            utenteExistente.Morada = utenteDto.Morada;
            utenteExistente.Genero = utenteDto.Genero;
            utenteExistente.Ativo = utenteDto.Ativo;
            
            if (!string.IsNullOrWhiteSpace(utenteDto.Fotografia))
                utenteExistente.Fotografia = utenteDto.Fotografia;

            return await _utenteRepository.UpdateAsync(utenteExistente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _utenteRepository.DeleteAsync(id);
        }

        public async Task<UtenteDTO> CriarUtenteAnonimoAsync(UtenteDTO utente)
        {
            var novoUtente = utente.Adapt<Utente>();
            novoUtente.EstadoUtente = EstadoUtente.UtenteAnonimo;
            var utenteCriado = await _utenteRepository.AddAsync(novoUtente);
            return utenteCriado.Adapt<UtenteDTO>();

        }

        public async Task<UtenteDTO> ConverterParaRegistadoAsync(int utenteId, int utilizadorId)
        {
            var utente = await _utenteRepository.GetByIdAsync(utenteId);
            if (utente == null)
            {
                throw new KeyNotFoundException($"Utente com ID {utenteId} não encontrado.");
            }

            await _utenteRepository.AtualizarEstadoUtenteAsync(utenteId, EstadoUtente.UtenteRegistado);
            utente.UtilizadorId = utilizadorId;
            return utente.Adapt<UtenteDTO>();
        }

        public Task<bool> ValidarNumeroUtenteDisponivelAsync(string numeroUtente)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidarEmailDisponivelAsync(string email)
        {
            var existe = await _utenteRepository.ExisteEmailAsync(email);
            return !existe;
        }

        public async Task<IEnumerable<UtenteDTO>> PesquisarUtentesAsync(string termo)
        {
            var utentes = await _utenteRepository.PesquisarPorNomeAsync(termo);
            return utentes.Adapt<IEnumerable<UtenteDTO>>();
        }

        public async Task<bool> AtualizarFotografiaAsync(int utenteId, string caminhoFotografia)
        {
            if (utenteId <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(utenteId));
            if (string.IsNullOrWhiteSpace(caminhoFotografia))
                throw new ArgumentException("O caminho da fotografia não pode ser vazio.", nameof(caminhoFotografia));

            var utente = await _utenteRepository.GetByIdAsync(utenteId);
            if (utente == null)
                throw new KeyNotFoundException($"Utente com ID {utenteId} não encontrado.");

            utente.Fotografia = caminhoFotografia;
            return await _utenteRepository.UpdateAsync(utente);
        }
    }
}
