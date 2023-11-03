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
        public async Task<ActionResult<PortatilParaAlquilerDTO>> GetPortatilesParaAlquiler(string? filtroMarca, string? filtroProcesador, string? filtroRam, int? filtroCantidadMinima, int? filtroCantidadMaxima)
        {
            var portatiles = await _context.Portatiles
                .Where(portatil => ((filtroMarca == null || portatil.Marca.NombreMarca.Equals(filtroMarca)) &&
                                    (filtroProcesador == null || portatil.Modelo.Contains(filtroProcesador)) &&
                                    (filtroRam == null || portatil.Modelo.Contains(filtroRam)) &&
                                    (filtroCantidadMinima == null || portatil.Cantidad >= filtroCantidadMinima) &&
                                    (filtroCantidadMaxima == null || portatil.Cantidad <= filtroCantidadMaxima) 
                                    ))
                .Include(portatil => portatil.Ram)
                .Include(portatil => portatil.Procesador)
                .OrderBy(portatil => portatil.Cantidad)
                .Select(portatil => new PortatilParaAlquilerDTO(portatil.Id, portatil.Nombre, portatil.Marca.NombreMarca, portatil.Procesador.ModeloProcesador, portatil.Cantidad, portatil.PrecioAlquiler))
                .ToListAsync();

            return Ok(portatiles);
        }
    }
}
