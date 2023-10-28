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
    public class ProveedoresController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private ILogger _logger;

        public ProveedoresController(ApplicationDBContext context, ILogger<PortatilesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<string>>> GetProveedor(string? nombre)
        {

            IList<string> proveedores = await _context.Proveedores
                .Where(proveedor => (nombre == null || proveedor.Nombre.Contains(nombre)))            
                .OrderBy(proveedor => proveedor.Nombre)
                .Select(proveedor => proveedor.Nombre)
                .ToListAsync();

            return Ok(proveedores);
        }
    }
}
