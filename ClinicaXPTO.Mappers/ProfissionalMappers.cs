using ClinicaXPTO.API.DTO;
using ClinicaXPTO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.Mappers
{
    public static class ProfissionalMappers
    {

        public static ProfissionalDTO ToProfissional (this Profissional profissionalModel )
        {
            return new ProfissionalDTO
            {
                Id = profissionalModel.Id,
                NomeCompleto = profissionalModel.NomeCompleto,
                Especialidade = profissionalModel.Especialidade

            };
        }
           
    }
}
