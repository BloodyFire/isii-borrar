using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OneHope.API.Models;
using Microsoft.EntityFrameworkCore;
using OneHope.Shared.PortatilDTOs;
using System.Net;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OneHope.Design.Models;

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
        public async Task<ActionResult<PortatilParaPedidoDTO>> GetPortatilesParaPedido(string? filtroModelo, string? filtroMarca, int? filtroStockMinimo, int? filtroStockMaximo, string? filtroProveedor, string? filtroNombre)
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
                                    (filtroProveedor == null || portatil.Proveedor.Nombre.Equals(filtroProveedor)) &&
                                    (filtroNombre == null || portatil.Nombre.Contains(filtroNombre)))
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
        public async Task<ActionResult<IList<PortatilesParaDevolverDTO>>> GetPortatilesParaDevolver(int? idCompra, DateTime? fecha, int CustomerId, double? precio)
        {
            
                     

            DateTime defaultDate = DateTime.Now.Date.AddDays(-30);
           


            IList<PortatilesParaDevolverDTO> portatiles = await _context.LineaCompra
                    .Include(portatil => portatil.Compra)
                    .Include(compra => compra.Portatil)
                    .ThenInclude(marca => marca.Marca)
                    .Where(portatil => (portatil.Compra.CustomerId.Equals(CustomerId)) &&
                                       (idCompra == null || portatil.IdCompra.Equals(idCompra)) &&
                                       (precio == null || portatil.PrecioUnitario > precio) &&
                                       ((fecha == null || portatil.Compra.FechaCompra.Equals(fecha)) &&
                                        portatil.Compra.FechaCompra >= defaultDate)
                                       )
                    .OrderBy(portatil => portatil.Compra.FechaCompra)
                    .Select(portatil => new PortatilesParaDevolverDTO(portatil.IdCompra, portatil.Portatil.Marca.NombreMarca, portatil.Cantidad,
                    portatil.Compra.FechaCompra, portatil.PrecioUnitario)
                     ).ToListAsync();
            return Ok(portatiles);

        }


        
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<PortatilParaComprarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPortatilesParaComprar(string? nombrePortatil,
            string? modeloPortatil, string? marcaPortatil, string? procesadorPortatil, string? ramPortatil, double? precioPortatil, int? stock)
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
                && (precioPortatil == null || portatil.PrecioCompra >= precioPortatil)
                && (stock==null || portatil.Stock > stock))
                .OrderBy(p=> p.Nombre)
                .Select(p => new PortatilParaComprarDTO(p.Id, p.Modelo, p.PrecioCompra,
                p.Ram.Capacidad, p.Marca.NombreMarca, p.Nombre, p.Procesador.ModeloProcesador, p.Stock)
                )
                .ToListAsync();

            return Ok(selectPortatiles);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<PortatilParaAlquilerDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PortatilParaAlquilerDTO>> GetPortatilesParaAlquiler(string? filtroMarca, string? filtroProcesador, string? filtroRam, string? filtroModelo)
        {
            var portatiles = await _context.Portatiles
                .Where(portatil => ((filtroMarca == null || portatil.Marca.NombreMarca.Equals(filtroMarca)) &&
                                    (filtroProcesador == null || portatil.Procesador.ModeloProcesador.Equals(filtroProcesador)) &&
                                    (filtroRam == null || portatil.Ram.Capacidad.Equals(filtroRam)) &&
                                    (filtroModelo == null || portatil.Modelo.Contains(filtroModelo))
                                    ))
                .Include(portatil => portatil.Ram)
                .Include(portatil => portatil.Procesador)
                .OrderBy(portatil => portatil.StockAlquilar)
                .Select(portatil => new PortatilParaAlquilerDTO(portatil.Id, portatil.Modelo, portatil.Marca.NombreMarca, portatil.Procesador.ModeloProcesador, portatil.Ram.Capacidad, portatil.StockAlquilar, portatil.PrecioAlquiler))
                .ToListAsync();

            return Ok(portatiles);
        }
    }



}
