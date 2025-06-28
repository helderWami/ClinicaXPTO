using ClinicaXPTO.DAL.AppDbContext;
using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicaXPTO.DAL.Repositories
{
    public class PedidoMarcacaoRepository : IPedidoMarcacaoRepository
    {
        private readonly ClinicaXPTODbContext _context;

        public PedidoMarcacaoRepository(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoMarcacao>> GetAllAsync()
        {
            return await _context.PedidoMarcacoes.ToListAsync();
        }

        public async Task<PedidoMarcacao> GetByIdAsync(int id)
        {
            var pedidoMarcacao = await _context.PedidoMarcacoes
                .Include(p => p.Utente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedidoMarcacao == null)
            {
                throw new KeyNotFoundException($"PedidoMarcacao com ID {id} nao encontrado.");
            }

            return pedidoMarcacao;
        }

        public async Task<PedidoMarcacao> AddAsync(PedidoMarcacao pedidoMarcacao)
        {
            _context.PedidoMarcacoes.Add(pedidoMarcacao);
            await _context.SaveChangesAsync();
            return pedidoMarcacao;
        }

        public async Task<bool> UpdateAsync(PedidoMarcacao pedidoMarcacao)
        {
            if (pedidoMarcacao == null)
                throw new ArgumentNullException(nameof(pedidoMarcacao));

            var existingPedido = await _context.PedidoMarcacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == pedidoMarcacao.Id);

            if (existingPedido == null)
                throw new KeyNotFoundException($"PedidoMarcacao com ID {pedidoMarcacao.Id} não encontrado para atualização.");

            try
            {
                _context.Entry(pedidoMarcacao).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("A entidade foi modificada por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar pedido de marcação: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pedidoMarcacao = await _context.PedidoMarcacoes.FirstOrDefaultAsync(p => p.Id == id);
            if (pedidoMarcacao == null)
            {
                throw new KeyNotFoundException($"PedidoMarcacao com ID {id} nao encontrado.");
            }
            _context.PedidoMarcacoes.Remove(pedidoMarcacao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AgendarPedidoAsync(AgendarPedidoDto agendarPedidoDto)
        {
            if (agendarPedidoDto == null)
                throw new ArgumentNullException(nameof(agendarPedidoDto));

            var pedidoMarcacao = await _context.PedidoMarcacoes
                .FirstOrDefaultAsync(p => p.Id == agendarPedidoDto.PedidoId);

            if (pedidoMarcacao == null)
                throw new KeyNotFoundException($"PedidoMarcacao com ID {agendarPedidoDto.PedidoId} não encontrado.");

            if (pedidoMarcacao.Estado == EstadoPedido.Agendado)
                throw new InvalidOperationException("Este pedido já está agendado.");

            if (agendarPedidoDto.DataHoraAgendada <= DateTime.Now)
                throw new ArgumentException("A data/hora de agendamento deve ser futura.");

            try
            {
                pedidoMarcacao.DataHoraAgendada = agendarPedidoDto.DataHoraAgendada;
                pedidoMarcacao.Estado = EstadoPedido.Agendado;
                pedidoMarcacao.AgendadoPorId = agendarPedidoDto.AdministrativoId;
                pedidoMarcacao.DataAgendamento = DateTime.UtcNow;

                _context.Entry(pedidoMarcacao).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("O pedido foi modificado por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao agendar pedido: {ex.Message}", ex);
            }
        }

        public async Task<bool> RealizarPedidoAsync(int pedidoId, int utilizadorId, DateTime dataRealizacao)
        {
            if (pedidoId <= 0)
                throw new ArgumentException("ID do pedido deve ser maior que zero.", nameof(pedidoId));

            if (utilizadorId <= 0)
                throw new ArgumentException("ID do utilizador deve ser maior que zero.", nameof(utilizadorId));

            if (dataRealizacao > DateTime.Now)
                throw new ArgumentException("A data de realização não pode ser futura.", nameof(dataRealizacao));

            var pedidoMarcacao = await _context.PedidoMarcacoes
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedidoMarcacao == null)
                throw new KeyNotFoundException($"PedidoMarcacao com ID {pedidoId} não encontrado.");

            if (pedidoMarcacao.Estado == EstadoPedido.Realizado)
                throw new InvalidOperationException("Este pedido já foi realizado.");

            if (pedidoMarcacao.Estado != EstadoPedido.Agendado)
                throw new InvalidOperationException("Apenas pedidos agendados podem ser marcados como realizados.");

            try
            {
                pedidoMarcacao.Estado = EstadoPedido.Realizado;
                pedidoMarcacao.RealizadoPorId = utilizadorId;
                pedidoMarcacao.DataRealizacao = dataRealizacao;

                _context.Entry(pedidoMarcacao).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("O pedido foi modificado por outro processo. Recarregue os dados e tente novamente.");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException($"Erro ao marcar pedido como realizado: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<PedidoMarcacao>> ObterPorUtenteAsync(int utenteId)
        {
            return await _context.PedidoMarcacoes
                .Include(p => p.Utente)
                .Where(p => p.UtenteId == utenteId)
                .OrderBy(p => p.DataHoraAgendada)
                .ToListAsync();
        }

        public async Task<IEnumerable<PedidoMarcacao>> ObterPorEstadoAsync(EstadoPedido estado)
        {
            return await _context.PedidoMarcacoes
                .Where(p => p.Estado == estado)
                .ToListAsync();
        }

        public async Task<PedidoMarcacao> ObterComItensAsync(int pedidoId)
        {
            var pedidoMarcacao = await _context.PedidoMarcacoes
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedidoMarcacao == null)
                throw new KeyNotFoundException($"PedidoMarcacao com ID {pedidoId} nao encontrado.");

            return pedidoMarcacao;
        }

        public async Task<IEnumerable<PedidoMarcacao>> PesquisarAsync(string numeroUtente, string nomeUtente, EstadoPedido? estado)
        {
            return await _context.PedidoMarcacoes
                .Include(p => p.Utente)
                .Where(p => (string.IsNullOrEmpty(numeroUtente) || p.Utente.NumeroUtente.Contains(numeroUtente)) &&
                             (string.IsNullOrEmpty(nomeUtente) || p.Utente.NomeCompleto.Contains(nomeUtente)) &&
                             (!estado.HasValue || p.Estado == estado.Value))
                .ToListAsync();
        }
    }
}
