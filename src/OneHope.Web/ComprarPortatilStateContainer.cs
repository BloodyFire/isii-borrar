using Microsoft.AspNetCore.Components;
using PortatilesAPI;

namespace OneHope.Web {
    // Esta clase se usa para mantener el estado de la aplicación.
    // Sirve por ejemplo, para mantener qué elementos ha seleccionado el usuario en cada momento.
    // Para poder usarlo hay que añadir como servicio en el Program.cs del proyecto Web:
    //       builder.Services.AddScoped<ComparArticuloStateContainer>();

    public class ComprarPortatilStateContainer {
         
        public CompraPorCrearDTO Compra { get; 
                                            private set;  // El set es privado para evitar que código maligno pueda modificar la compra.
                                        } = new CompraPorCrearDTO() {
            LineaCompra = new List<LineaCompraDTO>()
            
        };

        
        
         // Se añade un elemento al carrito de la compra.
        public void AddPortatilACompra(PortatilParaComprarDTO portatil, int? cantidad) {
            // Se comprueba que ese artículo no se ha añadido ya al carrito.
            if (!Compra.LineaCompra.Any(li => li.PortatilID == portatil.Id)) {
                int cant = (int)(cantidad == null ? 1 : cantidad);  // Si la cantidad es null lo añadimos con cantidad 1.
                // Si pides directamente en la interfaz de usuario la cantidad, el último párametro en vez de ser 1 sería la propia cantidad.
                Compra.LineaCompra.Add(LineaCompra(portatil.Id, portatil.Nombre, portatil.PrecioCompra, portatil.Marca, portatil.Procesador, portatil.Ram, portatil.Stock));
            }
        }

        //Añade/elimina el artículo al carrito al pulsar en la casilla. *@
        private void ToggleArticulo(PortatilParaComprarDTO portatil, ChangeEventArgs args)
            {
                bool _checked = (bool)args.Value;
                if (_checked)
                    AddPortatilACompra(portatil, 1);
                else
                    RemovePortatilACompra(portatil.Id);
            }

            // No sé por qué sólo se generar el constructor vacío para los DTOs, hay que investigar por qué.
            // De momento como solución he creado este método auxiliar para emular el constructor.
            public static LineaCompraDTO LineaCompra(int portatilID, string nombre, double precioUnitario,
                string marca, string procesador, string ram, int cantidad)
            {
                LineaCompraDTO li = new LineaCompraDTO();
                li.PortatilID = portatilID;
                li.Nombre = nombre;
                li.PrecioUnitario = precioUnitario;
                li.Marca = marca;
                li.Procesador = procesador;
                li.Ram = ram;
                li.Cantidad = cantidad;

                return li;
            }
        
            // Se elimina un elemento del carrito de la compra.

            public void RemovePortatilACompra(int portatilId) {

                LineaCompraDTO lineaFactura =
                    Compra.LineaCompra.FirstOrDefault(li => li.PortatilID.Equals(portatilId));
                Compra.LineaCompra.Remove(lineaFactura);
            }

        
            // Devuelve si el carrito está vacío o no.
            public bool isEmpty() {
                if (Compra.LineaCompra.Count == 0) return true;
                else return false;
            }

        
            // Devuelve si el carrito incluye o no un artículo.
            public bool includes(int id) {
                if (Compra.LineaCompra.Any(li => li.PortatilID.Equals(id))) { return true; }

                return false;
            }
        
            // Actualiza la cantidad que se quiere comprar del artículo cuyo id es 'id'.
            public void UpdateCarrito(int id, int cantidad) {
                LineaCompraDTO? portatil = Compra.LineaCompra.FirstOrDefault(li => li.PortatilID == id);

                if (portatil != null) portatil.Cantidad = cantidad;
            }
        
            // Se ha terminado la compra, así que hay que vaciar el carrito..
            public void FinalizarCompra() {
                Compra = new CompraPorCrearDTO() {
                    LineaCompra = new List<LineaCompraDTO>()
                };
            }
    }


}

