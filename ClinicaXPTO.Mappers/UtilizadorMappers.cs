using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.Mappers
{
    public static class UtilizadorMappers
    {

        public static UtilizadorDTO ToUtilizadorDTO(this Utilizador utilizadorModel)
        {
            return new UtilizadorDTO
            {
                Id = utilizadorModel.Id,
                Email = utilizadorModel.Email,
                //Perfil = utilizadorModel.Perfil.ToString()
            };

        }


    }
}
