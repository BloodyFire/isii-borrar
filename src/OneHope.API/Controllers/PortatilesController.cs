using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OneHope.API.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using OneHope.Shared.PortatilDTOs;

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
        public async Task<ActionResult<PortatilParaPedidoDTO>> GetPortatilesParaPedido(string? filtroModelo, string? filtroMarca, int? filtroStockMinimo, int? filtroStockMaximo, string? filtroProveedor)
        {

            if (filtroStockMinimo != null && filtroStockMaximo != null && filtroStockMinimo > filtroStockMaximo)
            {
                ModelState.AddModelError("filtroStockMinimo&filtroStockMaximo", "filtroStockMinimo debe ser menor que filtroStockMaximo");
                _logger.LogError($"{DateTime.Now} Error: filtroStockMinimo debe ser menor que filtroStockMaximo");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            IList<PortatilParaPedidoDTO> portatiles = await _context.Portatiles
                .Where(portatil =>  (filtroModelo == null || portatil.Modelo.Contains(filtroModelo)) &&
                                    (filtroMarca == null || portatil.Marca.NombreMarca.Equals(filtroMarca)) &&
                                    (filtroStockMinimo == null || portatil.Stock >= filtroStockMinimo) &&
                                    (filtroStockMaximo == null || portatil.Stock <= filtroStockMaximo) &&
                                    (filtroProveedor == null || portatil.Proveedor.Nombre.Equals(filtroProveedor)))
                .Include(portatil => portatil.Ram)
                .Include(portatil => portatil.Proveedor)
                .OrderBy(portatil => portatil.Stock)
                .Select(portatil => new PortatilParaPedidoDTO(portatil.Id, portatil.Modelo ,portatil.Marca.NombreMarca, portatil.Stock, portatil.PrecioCoste, portatil.Proveedor.Nombre))
                .ToListAsync();

            return Ok(portatiles);
        }
    }
}
