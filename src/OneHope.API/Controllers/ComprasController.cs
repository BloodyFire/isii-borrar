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
        [ProducesResponseType(typeof(IList<DetallesCompraDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<DetallesCompraDTO>> GetCompras(int id)
        {
            if(_context.Compras == null)
            {
                _logger.LogError("Error: la tabla de compras no existe");
                return NotFound();
            }
            //Partir de Portatil.
            var compraDto = await _context.Compras
                .Where(compra => compra.Id == id)
                .Include(compra => compra.LineasCompra)
                    .ThenInclude(compraPortatil => compraPortatil.Portatil)
                        .ThenInclude(portatil => portatil.Ram)
                .Include(compra => compra.LineasCompra)
                    .ThenInclude(portatil => portatil.Portatil)
                        .ThenInclude(portatil => portatil.Procesador)
                .Include(compra => compra.LineasCompra)
                    .ThenInclude(portatil => portatil.Portatil)
                        .ThenInclude(portatil => portatil.Marca)
                .Select(compra => new DetallesCompraDTO(compra.Id, compra.NombreCliente, compra.Apellidos, compra.Direccion,
                compra.LineasCompra
                            .Select(pi => new CompraPortatilDTO(pi.Portatil.Id, pi.Portatil.Nombre, pi.Portatil.PrecioCompra,
                            pi.Portatil.Marca.NombreMarca, pi.Portatil.Procesador.ModeloProcesador, pi.Portatil.Ram.Capacidad, pi.Portatil.Stock))
                       .ToList<CompraPortatilDTO>(),
                       (OneHope.Shared.TipoMetodoPago)compra.MetodoPago, compra.FechaCompra))
                .FirstOrDefaultAsync();

            if (compraDto == null)
            {
                _logger.LogError($"Error: La compra con id {id} no existe.");
                return NotFound();
            }

            return Ok(compraDto);
        }
    }
}
