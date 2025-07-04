﻿using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;
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
            if (utilizadorDto == null)
                throw new ArgumentNullException(nameof(utilizadorDto));

            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            // Verificar se o utilizador existe
            var utilizadorExistente = await _utilizadorRepository.GetByIdAsync(id);
            
            // Verificar se o email já existe para outro utilizador
            if (await _utilizadorRepository.ExisteEmailAsync(utilizadorDto.Email))
            {
                var utilizadorComEmail = await _utilizadorRepository.ObterPorEmailAsync(utilizadorDto.Email);
                if (utilizadorComEmail.Id != id)
                {
                    throw new InvalidOperationException("Este email já está sendo usado por outro utilizador.");
                }
            }

            // Mapear apenas os campos que devem ser atualizados
            utilizadorExistente.Email = utilizadorDto.Email;
            utilizadorExistente.Perfil = utilizadorDto.Perfil;
            utilizadorExistente.Ativo = utilizadorDto.Ativo;
            
            // Atualizar senha apenas se uma nova foi fornecida
            if (!string.IsNullOrWhiteSpace(utilizadorDto.NovaSenha))
            {
                // Aqui você pode adicionar hash da senha se necessário
                utilizadorExistente.Senha = utilizadorDto.NovaSenha;
            }

            return await _utilizadorRepository.UpdateAsync(utilizadorExistente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _utilizadorRepository.DeleteAsync(id);
        }

        public async Task<UtilizadorDTO> AutenticarAsync(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                return null;

            try
            {
                // Buscar utilizador por email
                var utilizador = await _utilizadorRepository.ObterPorEmailAsync(email);
                
                if (utilizador == null || !utilizador.Ativo)
                    return null;

                // Verificar se a senha está correta (comparação simples por enquanto)
                if (utilizador.Senha != senha)
                    return null;

                // Retornar DTO do utilizador
                return utilizador.Adapt<UtilizadorDTO>();
            }
            catch (Exception)
            {
                // Em caso de erro, não expor informações sensíveis
                return null;
            }
        }

        public async Task<UtilizadorDTO> CriarUtilizadorAsync(string email, string senha, Perfil perfil)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Email e senha são obrigatórios");

            // Verificar se o email já existe
            if (await _utilizadorRepository.ExisteEmailAsync(email))
                throw new InvalidOperationException("Este email já está registado");

            // Criar novo utilizador
            var novoUtilizador = new Utilizador
            {
                Email = email.ToLower().Trim(),
                Senha = senha, // Por enquanto sem hash
                Perfil = perfil,
                DataCriacao = DateTime.UtcNow,
                Ativo = true
            };

            // Salvar no banco de dados
            var utilizadorSalvo = await _utilizadorRepository.AddAsync(novoUtilizador);

            // Retornar DTO
            return utilizadorSalvo.Adapt<UtilizadorDTO>();
        }

        public async Task<bool> ValidarCredenciaisAsync(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                return false;

            try
            {
                // Buscar utilizador por email
                var utilizador = await _utilizadorRepository.ObterPorEmailAsync(email);
                
                if (utilizador == null || !utilizador.Ativo)
                    return false;

                // Verificar se a senha está correta (comparação simples por enquanto)
                return utilizador.Senha == senha;
            }
            catch (Exception)
            {
                // Em caso de erro, retornar false
                return false;
            }
        }
    }
}
