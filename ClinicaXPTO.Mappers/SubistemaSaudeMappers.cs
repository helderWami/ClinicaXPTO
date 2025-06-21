using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.Mappers
{
    public static class SubistemaSaudeMappers
    {

        public static SubsistemaSaudeDTO ToSubistemaSaude (this SubsistemaSaude subsistemaSaudeModel)
        {
            return new SubsistemaSaudeDTO
            {
                Id = subsistemaSaudeModel.Id,
                Nome = subsistemaSaudeModel.Nome

            };
        }


    }
}
