using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneHope.API.Models;

namespace OneHope.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly ILogger<PagosController> _logger;
    }
}
