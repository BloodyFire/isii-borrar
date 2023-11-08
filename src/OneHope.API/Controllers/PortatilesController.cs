using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OneHope.API.Models;
using Microsoft.EntityFrameworkCore;
using OneHope.Shared.PortatilDTOs;
using System.Net;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

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
        [ProducesResponseType(typeof(IList<PortatilesParaDevolverDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IList<PortatilesParaDevolverDTO>>> GetPortatilesParaDevolver(int? idCompra, DateTime? fecha, int? CustomerId)
        {
            
                     

            DateTime defaultDate = DateTime.Now.Date.AddDays(-30);
           


            IList<PortatilesParaDevolverDTO> portatiles = await _context.LineaCompra
                    .Include(portatil => portatil.Compra)
                    .Include(compra => compra.Portatil)
                    .ThenInclude(marca => marca.Marca)
                    .Where(portatil => (CustomerId == null || portatil.Compra.CustomerId.Equals(CustomerId)) &&
                                       (idCompra == null || portatil.IdCompra.Equals(idCompra)) &&
                                       ((fecha == null || portatil.Compra.FechaCompra.Equals(fecha)) &&
                                        portatil.Compra.FechaCompra >= defaultDate)
                                       )
                    .OrderBy(portatil => portatil.Compra.FechaCompra)
                    .Select(portatil => new PortatilesParaDevolverDTO(portatil.IdCompra, portatil.Portatil.Marca.NombreMarca, portatil.Cantidad,
                    portatil.Compra.FechaCompra, portatil.Compra.Total)
                     ).ToListAsync();
            return Ok(portatiles);

        }

    }



}
