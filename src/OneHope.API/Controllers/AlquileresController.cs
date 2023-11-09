using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneHope.API.Models;
using OneHope.Shared.AlquilerDTOs;
using System.Net;

namespace OneHope.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquileresController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<AlquileresController> _logger;

        private static int idLinea = 0;

        public AlquileresController(ApplicationDBContext context, ILogger<AlquileresController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetalleAlquilerDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetAlquiler(int id)
        {
            if (_context.Alquileres == null)
            {
                _logger.LogError("Error: La tabla de Alquileres no existe");
                return NotFound();
            }

            var alquilerDTO = await _context.Alquileres
             .Where(alquiler => alquiler.ID == id)
                 .Include(alquiler => alquiler.LineasAlquiler) //join table RentalItems
                    .ThenInclude(ai => ai.Portatil) //then join table Movies
                        .ThenInclude(movie => movie.Marca) //then join table Genre
             .Select(alquiler => new DetalleAlquilerDTO(alquiler.ID, alquiler.FechaAlquiler, alquiler.EmailCliente, alquiler.NombreCliente,
                    alquiler.ApellidosCliente, alquiler.DireccionEnvio, alquiler.TelefonoCliente,
                    (Shared.TipoMetodoPago)alquiler.MetodoPago,
                    alquiler.FechaInAlquiler, alquiler.FechaFinAlquiler,
                    alquiler.LineasAlquiler
                        .Select(la => new LineaAlquilerDTO(la.ID,la.Portatil.Id,
                                la.Portatil.PrecioAlquiler, la.Cantidad)).ToList<LineaAlquilerDTO>()))
             .FirstOrDefaultAsync();

            if (alquilerDTO == null)
            {
                _logger.LogError($"Error: Alquiler con id {id} no existe");
                return NotFound();
            }


            return Ok(alquilerDTO);
        }

        
    }
}
