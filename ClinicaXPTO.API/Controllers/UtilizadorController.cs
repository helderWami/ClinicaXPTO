using ClinicaXPTO.API.DAL;
using ClinicaXPTO.API.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaXPTO.API.Controllers
{
    [ApiController]
    [Route("api/utilizador")]

    public class UtilizadorController : ControllerBase
    {

        private readonly ClinicaXPTODbContext _context;

        public UtilizadorController(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var utilizador = _context.Utilizadores.ToList().Select(u => u.ToUtilizadorDTO());

            return Ok(utilizador);
        }

        [HttpGet("{id}")]

        public IActionResult GetById([FromRoute] int id)
        {
            var utilizador = _context.Utilizadores.Find(id);

            if (utilizador == null)
            {
                return NotFound();
            }

            return Ok(utilizador.ToUtilizadorDTO());
        }

    }
}
