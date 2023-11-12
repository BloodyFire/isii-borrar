using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> GetComputingProcess(bool error)
        {
            if (!error) {
                string result = "Este es el resultado de un proceso complejo.";
                return Ok(result);
            }
            else
            {
                _logger.LogError($"{DateTime.Now} Critical error: proceso demasiado complejo.");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<PortatilParaComprarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPortatilesParaComprar(string? nombrePortatil,
            string? modeloPortatil, string? marcaPortatil, string? procesadorPortatil, string? ramPortatil, double? precioPortatil)
        {
            IList<PortatilParaComprarDTO> selectPortatiles= await _context.Portatiles
                .Include(p => p.Marca)
                .Include(p => p.Procesador)
                .Include(p => p.Ram)
                .Include(p => p.LineasCompra)
                .ThenInclude(po => po.Compra)
                .Where(portatil => portatil.Stock>0 
                && (nombrePortatil == null || portatil.Nombre.Contains(nombrePortatil))
                && (modeloPortatil == null || portatil.Modelo.Contains(modeloPortatil))
                && (marcaPortatil == null || portatil.Marca.NombreMarca.Equals(marcaPortatil))
                && (procesadorPortatil == null || portatil.Procesador.ModeloProcesador.Equals(procesadorPortatil))
                && (ramPortatil == null || portatil.Ram.Capacidad.Equals(ramPortatil))
                && (precioPortatil == null || portatil.PrecioCompra.Equals(precioPortatil)))
                .OrderBy(p=> p.Nombre)
                .Select(p => new PortatilParaComprarDTO(p.Id, p.Modelo, p.PrecioCompra,
                p.Ram.Capacidad, p.Marca.NombreMarca, p.Nombre, p.Procesador.ModeloProcesador, p.Stock)
                )
                .ToListAsync();

            return Ok(selectPortatiles);
        }
    }
}
