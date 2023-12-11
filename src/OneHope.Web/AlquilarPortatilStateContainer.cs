using PortatilesAPI;

namespace OneHope.Web
{
    // Esta clase se usa para mantener el estado de la aplicación.
    // Sirve por ejemplo, para mantener qué elementos ha seleccionado el usuario en cada momento.
    // Para poder usarlo hay que añadir como servicio en el Program.cs del proyecto Web:
    //       builder.Services.AddScoped<ComparArticuloStateContainer>();

    public class AlquilarPortatilStateContainer {
         
        public AlquilerParaCrearDTO Alquiler { get; 
                                            private set;  // El set es privado para evitar que código maligno pueda modificar la compra.
                                        } = new AlquilerParaCrearDTO() {
            LineasPedido = new List<LineaAlquilerDTO>()
        };

        
       
         // Se añade un elemento al carrito de la compra.
        public void AddArticuloACompra(PortatilParaAlquilerDTO portatil, int? cantidad) {
            // Se comprueba que ese artículo no se ha añadido ya al carrito.
            if (!Alquiler.LineasPedido.Any(li => li.PortatilID == portatil.Id)) {
                int cant = (int)(cantidad == null ? 1 : cantidad);  // Si la cantidad es null lo añadimos con cantidad 1.
                // Si pides directamente en la interfaz de usuario la cantidad, el último párametro en vez de ser 1 sería la propia cantidad.
                Alquiler.LineasPedido.Add(LineaAlquiler(0, cant, portatil, Alquiler));
            }
        }

        // No sé por qué sólo se generar el constructor vacío para los DTOs, hay que investigar por qué.
        // De momento como solución he creado este método auxiliar para emular el constructor.
        public static LineaAlquilerDTO LineaFactura(int portatilId, float precioAlquiler, int cantidad)
        {
            LineaAlquilerDTO li = new LineaAlquilerDTO();
            li.PortatilID = portatilId;
            li.PrecioAlquiler = precioAlquiler;
            li.Cantidad = cantidad;

            return li;
        }
        
        // Se elimina un elemento del carrito de la compra.

        public void RemoveArticuloACompra(int articuloId) {

            LineaAlquilerDTO lineaAlquiler =
                Alquiler.LineasPedido.FirstOrDefault(li => li.PortatilID.Equals(articuloId));
            Alquiler.LineasPedido.Remove(lineaAlquiler);
        }
        
        // Devuelve si el carrito está vacío o no.
        public bool isEmpty() {
            if (Alquiler.LineasPedido.Count == 0) return true;
            else return false;
        }

        // Devuelve si el carrito incluye o no un artículo.
        public bool includes(int id) {
            if (Alquiler.LineasPedido.Any(li => li.PortatilID.Equals(id))) { return true; }

            return false;
        }

        // Actualiza la cantidad que se quiere comprar del artículo cuyo id es 'id'.
        public void UpdateCarrito(int id, int cantidad) {
            LineaAlquilerDTO? portatil = Alquiler.LineasPedido.FirstOrDefault(li => li.PortatilID == id);

            if (portatil != null) portatil.Cantidad = cantidad;
        }

        
        
        // Se ha terminado la compra, así que hay que vaciar el carrito..
        public void FinalizarCompra() {
            Alquiler = new AlquilerParaCrearDTO() {
                LineasPedido = new List<LineaAlquilerDTO>()
            };
        }
        
    }
    

    }

