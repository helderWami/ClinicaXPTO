using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaXPTO.Models;

namespace ClinicaXPTO.Shared.Interfaces.Repositories
{
    public interface IProfissionalRepository
    {
        Task<IEnumerable<Profissional>> GetAllAsync();
        Task<Profissional> GetByIdAsync(int id);
        Task<Profissional> AddAsync(Profissional profissional);
        Task<bool> UpdateAsync(Profissional profissional);
        Task<bool> DeleteAsync(int id);
    }
}
