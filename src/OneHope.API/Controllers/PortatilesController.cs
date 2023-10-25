using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneHope.API.Models;
using System.Net;

namespace OneHope.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortatilesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly ILogger<PortatilesController> _logger;

        public PortatilesController(ApplicationDBContext context, ILogger<PortatilesController> logger)
        {
            _context = context;
            _logger = logger;
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
        [ProducesResponseType(typeof(IList<Portatil>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Portatil>> GetPortatilesParaComprar(string? nombre,
            string? marca, string? procesador, string? ram, int? precio)
        {
            var portatiles = await _context.Portatiles
                .Where(portatil => (nombre == null || portatil.Nombre.Contains(nombre)) &&
                      (marca == null || portatil.Marca.NombreMarca.Equals(marca)) &&
                      (procesador == null || portatil.Procesador.ModeloProcesador.Equals(procesador)) &&
                      (ram == null || portatil.Ram.Capacidad.Equals(ram)) &&
                      (precio == null || portatil.PrecioCompra.Equals(precio)) &&
                      (portatil.Stock > 0))
                .Include(Portatil => Portatil.Marca)
                .Include(Portatil => Portatil.Procesador)
                .Include(Portatil => Portatil.Ram)
                .ToListAsync();

            return Ok(portatiles);
        }
    }
}
