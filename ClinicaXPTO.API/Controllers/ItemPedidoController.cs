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
    [Route("api/itemPedido")]
    public class ItemPedidoController : ControllerBase
    {
        private readonly ClinicaXPTODbContext _context;

        public ItemPedidoController(ClinicaXPTODbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var itemPedido = _context.ItemPedidos.ToList().Select( i => i.ToItemPedido());

            return Ok(itemPedido);
        }

        [HttpGet("{id}")]

        public IActionResult GetById([FromRoute] int id)
        {
            var itemPedido = _context.ItemPedidos.Find(id);

            if (itemPedido == null)
            {
                return NotFound();
            }

            return Ok(itemPedido.ToItemPedido());

        }


    }
}
