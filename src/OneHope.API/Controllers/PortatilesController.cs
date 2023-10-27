using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OneHope.API.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Azure.Identity;
using OneHope.Shared.PortatilDTO;

namespace OneHope.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortatilesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<PortatilesController> _logger;

        public PortatilesController(
            ApplicationDBContext context, ILogger<PortatilesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<PortatilParaPedidoDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PortatilParaPedidoDTO>> GetPortatilesParaPedido()
        {
            var portatiles = await _context.Portatiles
                .Include(portatil => portatil.Marca)
                .Include(portatil => portatil.Procesador)
                .Include(portatil => portatil.Ram)
                .Include(portatil => portatil.Proveedor)
                .Select(portatil => new PortatilParaPedidoDTO(portatil.Id, portatil.Modelo ,portatil.Marca.NombreMarca, portatil.Stock, portatil.PrecioCoste, portatil.Proveedor.Nombre))
                .ToListAsync();

            return Ok(portatiles);
        }
    }
}
