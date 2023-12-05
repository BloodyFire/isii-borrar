using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using PortatilesAPI;

namespace OneHope.Web {
    // Esta clase se usa para mantener el estado de la aplicación.
    // Sirve por ejemplo, para mantener qué elementos ha seleccionado el usuario en cada momento.
    // Para poder usarlo hay que añadir como servicio en el Program.cs del proyecto Web:
    //       builder.Services.AddScoped<ReabastecerPortatilStateContainer>();

    public class ReabastecerPortatilStateContainer {
         
        public PedidoParaCrearDTO Pedido { get; 
                                            private set;  // El set es privado para evitar que código maligno pueda modificar la compra.
                                        } = new PedidoParaCrearDTO() {
            LineasPedido = new List<LineaPedidoDTO>()
        };

        
        
         // Se añade un elemento al carrito de la compra.
        public void AddPortatilAPedido(PortatilParaPedidoDTO portatil, int? cantidad) {
            // Se comprueba que ese portatile no se ha añadido ya al carrito.
            if (!Pedido.LineasPedido.Any(li => li.PortatilID == portatil.Id)) {
                int cant = (int)(cantidad == null ? 1 : cantidad);  // Si la cantidad es null lo añadimos con cantidad 1.
                // Si pides directamente en la interfaz de usuario la cantidad, el último párametro en vez de ser 1 sería la propia cantidad.
                Pedido.LineasPedido.Add(LineaPedido(portatil.Id, portatil.Modelo, portatil.PrecioCoste, cant));
            }
        }

        public void SetDireccion(string direccion) {
            Pedido.Direccion = direccion;
        }

        public void SetComentarios(string comentarios) { 
            Pedido.Comentarios = comentarios;
        }

        //TODO: THIS SHOULD BE DONE OUTSIDE THE FRONTEND
        public void SetCodigoEmpleado(string codEmpleado)
        {
            Pedido.CodigoEmpleado = codEmpleado;
        }

        // Se elimina un elemento del carrito de la compra.

        public void RemovePortatilAPedir(int portatilId) {
            LineaPedidoDTO lineaPedido =
                Pedido.LineasPedido.FirstOrDefault(li => li.PortatilID.Equals(portatilId));
            Pedido.LineasPedido.Remove(lineaPedido);
        }

        // Devuelve si el carrito está vacío o no.
        public bool isEmpty() {
            return Pedido.LineasPedido.Count == 0;
        }

        // Devuelve si el carrito incluye o no un portatil.
        public bool includes(int id) {
            return Pedido.LineasPedido.Any(li => li.PortatilID.Equals(id));
        }

        // Actualiza la cantidad que se quiere comprar del portatil cuyo id es 'id'.
        public void UpdateCarrito(int id, int cantidad) {
            LineaPedidoDTO? portatil = Pedido.LineasPedido.FirstOrDefault(li => li.PortatilID == id);

            if (portatil != null) portatil.Cantidad = cantidad;
        }

        // No sé por qué sólo se generar el constructor vacío para los DTOs, hay que investigar por qué.
        // De momento como solución he creado este método auxiliar para emular el constructor.
        public static LineaPedidoDTO LineaPedido(int portatilId, string modelo, double precio, int cantidad) {
            LineaPedidoDTO li = new LineaPedidoDTO();
            li.PortatilID = portatilId;
            li.Modelo = modelo;
            li.Cantidad = cantidad;
            li.PrecioUnitario = precio;

            return li;
        }

        // Se ha terminado la compra, así que hay que vaciar el carrito..
        public void FinalizarPedido() {
            Pedido = new PedidoParaCrearDTO() {
                LineasPedido = new List<LineaPedidoDTO>()
            };
        }
    }


}

