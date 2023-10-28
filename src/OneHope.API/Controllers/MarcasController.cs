using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
//using OneHope.Shared.PortatilDTOs;
using OneHope.API.Models;

namespace OneHope.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private ILogger _logger;

        public MarcasController(ApplicationDBContext context, ILogger<PortatilesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<string>>> GetMarca(string? nombreMarca)
        {

            IList<string> marcas = await _context.Marcas
                .Where(marca => (nombreMarca == null || marca.NombreMarca.Contains(nombreMarca)))            
                .OrderBy(marca => marca.NombreMarca)
                .Select(marca => marca.NombreMarca)
                .ToListAsync();

            return Ok(marcas);
        }
    }
}
