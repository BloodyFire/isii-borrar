using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OneHope.API.Models;
using Microsoft.EntityFrameworkCore;
using OneHope.Shared.PortatilDTOs;
using System.Net;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

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

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<PortatilesParaDevolverDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IList<PortatilesParaDevolverDTO>>> GetPortatilesParaDevolver(int? idCompra, DateTime? fecha, int CustomerId)
        {
            
                     

            DateTime defaultDate = DateTime.Now.Date.AddDays(-30);
           


            IList<PortatilesParaDevolverDTO> portatiles = await _context.LineaCompra
                    .Include(portatil => portatil.Compra)
                    .Include(compra => compra.Portatil)
                    .ThenInclude(marca => marca.Marca)
                    .Where(portatil => (portatil.Compra.CustomerId.Equals(CustomerId)) &&
                                       (idCompra == null || portatil.IdCompra.Equals(idCompra)) &&
                                       ((fecha == null || portatil.Compra.FechaCompra.Equals(fecha)) &&
                                        portatil.Compra.FechaCompra >= defaultDate)
                                       )
                    .OrderBy(portatil => portatil.Compra.FechaCompra)
                    .Select(portatil => new PortatilesParaDevolverDTO(portatil.IdCompra, portatil.Portatil.Marca.NombreMarca, portatil.Cantidad,
                    portatil.Compra.FechaCompra, portatil.PrecioUnitario)
                     ).ToListAsync();
            return Ok(portatiles);

        }

    }



}
