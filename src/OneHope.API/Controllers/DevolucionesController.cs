
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneHope.API.Models;
using System.Net;
using OneHope.Shared.DevolucionDTOs;
using System.Linq;

namespace OneHope.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DevolucionesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<DevolucionesController> _logger;

        public DevolucionesController(ApplicationDBContext context, ILogger<DevolucionesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(DevolucionDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDevolucion(int id)
        {
            if (_context.Devoluciones == null)
            {
                _logger.LogError("Error: La tabla devoluciones no existe");
                return NotFound();
            }


            var devolucion = await _context.Devoluciones
                                   .Include(l => l.LineaDevolucion)
                                   .ThenInclude(d => d.LineaCompra)
                                   .ThenInclude(c => c.Compra)
                                   .Where(d => d.IdDevolucion == id)
                                   .Select(d => new DevolucionDetailDTO(d.IdDevolucion, d.MotivoDevolucion, d.DireccionRecogida,
                                       d.Fecha, d.LineaDevolucion
                                           .Select(l => new DevolucionItemDTO(l.LineaCompra.IdPortatil, l.Cantidad,
                                           l.LineaCompra.Portatil.Modelo, l.LineaCompra.IdCompra, l.LineaCompra.IdLinea, l.LineaCompra.PrecioUnitario)).ToList<DevolucionItemDTO>(), d.NotaRepartidor
                                       )).FirstOrDefaultAsync();

            if (devolucion == null)
            {
                _logger.LogError($"Error: Devolucion con id {id} no existe");
                return NotFound();
            }

            return Ok(devolucion);
            }

        
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DevolucionDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateDevolucion(DevolucionForCreateDTO devolucionForCreate)
        {
            if (_context.Devoluciones == null)
            {
                _logger.LogError("Error: No existe la tabla devoluciones");
                return Problem("Entity set 'ApplicationDBContext.Devoluciones'  is null.");
            }


            if (devolucionForCreate.LineasDevoluciones.Count == 0) {
                ModelState.AddModelError("LineasCompra", "Error: Tienes que incluir al menos un portatil en la devolucion.");
                return BadRequest(new ValidationProblemDetails(ModelState));

            }


            Devolucion devolucion = new Devolucion(DateTime.Now,
                devolucionForCreate.CuantiaDevolucion, devolucionForCreate.DireccionRecogida, devolucionForCreate.NotaRepartidor,
                devolucionForCreate.MotivoDevolucion);


            LineaCompra lineaCompra; 

            foreach (var linea in devolucionForCreate.LineasDevoluciones)
            {
            
                    lineaCompra = await _context.LineaCompra.FindAsync(linea.IdLineaCompra);
                    if (lineaCompra == null)
                    {
                        ModelState.AddModelError("LineasDevolucion", $"Error: El portatil modelo {linea.Modelo} con Id {linea.IdPortatil} no existe en la base de datos.");
                    }
                    else
                    {
                        if (lineaCompra.Cantidad < linea.Cantidad)
                        {
                            ModelState.AddModelError("LineaDevolucion", $"Error! No puedes devolver mas portatiles de los comprados");
                        }
                        else
                        {
                            //we decrease the number of laptops available
                            devolucion.LineaDevolucion.Add(new LineaDevolucion( linea.Cantidad ,lineaCompra, devolucion ));
                        }
                    }
                    

            }

                //if there is any problem because of the available quantity of movies or because the movie does not exist
                if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(devolucion);

            try
            {
                //we store in the database both rental and its rentalitems
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Devolucion", $"Error! Ha habido un error guardando tu devolucion, por favor, prueba mas tarde");
                return BadRequest(new ValidationProblemDetails(ModelState));

            }

            //it returns devolucionDetail
            var devolucionDetail = new DevolucionDetailDTO(devolucion.IdDevolucion, devolucion.MotivoDevolucion, 
                devolucion.DireccionRecogida, devolucion.Fecha,
                devolucionForCreate.LineasDevoluciones, devolucion.NotaRepartidor);

            return CreatedAtAction("GetDevolucion", new { id = devolucion.IdDevolucion}, devolucionDetail);
        }
        
        
    }
}
