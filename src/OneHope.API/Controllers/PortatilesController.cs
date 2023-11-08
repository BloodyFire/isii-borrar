using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OneHope.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
        [ProducesResponseType(typeof(IList<PortatilParaAlquilerDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PortatilParaAlquilerDTO>> GetPortatilesParaAlquiler(string? filtroMarca, string? filtroProcesador, string? filtroRam)
        {
            var portatiles = await _context.Portatiles
                .Where(portatil => ((filtroMarca == null || portatil.Marca.NombreMarca.Equals(filtroMarca)) &&
                                    (filtroProcesador == null || portatil.Procesador.ModeloProcesador.Equals(filtroProcesador)) &&
                                    (filtroRam == null || portatil.Ram.Capacidad.Equals(filtroRam))
                                    ))
                .Include(portatil => portatil.Ram)
                .Include(portatil => portatil.Procesador)
                .OrderBy(portatil => portatil.StockAlquilar)
                .Select(portatil => new PortatilParaAlquilerDTO(portatil.Id, portatil.Modelo, portatil.Marca.NombreMarca, portatil.Procesador.ModeloProcesador, portatil.StockAlquilar, portatil.PrecioAlquiler))
                .ToListAsync();

            return Ok(portatiles);
        }
    }
}
