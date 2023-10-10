
using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class Portatil
    {

        public Portatil() {
            LineasDevolucion = new List<LineaDevolucion>();
            LineasCompra = new List<LineaCompra>();
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
                   StockAlquiler == portatil.StockAlquiler &&
                   EqualityComparer<Ram>.Default.Equals(Ram, portatil.Ram) &&
                   EqualityComparer<Procesador>.Default.Equals(Procesador, portatil.Procesador) &&
                   EqualityComparer<Marca>.Default.Equals(Marca, portatil.Marca);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(ID);
            hash.Add(Modelo);
            hash.Add(PrecioCompra);
            hash.Add(PrecioAlquiler);
            hash.Add(PrecioCoste);
            hash.Add(Stock);
            hash.Add(StockAlquiler);
            hash.Add(Ram);
            hash.Add(Procesador);
            hash.Add(Marca);
            return hash.ToHashCode();
        }
    }
}
