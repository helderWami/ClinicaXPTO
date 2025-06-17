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
    [Route("api/tipoActoClinico")]
    public class TipoActoClinicoController : ControllerBase
    {
        private readonly ClinicaXPTODbContext _context;

        public TipoActoClinicoController(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var tipoactoclinico = _context.TipoActoClinicos.ToList().Select(t => t.ToActoClinico());

            return Ok(tipoactoclinico);
        }

        [HttpGet("{id}")]

        public IActionResult GetById([FromRoute] int id)
        {
            var actoclinico = _context.TipoActoClinicos.Find(id);

            if (actoclinico == null)
            {
                return NotFound();
            }

            return Ok(actoclinico.ToActoClinico());

        }
    }
}
