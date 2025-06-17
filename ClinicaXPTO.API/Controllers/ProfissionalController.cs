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
    [Route("api/profissional")]
    public class ProfissionalController : ControllerBase
    {
        public readonly ClinicaXPTODbContext _context;

        public ProfissionalController(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var profissional = _context.Profissionais.ToList().Select( p => p.ToProfissional());

            return Ok(profissional);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var profissional = _context.Profissionais.Find(id);

            if (profissional == null)
            {
                return NotFound();
            }

            return Ok(profissional.ToProfissional());

        }
    }

}
    
