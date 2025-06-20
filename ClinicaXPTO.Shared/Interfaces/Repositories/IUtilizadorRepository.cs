using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface IUtilizadorRepository
    {
        Task<IEnumerable<Utilizador>> GetAllAsync();
        Task<Utilizador> GetByIdAsync(int id);
        Task<Utilizador> AddAsync(Utilizador utilizador);
        Task<bool> UpdateAsync(Utilizador utilizador);
        Task<bool> DeleteAsync(int id);
    }
}
