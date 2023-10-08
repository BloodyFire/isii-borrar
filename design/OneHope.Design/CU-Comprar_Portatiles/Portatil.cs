namespace OneHope.API.Models
{
    public class Portatil
    {
        public Portatil() 
        { 
        }

        public Portatil(int id, string nombre, double precioCompra, double precioAlquiler, int cantidadCompra, int cantidadAlquiler, int idProveedor, Procesador procesador, Ram ram, Marca marca)
        {
            Id = id;
            Nombre = nombre;
            PrecioCompra = precioCompra;
            PrecioAlquiler = precioAlquiler;
            CantidadCompra = cantidadCompra;
            CantidadAlquiler = cantidadAlquiler;
            IdProveedor = idProveedor;
            Procesador = procesador;
            Ram = ram;
            Marca = marca;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre ha de ser de 50 carácteres o menos.")]
        public string Nombre {  get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El precio mínimo es de 1€.")]
        [Display(Name ="Precio De Compra")]
        public double PrecioCompra {  get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El precio mínimo es de 1€.")]
        [Display(Name = "Precio De Alquiler")]
        public double PrecioAlquiler { get; set; }

        [Required]
        [Display(Name = "Cantidad De Portátiles")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad mínima para comprar es 1 portátil.")]
        public int CantidadCompra {  get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1,100, ErrorMessage = "Puedes alquilar desde un portátil hasta 100.")]
        [Display(Name = "Portátiles A Alquilar")]
        public int CantidadAlquiler { get; set; }

        public int IdProveedor {  get; set; }

        public IList<LineaCompra> ListaCompra { get; set; }

        [Required]
        public Procesador Procesador { get; set; }
        [Required]
        public Ram Ram { get; set; }
        [Required]
        public Marca Marca { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Portatil portatil &&
                   Id == portatil.Id &&
                   Nombre == portatil.Nombre &&
                   PrecioCompra == portatil.PrecioCompra &&
                   PrecioAlquiler == portatil.PrecioAlquiler &&
                   CantidadCompra == portatil.CantidadCompra &&
                   CantidadAlquiler == portatil.CantidadAlquiler &&
                   IdProveedor == portatil.IdProveedor &&
                   EqualityComparer<Procesador>.Default.Equals(Procesador, portatil.Procesador) &&
                   EqualityComparer<Ram>.Default.Equals(Ram, portatil.Ram) &&
                   EqualityComparer<Marca>.Default.Equals(Marca, portatil.Marca);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre);
        }
    }
}
