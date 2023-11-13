using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OneHope.API.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using OneHope.Shared.PedidoDTOs;

namespace OneHope.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<PedidosController> _logger;

        public PedidosController(
           ApplicationDBContext context, ILogger<PedidosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetallePedidoDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreatePedido(PedidoParaCrearDTO pedidoParaCrear)
        {
            if (_context.Pedidos == null)
            {
                _logger.LogError("Error: No existe la tabla Pedidos");
                return Problem("Entity set 'ApplicationDBContext.Pedidos'  is null.");
            }

            if (pedidoParaCrear.LineasPedido.Count == 0)
            {
                ModelState.AddModelError("LineasPedido", "Error: Tienes que incluir al menos un portatil en el pedido.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }


            Pedido pedido = new Pedido(pedidoParaCrear.Total, DateTime.Now, pedidoParaCrear.CodigoEmpleado,
                pedidoParaCrear.Direccion,  new List<LineaPedido>(), pedidoParaCrear.TipoMetodoPago, pedidoParaCrear.Comentarios);

            Portatil portatil;
            foreach (var linea in pedidoParaCrear.LineasPedido)
            {
                portatil = await _context.Portatiles.FindAsync(linea.PortatilID);
                if (portatil == null)
                {
                    ModelState.AddModelError("LineasPedido", $"Error: El portatil modelo {linea.Modelo} con Id {linea.PortatilID} no existe en la base de datos.");
                }
                else
                {
                    //Actualizamos el precio y modelo por los de la bbdd para garantizar la integridad de los datos.
                    linea.PrecioUnitario = portatil.PrecioCoste;
                    linea.Modelo = portatil.Modelo;
                    pedido.LineasPedido.Add(new LineaPedido(portatil, linea.Cantidad, pedido, portatil.PrecioCoste));
                }
                /* No es necesario validar el stock. No conocemos el stock de los proveedores.*/

            }

            //Actualizamos el precio del pedido al precio real.
            pedido.Total = pedidoParaCrear.Total;

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            var detallePedido = new DetallePedidoDTO(pedido.Id, pedido.Direccion,
                pedidoParaCrear.LineasPedido,
                pedido.CodigoEmpleado,
                pedidoParaCrear.TipoMetodoPago,
                pedido.FechaPedido,
                pedido.Comentarios);

            return CreatedAtAction("GetPedido", new { id = pedido.Id }, detallePedido);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetallePedidoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetPedido(int id)
        {
            if (_context.Pedidos == null)
            {
                _logger.LogError("Error: No existe la tabla de pedidos.");
                return NotFound();
            }

            var pedidoDTO = await _context.Pedidos
             .Where(pedido => pedido.Id == id)
                 .Include(pedido => pedido.LineasPedido)
                    .ThenInclude(lineaPedido => lineaPedido.Portatil)
                        .ThenInclude(portatil => portatil.Marca)
             .Select(pedido => new DetallePedidoDTO(pedido.Id, pedido.Direccion,
                    pedido.LineasPedido
                        .Select(lp => new LineaPedidoDTO(lp.Portatil.Id,
                                lp.Portatil.Modelo,lp.PrecioUnitario,
                                lp.Cantidad)).ToList(),
                    pedido.CodigoEmpleado,
                    pedido.TipoMetodoPago, pedido.FechaPedido, pedido.Comentarios))
             .FirstOrDefaultAsync();


            if (pedidoDTO == null)
            {
                _logger.LogError($"Error: El pedido con {id} no existe.");
                return NotFound();
            }

            return Ok(pedidoDTO);
        }
    }
}
