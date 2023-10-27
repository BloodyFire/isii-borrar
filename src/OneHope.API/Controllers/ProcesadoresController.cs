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
    public class ProcesadoresController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private ILogger _logger;

        public ProcesadoresController(ApplicationDBContext context, ILogger<PortatilesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<string>>> GetProcesador(string? modeloProcesador)
        {

            IList<string> procesadores = await _context.Procesadores
                .Where(procesador => (modeloProcesador == null || procesador.ModeloProcesador.Contains(modeloProcesador)))            
                .OrderBy(procesador => procesador.ModeloProcesador)
                .Select(procesador => procesador.ModeloProcesador)
                .ToListAsync();

            return Ok(procesadores);
        }
    }
}
