using ClinicaXPTO.DTO;
using ClinicaXPTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.Mappers
{
    public static class TipoActoClinicoMappers
    {
        public static TipoActoClinicoDTO ToActoClinico(this TipoActoClinico tipoActoClinicoModel)
        {
            return new TipoActoClinicoDTO
            {
                Id  = tipoActoClinicoModel.Id,
                Descricao = tipoActoClinicoModel.Descricao,
                DuracaoPadrao = tipoActoClinicoModel.DuracaoPadrao,
                Preco = tipoActoClinicoModel.Preco
            };

        }

    }
}
