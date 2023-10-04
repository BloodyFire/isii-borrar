namespace OneHope.API.Models
{
    public class Portatil
    {
        public Portatil()
        {
        }

        public Portatil(int id, string modelo, Procesador proceador, RAM rAM, Marca marca, string nombre, double precioCompra, double precioAlquiler, double precioCoste, int stock, int stockAlquilar)
        {
            Id = id;
            Modelo = modelo;
            Proceador = proceador;
            RAM = rAM;
            Marca = marca;
            Nombre = nombre;
            PrecioCompra = precioCompra;
            PrecioAlquiler = precioAlquiler;
            PrecioCoste = precioCoste;
            Stock = stock;
            StockAlquilar = stockAlquilar;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El modelo del portatil no puede tener más de 50 characters.")]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public Procesador Proceador { get; set; }

        [Required]
        [Display(Name = "Memoria RAM")]
        public RAM RAM {  get; set; }

        [Required]
        public Marca Marca { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre del portatil no puede tener más de 100 characters.")]
        //it assigns a value by default
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio de compra mínimo es 1")]
        [Display(Name = "Precio de compra")]
        public double PrecioCompra { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio de alquiler mínimo es 1")]
        [Display(Name = "Precio de alquiler")]
        public double PrecioAlquiler { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio de coste mínimo es 1")]
        [Display(Name = "Precio de coste")]
        public double PrecioCoste { get; set; }

        [Required]
        [Display(Name = "Unidades disponibles")]
        public int Stock { get; set; } = 0;

        [Required]
        [Display(Name = "Unidades disponibles")]
        public int StockAlquilar { get; set; } = 0;

        public override bool Equals(object? obj)
        {
            return obj is Portatil portatil &&
                   Id == portatil.Id &&
                   Modelo == portatil.Modelo &&
                   EqualityComparer<Procesador>.Default.Equals(Proceador, portatil.Proceador) &&
                   EqualityComparer<RAM>.Default.Equals(RAM, portatil.RAM) &&
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
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Modelo);
            hash.Add(Proceador);
            hash.Add(RAM);
            hash.Add(Marca);
            hash.Add(Nombre);
            hash.Add(PrecioCompra);
            hash.Add(PrecioAlquiler);
            hash.Add(PrecioCoste);
            hash.Add(Stock);
            hash.Add(StockAlquilar);
            return hash.ToHashCode();
        }
    }
}
