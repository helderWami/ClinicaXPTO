using ClinicaXPTO.API.DTO;
using ClinicaXPTO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.Mappers
{
    public static class UtenteMappers
    {

        public static UtenteDTO ToUtenteDto (this Utente utenteModel )
        {
            return new UtenteDTO
            {
                Id = utenteModel.Id,
                NumeroUtente = utenteModel.NumeroUtente,
                NomeCompleto = utenteModel.NomeCompleto,
                DataNascimento = utenteModel.DataNascimento,
                Genero = utenteModel.Genero.HasValue ? utenteModel.Genero.ToString() : null,
                Telemovel = utenteModel.Telemovel,
                EmailContacto = utenteModel.EmailContacto,
                Morada = utenteModel.Morada
            };

        }

    }
}
