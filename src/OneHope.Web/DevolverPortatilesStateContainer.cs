
using PortatilesAPI;

namespace OneHope.Web {
    // Esta clase se usa para mantener el estado de la aplicación.
    // Sirve por ejemplo, para mantener qué elementos ha seleccionado el usuario en cada momento.
    // Para poder usarlo hay que añadir como servicio en el Program.cs del proyecto Web:
    //       builder.Services.AddScoped<ComparArticuloStateContainer>();

    public class DevolverPortatilesStateContainer {
         
        public DevolucionForCreateDTO Devolucion { get; 
                                            private set;  // El set es privado para evitar que código maligno pueda modificar la devolucion.
                                        } = new DevolucionForCreateDTO() {
            LineasDevoluciones = new List<DevolucionItemDTO>()
        };

        public DevolverPortatilesStateContainer()
        {
            Devolucion.MotivoDevolucion = "";
            Devolucion.DireccionRecogida = "";
        }
       
        
         // Se añade un elemento al carrito de la compra.
        public void AddPortatilADevolver(PortatilesParaDevolverDTO portatil, int? cantidad) {
            // Se comprueba que ese artículo no se ha añadido ya al carrito.
            if (!Devolucion.LineasDevoluciones.Any(li => li.IdCompra == portatil.IdCompra)) {
                int cant = (int)(cantidad == null ? 1 : cantidad);  // Si la cantidad es null lo añadimos con cantidad 1.
                // Si pides directamente en la interfaz de usuario la cantidad, el último párametro en vez de ser 1 sería la propia cantidad.
                Devolucion.LineasDevoluciones.Add(LineaDevolucion( portatil.IdPortatil, cant, portatil.Modelo, portatil.IdCompra, portatil.IdLineaCompra, portatil.Total));
            }
        }

        // No sé por qué sólo se generar el constructor vacío para los DTOs, hay que investigar por qué.
        // De momento como solución he creado este método auxiliar para emular el constructor.

        public static DevolucionItemDTO LineaDevolucion(int idPortatil, int cantidad, string modelo, int idCompra, int idLineaCompra, double precioUnitario)
        {
            DevolucionItemDTO li = new DevolucionItemDTO();
            li.IdPortatil = idPortatil;
            li.Cantidad = cantidad;
            li.Modelo = modelo;
            li.IdCompra = idCompra;
            li.IdLineaCompra = idLineaCompra;
            li.PrecioUnitario = precioUnitario;

            return li;
        }


        public void SetDireccionRecogida(string direccionRecogida)
        {
            Devolucion.DireccionRecogida = direccionRecogida;
        }

        public void SetMotivoDevolucion(string motivoDevolucion)
        {
            Devolucion.MotivoDevolucion = motivoDevolucion;
        }

        public void SetNotaRepartidor(string notaRepartidor)
        {
            Devolucion.NotaRepartidor = notaRepartidor;
        }

        // Se elimina un elemento del carrito de la devolucion.

        public void RemovePortatilADevolver(int portatilId) {

            DevolucionItemDTO lineaDevolucion =
                  Devolucion.LineasDevoluciones.FirstOrDefault(li => li.IdPortatil.Equals(portatilId));
                  Devolucion.LineasDevoluciones.Remove(lineaDevolucion);
        }

                // Devuelve si el carrito está vacío o no.
        public bool isEmpty() {
             if (Devolucion.LineasDevoluciones.Count == 0) return true;
             else return false;
        }

       
        // Devuelve si el carrito incluye o no un portátil.
        public bool includes(int id) {
              if (Devolucion.LineasDevoluciones.Any(li => li.IdPortatil.Equals(id))) return true; 
              else return false;
        }

                // Actualiza la cantidad que se quiere comprar del portátil cuyo id es 'id'.
         public void UpdateCarrito(int id, int cantidad) {
               DevolucionItemDTO? portatil = Devolucion.LineasDevoluciones.FirstOrDefault(li => li.IdPortatil == id);

               if (portatil != null) portatil.Cantidad = cantidad;
         }

                // No sé por qué sólo se generar el constructor vacío para los DTOs, hay que investigar por qué.
                // De momento como solución he creado este método auxiliar para emular el constructor.
                
        // Se ha terminado la devolución, así que hay que vaciar el carrito..
        public void FinalizarDevolver() {
                    Devolucion = new DevolucionForCreateDTO() {
                    LineasDevoluciones = new List<DevolucionItemDTO>()
                    };
        }
    }
             

    }

