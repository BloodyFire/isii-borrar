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
    public class RamsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private ILogger _logger;

        public RamsController(ApplicationDBContext context, ILogger<PortatilesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<string>>> GetRam(string? capacidad)
        {

            IList<string> rams = await _context.Rams
                .Where(ram => (capacidad == null || ram.Capacidad.Contains(capacidad)))            
                .OrderBy(ram => ram.Capacidad)
                .Select(ram => ram.Capacidad)
                .ToListAsync();

            return Ok(rams);
        }
    }
}
