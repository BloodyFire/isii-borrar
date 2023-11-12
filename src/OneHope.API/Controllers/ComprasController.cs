using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneHope.API.Models;
using OneHope.Shared.CompraDTOs;
using OneHope.Shared.PortatilDTOs;
using System.Net;

namespace OneHope.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<ComprasController> _logger;

        public ComprasController(ApplicationDBContext context, ILogger<ComprasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetallesCompraDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCompra(int id)
        {
            if (_context.Compras == null)
            {
                _logger.LogError("Error: la tabla de compras no existe");
                return NotFound();
            }

            var compraDto = await _context.Compras
                .Where(compra => compra.Id == id)
                    .Include(compra => compra.LineasCompra)
                        .ThenInclude(lineaCompra => lineaCompra.Portatil)
                            .ThenInclude(portatil => portatil.Ram)
                    .Include(compra => compra.LineasCompra)
                        .ThenInclude(portatil => portatil.Portatil)
                            .ThenInclude(portatil => portatil.Procesador)
                    .Include(compra => compra.LineasCompra)
                        .ThenInclude(portatil => portatil.Portatil)
                            .ThenInclude(portatil => portatil.Marca)
                .Select(compra => new DetallesCompraDTO(compra.Id, compra.NombreCliente, compra.Apellidos, compra.Direccion,
                compra.LineasCompra
                            .Select(pi => new LineaCompraDTO(pi.Portatil.Id, pi.Portatil.Nombre, pi.Portatil.PrecioCompra,
                                    pi.Portatil.Marca.NombreMarca, pi.Portatil.Procesador.ModeloProcesador, pi.Portatil.Ram.Capacidad, pi.Cantidad))
                                    .ToList<LineaCompraDTO>(),
                       (OneHope.Shared.TipoMetodoPago)compra.MetodoPago, compra.FechaCompra))
                .FirstOrDefaultAsync();

            if (compraDto == null)
            {
                _logger.LogError($"Error: La compra con id {id} no existe.");
                return NotFound();
            }

            return Ok(compraDto);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetallesCompraDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails),(int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CrearCompra(CompraPorCrearDTO compraPorCrear)
        {
            if (_context.Compras == null)
            {
                _logger.LogError("Error: la tabla compra no existe.");
                return Problem("Entity set 'ApplicationDBContext.Compras'  is null.");
            }

            if(compraPorCrear.LineasCompra.Count == 0)
            {
                ModelState.AddModelError("LineasCompra", "Error! Debes incluir al menos un portátil para comprarlo");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var portatilID = compraPorCrear.LineasCompra.Select(lc => lc.PortatilID).ToList<int>();

            var portatiles = _context.Portatiles.Include(p => p.LineasCompra)
                .ThenInclude(lc => lc.Compra)
                .Where(p => portatilID.Contains(p.Id))
                .Select(p => new
                {
                    p.Id,
                    p.Modelo,
                    p.Marca,
                    p.Procesador,
                    p.Stock,
                    NumeroDePortatilesComprados = p.LineasCompra.Count(),
                    p.PrecioCompra
                })
                .ToList();

            foreach(LineaCompraDTO lineaCompra in compraPorCrear.LineasCompra)
            {
                var portatilCambio = portatiles.FirstOrDefault(p => p.Id == lineaCompra.PortatilID);

                if (portatilCambio != null)
                {
                    lineaCompra.PrecioUnitario = portatilCambio.PrecioCompra;
                }
            }

            Compra compra = new Compra(compraPorCrear.NombreUsuario, compraPorCrear.ApellidosUsuario, compraPorCrear.Direccion, DateTime.Today,
                new List<LineaCompra>(), (Shared.TipoMetodoPago)compraPorCrear.MetodoPago, compraPorCrear.PrecioTotal);

            Portatil portatil;
            foreach (var item in compraPorCrear.LineasCompra)
            {
                portatil = await _context.Portatiles.FindAsync(item.PortatilID);
                if(portatil == null)
                {
                    ModelState.AddModelError("LineasCompra", $"Error! El portátil con nombre {item.Nombre} y con ID {item.PortatilID} no existe en la base de datos.");

                }
                else{
                    if (portatil.Stock < item.Cantidad)
                    {
                        ModelState.AddModelError("LineasCompra", $"Error! El portátil con nombre {item.Nombre} solo tiene {portatil.Stock} unidades disponibles, pero has seleccionado {item.Cantidad} unidades para comprar.");
                    }
                    else
                    {
                        portatil.Stock -= item.Cantidad;
                        compra.LineasCompra.Add(new LineaCompra(portatil, item.Cantidad, compra));
                    }
                }

            }

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            var detalleCompra = new DetallesCompraDTO(compra.Id, compra.NombreCliente,
                compra.Apellidos, compra.Direccion, compraPorCrear.LineasCompra,
                compraPorCrear.MetodoPago, compra.FechaCompra);

            return CreatedAtAction("GetCompra", new { id = compra.Id }, detalleCompra);

        }
        
        private bool PurchaseExists(int id)
        {
            return (_context.Compras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
