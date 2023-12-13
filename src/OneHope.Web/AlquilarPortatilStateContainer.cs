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
            LineasAlquiler = new List<LineaAlquilerDTO>()
        };

        public AlquilarPortatilStateContainer()
        {
            Alquiler.EmailCliente = "";
            Alquiler.NombreCliente = "";
            Alquiler.ApellidosCliente = "";
            Alquiler.Direccion = "";
        }
       
         // Se añade un elemento al carrito de la compra.
        public void AddArticuloAAlquiler(PortatilParaAlquilerDTO portatil, int? cantidad) {
            // Se comprueba que ese artículo no se ha añadido ya al carrito.
            if (!Alquiler.LineasAlquiler.Any(li => li.PortatilID == portatil.Id)) {
                int cant = (int)(cantidad == null ? 1 : cantidad);  // Si la cantidad es null lo añadimos con cantidad 1.
                // Si pides directamente en la interfaz de usuario la cantidad, el último párametro en vez de ser 1 sería la propia cantidad.
                Alquiler.LineasAlquiler.Add(LineaAlquiler(portatil.Id, portatil.PrecioAlquiler, cant, portatil.Marca, portatil.Modelo,
                    portatil.Procesador, portatil.Ram));
            }
        }


        // No sé por qué sólo se generar el constructor vacío para los DTOs, hay que investigar por qué.
        // De momento como solución he creado este método auxiliar para emular el constructor.
        public static LineaAlquilerDTO LineaAlquiler(int portatilId, double precioAlquiler, int cantidad, string marca, string modelo, string procesador, string ram)
        {
            LineaAlquilerDTO li = new LineaAlquilerDTO();
            li.PortatilID = portatilId;
            li.PrecioAlquiler = precioAlquiler;
            li.Cantidad = cantidad;
            li.Marca = marca;
            li.Modelo = modelo;
            li.Procesador = procesador;
            li.Ram = ram;

            return li;
        }
        
        // Se elimina un elemento del carrito de la compra.

        public void RemoveArticuloAAlquiler(int articuloId) {

            LineaAlquilerDTO lineaAlquiler =
                Alquiler.LineasAlquiler.FirstOrDefault(li => li.PortatilID.Equals(articuloId));
            Alquiler.LineasAlquiler.Remove(lineaAlquiler);
        }
        
        // Devuelve si el carrito está vacío o no.
        public bool isEmpty() {
            if (Alquiler.LineasAlquiler.Count == 0) return true;
            else return false;
        }

        // Devuelve si el carrito incluye o no un artículo.
        public bool includes(int id) {
            if (Alquiler.LineasAlquiler.Any(li => li.PortatilID.Equals(id))) { return true; }

            return false;
        }

        // Actualiza la cantidad que se quiere comprar del artículo cuyo id es 'id'.
        public void UpdateCarrito(int id, int cantidad) {
            LineaAlquilerDTO? portatil = Alquiler.LineasAlquiler.FirstOrDefault(li => li.PortatilID == id);

            if (portatil != null) portatil.Cantidad = cantidad;
        }

        public void SetNombreCliente(string nombreCliente)
        {
            Alquiler.NombreCliente = nombreCliente;
        }

        public void SetApellidosCliente(string apellidosCliente)
        {
            Alquiler.ApellidosCliente = apellidosCliente;
        }

        public void SetEmailCliente(string emailCliente)
        {
            Alquiler.EmailCliente = emailCliente;
        }

        public void SetDireccionCliente(string direccionCliente)
        {
            Alquiler.Direccion = direccionCliente;
        }

        public void SetTelefonoCliente(int telefono)
        {
            Alquiler.TelefonoCliente = telefono;
        }

        // Se ha terminado la compra, así que hay que vaciar el carrito..
        public void FinalizarAlquiler() {
            Alquiler = new AlquilerParaCrearDTO() {
                LineasAlquiler = new List<LineaAlquilerDTO>()
            };
        }
        
    }
    

    }

