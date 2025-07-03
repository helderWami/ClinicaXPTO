using ClinicaXPTO.DTO;
using ClinicaXPTO.Shared.Interfaces.Repositories;
using ClinicaXPTO.Shared.Interfaces.Services;
using Mapster;
using ClinicaXPTO.Models;
using ClinicaXPTO.Models.Enuns;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ClinicaXPTO.Service.Services
{
    public class PedidoMarcacaoServico : IPedidoMarcacaoService
    {
        private readonly IPedidoMarcacaoRepository _pedidoMarcacao;
        private readonly IUtenteRepository _utenteRepository;
        private readonly IUtilizadorService _utilizadorService;
        private readonly IEmailService _emailService;
        private readonly ITipoActoClinicoService _tipoActoClinicoService;
        private readonly IProfissionalService _profissionalService;

        public PedidoMarcacaoServico(
            IPedidoMarcacaoRepository pedidoMarcacao,
            IUtenteRepository utenteRepository,
            IUtilizadorService utilizadorService,
            IEmailService emailService,
            ITipoActoClinicoService tipoActoClinicoService,
            IProfissionalService profissionalService)
        {
            _pedidoMarcacao = pedidoMarcacao;
            _utenteRepository = utenteRepository;
            _utilizadorService = utilizadorService;
            _emailService = emailService;
            _tipoActoClinicoService = tipoActoClinicoService;
            _profissionalService = profissionalService;
        }

        public async Task<IEnumerable<PedidoMarcacaoDTO>> GetAllAsync()
        {
            var pedidoMercacao = await _pedidoMarcacao.GetAllAsync();
            return pedidoMercacao.Adapt<IEnumerable<PedidoMarcacaoDTO>>();
        }

        public async Task<PedidoMarcacaoDTO> GetByIdAsync(int id)
        {
            var pedidoMercacao = await _pedidoMarcacao.GetByIdAsync(id);
            return pedidoMercacao.Adapt<PedidoMarcacaoDTO>();
        }

        public async Task<PedidoMarcacaoDTO> CreateAsync(CriarPedidoMarcacaoDTO pedidoMarcacaoDto)
        {
            var pedidoMarcacao = pedidoMarcacaoDto.Adapt<PedidoMarcacao>();
            var novoPedido = await _pedidoMarcacao.AddAsync(pedidoMarcacao);
            return novoPedido.Adapt<PedidoMarcacaoDTO>();
        }

        public async Task<bool> UpdateAsync(int id, AtualizarPedidoMarcacaoDTO pedidoMarcacaoDto)
        {
            if (pedidoMarcacaoDto == null)
                throw new ArgumentNullException(nameof(pedidoMarcacaoDto));

            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));

            // Verificar se o pedido existe
            var pedidoExistente = await _pedidoMarcacao.GetByIdAsync(id);

            // Mapear apenas os campos que devem ser atualizados
            if (pedidoMarcacaoDto.InicioIntervalo.HasValue)
                pedidoExistente.InicioIntervalo = pedidoMarcacaoDto.InicioIntervalo.Value;
            
            if (pedidoMarcacaoDto.FimIntervalo.HasValue)
                pedidoExistente.FimIntervalo = pedidoMarcacaoDto.FimIntervalo.Value;
            
            if (pedidoMarcacaoDto.Estado.HasValue)
                pedidoExistente.Estado = pedidoMarcacaoDto.Estado.Value;
            
            if (!string.IsNullOrWhiteSpace(pedidoMarcacaoDto.Observacoes))
                pedidoExistente.Observacoes = pedidoMarcacaoDto.Observacoes;

            return await _pedidoMarcacao.UpdateAsync(pedidoExistente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _pedidoMarcacao.DeleteAsync(id);
        }

        public async Task<PedidoMarcacaoDTO> CriarPedidoAnonimoAsync(CriarUtenteDTO dadosUtente, CriarPedidoMarcacaoDTO pedidoMarcacaoDTO)
        {
            var utente = dadosUtente.Adapt<Utente>();
            var pedidoMarcacao = pedidoMarcacaoDTO.Adapt<PedidoMarcacao>();

            utente.EstadoUtente = EstadoUtente.UtenteAnonimo;

            var utenteCriado = await _utenteRepository.AddAsync(utente);

            pedidoMarcacao.UtenteId = utenteCriado.Id;

            var novoPedido = await _pedidoMarcacao.AddAsync(pedidoMarcacao);

            return novoPedido.Adapt<PedidoMarcacaoDTO>();
        }

        public async Task<bool> AgendarPedidoAsync(AgendarPedidoDto agendarPedidoDto)
        {
            var agendado = await _pedidoMarcacao.AgendarPedidoAsync(agendarPedidoDto);
            if (!agendado)
                return false;

            // Obter o pedido agendado para aceder ao utente
            var pedido = await _pedidoMarcacao.GetByIdAsync(agendarPedidoDto.PedidoId);
            if (pedido == null)
                return false;

            // Obter o utente associado
            var utente = await _utenteRepository.GetByIdAsync(pedido.UtenteId);
            if (utente == null)
                return false;

            // Se o utente for anónimo, converter para registado
            if (!utente.UtilizadorId.HasValue)
            {
                // Gerar senha aleatória
                var senha = Guid.NewGuid().ToString("N").Substring(0, 8);
                // Criar utilizador
                var utilizador = await _utilizadorService.CriarUtilizadorAsync(utente.EmailContacto, senha, Perfil.UtenteRegistado);
                // Converter utente para registado
                utente.UtilizadorId = utilizador.Id;
                utente.EstadoUtente = EstadoUtente.UtenteRegistado;
                await _utenteRepository.UpdateAsync(utente);
                // Enviar email de boas-vindas (sem senha)
                var assunto = "Bem-vindo à Clínica XPTO!";
                var mensagem = $"Bem-vindo à Clínica XPTO! O seu registo foi criado. Email: {utente.EmailContacto}";
                await _emailService.EnviarEmailAsync(utente.EmailContacto, assunto, mensagem);
            }

            return true;
        }

        public async Task<bool> RealizarPedidoAsync(int pedidoId, int utilizadorId, DateTime dataRealizacao)
        {
            return await _pedidoMarcacao.RealizarPedidoAsync(pedidoId, utilizadorId, dataRealizacao);
        }

        public async Task<IEnumerable<PedidoMarcacaoDTO>> ObterHistoricoPorUtenteAsync(int utenteId)
        {
            var pedidoMarcacoes = await _pedidoMarcacao.ObterPorUtenteAsync(utenteId); 

            return pedidoMarcacoes.Adapt<IEnumerable<PedidoMarcacaoDTO>>();
        }

        public async Task<IEnumerable<PedidoMarcacaoDTO>> PesquisarPedidosAsync(string numeroUtente, string nomeUtente, EstadoPedido? estado)
        {
            var pedidoMarcacoes = await _pedidoMarcacao.PesquisarAsync(numeroUtente, nomeUtente, estado);

            return pedidoMarcacoes.Adapt<IEnumerable<PedidoMarcacaoDTO>>();
        }

        public async Task<IEnumerable<PedidoMarcacaoDTO>> ObterPedidosPendentesAsync()
        {
            var pedidoMarcacoes = await _pedidoMarcacao.ObterPorEstadoAsync(EstadoPedido.Pendente);

            return pedidoMarcacoes.Adapt<IEnumerable<PedidoMarcacaoDTO>>();
        }

        public async Task<byte[]> ExportarMarcacaoParaPdfAsync(int pedidoId)
        {
            var pedido = await _pedidoMarcacao.GetByIdAsync(pedidoId);
            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com ID {pedidoId} não encontrado.");

            // Obter utente
            var utente = await _utenteRepository.GetByIdAsync(pedido.UtenteId);
            if (utente == null)
                throw new KeyNotFoundException($"Utente com ID {pedido.UtenteId} não encontrado.");

            // Preparar lista de atos clínicos e profissionais
            var listaAtos = new List<string>();
            foreach (var item in pedido.Itens)
            {
                string nomeTipoActo = "-";
                string nomeProfissional = "-";
                try {
                    var tipoActo = await _tipoActoClinicoService.GetByIdAsync(item.TipoActoClinicoId);
                    nomeTipoActo = tipoActo?.Descricao ?? "-";
                } catch { }
                if (item.ProfissionalId.HasValue)
                {
                    try {
                        var profissional = await _profissionalService.GetByIdAsync(item.ProfissionalId.Value);
                        nomeProfissional = profissional?.NomeCompleto ?? "-";
                    } catch { }
                }
                listaAtos.Add($"Ato Clínico: {nomeTipoActo} | Profissional: {nomeProfissional}");
            }

            // Gerar PDF
            var pdf = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text($"Detalhes da Marcação #{pedido.Id}").SemiBold().FontSize(18);
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Data do Pedido: {pedido.DataPedido:dd/MM/yyyy HH:mm}");
                        col.Item().Text($"Estado: {pedido.Estado}");
                        col.Item().Text("");
                        col.Item().Text($"Utente: {utente.NomeCompleto}");
                        col.Item().Text($"Email: {utente.EmailContacto}");
                        col.Item().Text($"Telemóvel: {utente.Telemovel}");
                        col.Item().Text($"Data/Hora Agendada: {(pedido.DataHoraAgendada.HasValue ? pedido.DataHoraAgendada.Value.ToString("dd/MM/yyyy HH:mm") : "-")}");
                        col.Item().Text($"Observações: {pedido.Observacoes}");
                        col.Item().Text("");
                        foreach (var ato in listaAtos)
                        {
                            col.Item().Text(ato);
                        }
                    });
                    page.Footer().AlignCenter().Text("Clínica XPTO - Exportação de Marcação").FontSize(10);
                });
            });
            return pdf.GeneratePdf();
        }
    }
}
