using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
