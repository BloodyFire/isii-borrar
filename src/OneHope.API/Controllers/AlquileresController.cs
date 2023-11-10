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
        private static int idAlquiler = 0;

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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetalleAlquilerDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CrearAlquiler(AlquilerParaCrearDTO alquilerParaCrear)
        {
            if (_context.Alquileres == null)
            {
                _logger.LogError("Error: La tabla Alquileres no existe");
                return Problem("Entity set 'ApplicationDBContext.Purchases'  is null.");
            }

            //any validation defined in PurchaseForCreate is checked before running the method so they don't have to be checked again
            if (alquilerParaCrear.FechaInAlquiler <= DateTime.Today)
                ModelState.AddModelError("FechaInAlquiler", "Error! Tu fecha de inicio de alquiler no puede empezar ni hoy ni antes");

            if (alquilerParaCrear.FechaFinAlquiler <= alquilerParaCrear.FechaInAlquiler)
                ModelState.AddModelError("FechaFinAlquiler&FechaInAlquiler", "Error! Tu fecha de fin de alquiler no puede acabar antes o el mismo dia que tu fecha de inicio");

            if (alquilerParaCrear.LineasAlquiler.Count == 0)
                ModelState.AddModelError("LineasAlquiler", "Error! Debes alquilar al menos un portatil");


            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var portatilIDs = alquilerParaCrear.LineasAlquiler.Select(la => la.PortatilID).ToList<int>();

            var movies = _context.Portatiles.Include(p => p.LineasAlquiler)
                .ThenInclude(la => la.Alquiler)
                .Where(p => portatilIDs.Contains(p.Id))

                //we use an anonymous type https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types
                .Select(p => new {
                    p.Id,
                    p.Modelo,
                    p.StockAlquilar,
                    //Elegimos los portatiles disponibles para alquilar
                    NumeroDePortatilesAlquilados = p.LineasAlquiler.Count(la => la.Alquiler.FechaInAlquiler <= alquilerParaCrear.FechaFinAlquiler
                            && la.Alquiler.FechaFinAlquiler >= alquilerParaCrear.FechaInAlquiler)
                })
                .ToList();



            Alquiler alquiler = new Alquiler(idAlquiler, DateTime.Now, alquilerParaCrear.FechaInAlquiler, alquilerParaCrear.FechaFinAlquiler, 
                alquilerParaCrear.Total, alquilerParaCrear.NombreCliente, alquilerParaCrear.ApellidosCliente, alquilerParaCrear.DireccionEnvio,
                alquilerParaCrear.EmailCliente, (int)alquilerParaCrear.TelefonoCliente, (OneHope.API.Models.TipoMetodoPago)alquilerParaCrear.TipoMetodoPago,
                new List<LineaAlquiler>());

            idAlquiler++;

            Portatil portatil;
            foreach (var linea in alquilerParaCrear.LineasAlquiler)
            {
                portatil = await _context.Portatiles.FindAsync(linea.PortatilID);
                //Check de que hay disponibles para alquilar
                if ((portatil == null) || (portatil.StockAlquilar < linea.Cantidad))
                {
                    ModelState.AddModelError("RentalItems", $"Error! Portatil con id '{linea.PortatilID}' no puede ser alquilado desde {alquilerParaCrear.FechaInAlquiler.ToShortDateString()} hasta {alquilerParaCrear.FechaFinAlquiler.ToShortDateString()}");
                }
                else
                {
                    // rental does not exist in the database yet and does not have a valid Id, so we must relate rentalitem to the object rental
                    alquiler.LineasAlquiler.Add(new LineaAlquiler(idLinea, linea.Cantidad, portatil, alquiler));
                    idLinea++;
                }
            }

            //if there is any problem because of the available quantity of movies or because the movie does not exist
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(alquiler);

            try
            {
                //we store in the database both rental and its rentalitems
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Rental", $"Error! Hubo un error al guardar tu alquiler, intentalo mas tarde");
            }

            //it returns rentalDetail
            var detalleAlquiler = new DetalleAlquilerDTO(alquiler.ID, alquiler.FechaAlquiler,
                alquiler.EmailCliente, alquiler.NombreCliente, alquiler.ApellidosCliente,
                alquiler.DireccionEnvio, alquiler.TelefonoCliente, alquilerParaCrear.TipoMetodoPago,
                alquiler.FechaInAlquiler, alquiler.FechaFinAlquiler,
                alquilerParaCrear.LineasAlquiler);

            return CreatedAtAction("GetRental", new { id = alquiler.ID }, detalleAlquiler);
        }
    }
}
