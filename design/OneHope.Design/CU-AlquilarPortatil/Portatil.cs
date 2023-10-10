using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class Portatil
    {
        //Atributos
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "El modelo del portatil no puede tener mas de 75 caracteres")]
        public string Modelo { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio de compra minimo es 1")]
        [Display(Name = "Precio de compra")]
        public float PrecioCompra { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio de alquiler minimo es 1")]
        [Display(Name = "Precio de alquiler")]
        public float PrecioAlq { get; set;}

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio de compra a proveedor minimo es 1")]
        [Display(Name = "Precio de compra a proveedor")]
        public float PrecioCoste { get; set; }

        [Required]
        [Display(Name = "Unidades disponibles para comprar")]
        public int Stock {  get; set; }

        [Required]
        [Display(Name = "Unidades disponibles para alquilar")]
        public int StockAlq { get; set; }

        [Required]
        public Procesador Procesador { get; set; }

        [Required]
        public Marca Marca { get; set; }

        [Required]
        [Display(Name = "Memoria RAM")]
        public RAM RAM { get; set; }

        public IList<LineaAlquiler> LineasAlquiler { get; set; }

        //Constructores
        public Portatil() 
        {
            LineasAlquiler = new List<LineaAlquiler>();
        }

        public Portatil(int iD, IList<LineaAlquiler> lineasAlquiler, string modelo, float precioCompra, float precioAlq, float precioCoste, int stock, int stockAlq, Procesador procesador, RAM rAM, Marca marca)
        {
            ID = iD;
            LineasAlquiler = lineasAlquiler;
            Modelo = modelo;
            PrecioCompra = precioCompra;
            PrecioAlq = precioAlq;
            PrecioCoste = precioCoste;
            Stock = stock;
            StockAlq = stockAlq;
            Procesador = procesador;
            Marca = marca;
            RAM = rAM;
        }

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is Portatil port &&
                   ID == port.ID &&
                   Modelo == port.Modelo &&
                   EqualityComparer<Procesador>.Default.Equals(Procesador, port.Procesador) &&
                   EqualityComparer<RAM>.Default.Equals(RAM, port.RAM) &&
                   EqualityComparer<Marca>.Default.Equals(Marca, port.Marca) &&
                   PrecioCompra == port.PrecioCompra &&
                   PrecioAlq == port.PrecioAlq &&
                   PrecioCoste == port.PrecioCoste &&
                   Stock == port.Stock &&
                   StockAlq == port.StockAlq;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(ID);
            hash.Add(Modelo);
            hash.Add(Procesador);
            hash.Add(RAM);
            hash.Add(Marca);
            hash.Add(PrecioCompra);
            hash.Add(PrecioAlq);
            hash.Add(PrecioCoste);
            hash.Add(Stock);
            hash.Add(StockAlq);
            return hash.ToHashCode();
        }
    }
}
