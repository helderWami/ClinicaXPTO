using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface IPedidoMarcacaoRepository
    {
        Task<IEnumerable<PedidoMarcacao>> GetAllAsync();
        Task<PedidoMarcacao> GetByIdAsync(int id);
        Task<PedidoMarcacao> AddAsync(PedidoMarcacao pedidoMarcacao);
        Task<bool> UpdateAsync(PedidoMarcacao pedidoMarcacao);
        Task<bool> DeleteAsync(int id);
    }
}
