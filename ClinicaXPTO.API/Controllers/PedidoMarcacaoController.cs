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
    [Route("api/pedidoMarcacao")]
    public class PedidoMarcacaoController : ControllerBase
    {
        private readonly ClinicaXPTODbContext _context;

        public PedidoMarcacaoController(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var pedidoMarcacao = _context.PedidoMarcacoes.ToList().Select( p => p.ToPedidoMarcacao());

            return Ok(pedidoMarcacao);
        }

        [HttpGet("{id}")]

        public IActionResult GetById([FromRoute] int id)
        {
            var pedidoMarcacao = _context.PedidoMarcacoes.Find(id);

            if (pedidoMarcacao == null)
            {
                return NotFound();
            }

            return Ok(pedidoMarcacao.ToPedidoMarcacao());

        }

    }
}
