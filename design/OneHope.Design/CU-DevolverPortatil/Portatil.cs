using OneHope.Design.Models;
using System.ComponentModel.DataAnnotations;

namespace OneHope.API.Models
{
    public class Portatil
    {
        public Portatil()
        {
            LineasPedido = new List<LineaPedido>();
        }

        public Portatil(int id, string modelo, Procesador procesador, Ram ram, Marca marca, string nombre, double precioCompra, double precioAlquiler, double precioCoste, int stock, int stockAlquilar)
        {
            Id = id;
            Modelo = modelo;
            Procesador = procesador;
            Ram = ram;
            Marca = marca;
            Nombre = nombre;
            PrecioCompra = precioCompra;
            PrecioAlquiler = precioAlquiler;
            PrecioCoste = precioCoste;
            Stock = stock;
            StockAlquilar = stockAlquilar;
            LineasPedido = new List<LineaPedido>();
            ListaCompra = new List<LineaCompra>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El modelo del portatil no puede tener más de 50 characters.")]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El precio mínimo es de 1€.")]
        [Display(Name ="Precio De Compra")]
        public double PrecioCompra {  get; set; }

        [Required]
        [Display(Name = "Memoria RAM")]
        public Ram Ram {  get; set; }
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El precio mínimo es de 1€.")]
        [Display(Name = "Precio De Alquiler")]
        public double PrecioAlquiler { get; set; }

        [Required]
        public Marca Marca { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre del portatil no puede tener más de 100 characters.")]
        //it assigns a value by default
        public string Nombre { get; set; } = string.Empty;




        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio de coste mínimo es 1")]
        [Display(Name = "Precio de coste")]
        public double PrecioCoste { get; set; }
        public IList<LineaCompra> ListaCompra { get; set; }

        [Required]
        public Procesador Procesador { get; set; }
        [Display(Name = "Unidades disponibles")]
        public int Stock { get; set; } = 0;

        [Required]
        [Display(Name = "Unidades disponibles")]
        public int StockAlquilar { get; set; } = 0;

        public IList<LineaPedido> LineasPedido { get; set; } = new List<LineaPedido>();

        [Required]
        public Proveedor Proveedor { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Portatil portatil &&
                   Id == portatil.Id &&
                   Modelo == portatil.Modelo &&
                   EqualityComparer<Procesador>.Default.Equals(Procesador, portatil.Procesador) &&
                   EqualityComparer<Ram>.Default.Equals(Ram, portatil.Ram) &&
                   EqualityComparer<Marca>.Default.Equals(Marca, portatil.Marca) &&
                   Nombre == portatil.Nombre &&
                   PrecioCompra == portatil.PrecioCompra &&
                   PrecioAlquiler == portatil.PrecioAlquiler &&
                   PrecioCoste == portatil.PrecioCoste &&
                   Stock == portatil.Stock &&
                   StockAlquilar == portatil.StockAlquilar;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Modelo, Nombre);
        }
    }
}
