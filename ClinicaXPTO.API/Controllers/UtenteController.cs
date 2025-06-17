using ClinicaXPTO.API.DAL;
using ClinicaXPTO.API.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.Controllers
{

    namespace ClinicaXPTO.API.Controllers
    {

        [ApiController]
        [Route("api/utente")]
        public class UtenteController : ControllerBase
        {
            private readonly ClinicaXPTODbContext _context;
            public UtenteController(ClinicaXPTODbContext context)
            {

                _context = context;

            }

            [HttpGet]

            public IActionResult GetAll()
            {
                var utentes = _context.Utentes.ToList().Select(u => u.ToUtenteDto());

                return Ok(utentes);
            }

            [HttpGet("{id}")]
            public IActionResult GetById([FromRoute] int id)
            {
                var utente = _context.Utentes.Find(id);

                if (utente == null)
                {
                    return NotFound();
                }

                return Ok(utente.ToUtenteDto());
            }


        }
    }

}
