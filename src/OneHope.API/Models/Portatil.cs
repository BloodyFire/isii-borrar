using Microsoft.AspNetCore.Routing.Constraints;


namespace OneHope.API.Models
{
    public class Portatil
    {

        public Portatil() {
            LineasDevolucion = new List<LineaDevolucion>();
        }

        [Key]
        public int ID { get; set; }

        [Required, StringLength(20, ErrorMessage = "Name of Genre cannot be longer than 20 characters", MinimumLength =1)]
        public string Modelo { get; set; }

        [Required]
        public float PrecioCompra { get; set; }

        [Required]
        public float PrecioAlquiler { get; set; }

        [Required]
        public int PrecioCoste { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int StockAlquiler { get; set; }

        public IList<LineaDevolucion> LineasDevolucion { get; set; }

        public IList<LineaCompra> LineasCompra { get; set; }

        [Required]
        public Ram Ram { get; set; }

        [Required]
        public Procesador Procesador { get; set; }

        [Required]
        public Marca Marca { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is Portatil portatil &&
                   ID == portatil.ID &&
                   Modelo == portatil.Modelo &&
                   PrecioCompra == portatil.PrecioCompra &&
                   PrecioAlquiler == portatil.PrecioAlquiler &&
                   PrecioCoste == portatil.PrecioCoste &&
                   Stock == portatil.Stock &&
                   StockAlquiler == portatil.StockAlquiler;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Modelo, PrecioCompra, PrecioAlquiler, PrecioCoste, Stock, StockAlquiler);
        }
    }
}
