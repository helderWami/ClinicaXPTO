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
    [Route("api/SubsistemaSaude")]
    public class SubsistemaSaudeController : ControllerBase
    {
        private readonly ClinicaXPTODbContext _context;

        public SubsistemaSaudeController(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var subsistemaSaude = _context.SubsistemasSaude.ToList().Select(s => s.ToSubistemaSaude());

            return Ok(subsistemaSaude);

        }

        [HttpGet("{id}")]

        public IActionResult GetById([FromRoute] int id)
        {
            var subsistemaSaude = _context.SubsistemasSaude.Find(id);

            if (subsistemaSaude == null)
            {
                return NotFound();
            }

            return Ok(subsistemaSaude.ToSubistemaSaude());

        }


    }
}
