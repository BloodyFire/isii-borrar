namespace OneHope.API.Models
{
    public class Portatil
    {
        public Portatil() 
        { 
        }

        public Portatil(int id, string nombre, double precio_Compra, double precio_Alquiler, int cantidad_Compra, int cantidad_Alquiler, int id_Proveedor, Procesador procesador, Ram ram, Marca marca)
        {
            Id = id;
            Nombre = nombre;
            Precio_Compra = precio_Compra;
            Precio_Alquiler = precio_Alquiler;
            Cantidad_Compra = cantidad_Compra;
            Cantidad_Alquiler = cantidad_Alquiler;
            Id_Proveedor = id_Proveedor;
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
        public double Precio_Compra {  get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El precio mínimo es de 1€.")]
        [Display(Name = "Precio De Alquiler")]
        public double Precio_Alquiler { get; set; }

        [Required]
        [Display(Name = "Cantidad De Portátiles")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad mínima para comprar es 1 portátil.")]
        public int Cantidad_Compra {  get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1,100, ErrorMessage = "Puedes alquilar desde un portátil hasta 100.")]
        [Display(Name = "Portátiles A Alquilar")]
        public int Cantidad_Alquiler { get; set; }

        public int Id_Proveedor {  get; set; }

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
                   Precio_Compra == portatil.Precio_Compra &&
                   Precio_Alquiler == portatil.Precio_Alquiler &&
                   Cantidad_Compra == portatil.Cantidad_Compra &&
                   Cantidad_Alquiler == portatil.Cantidad_Alquiler &&
                   Id_Proveedor == portatil.Id_Proveedor &&
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
