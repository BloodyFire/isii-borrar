using OneHope.Design.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace OneHope.Shared.PortatilDTOs
{
    public class PortatilParaComprarDTO
    {
        public PortatilParaComprarDTO(int id, string modelo, double precioCompra, Ram ram, Marca marca, string nombre, Procesador procesador, int stock)
        {
            Id = id;
            Modelo = modelo ?? throw new ArgumentNullException(nameof(modelo));
            PrecioCompra = precioCompra;
            Ram = ram ?? throw new ArgumentNullException(nameof(ram));
            Marca = marca ?? throw new ArgumentNullException(nameof(marca));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Procesador = procesador ?? throw new ArgumentNullException(nameof(procesador));
            Stock = stock;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El modelo del portatil no puede tener más de 50 characters.")]
        [JsonPropertyName("modelo")]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "El precio mínimo es de 1€.")]
        [Display(Name = "Precio De Compra")]
        [JsonPropertyName("precioCompra")]
        public double PrecioCompra { get; set; }

        [Required]
        [Display(Name = "Memoria RAM")]
        [JsonPropertyName("ram")]
        public Ram Ram { get; set; }

        [Required]
        public Marca Marca { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre del portatil no puede tener más de 100 characters.")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("procesador")]
        public Procesador Procesador { get; set; }

        [Display(Name = "Unidades disponibles")]
        [JsonPropertyName("stock")]
        public int Stock { get; set; } = 0;

        public override bool Equals(object? obj)
        {
            return obj is PortatilParaComprarDTO dTO &&
                   Id == dTO.Id &&
                   Modelo == dTO.Modelo &&
                   PrecioCompra == dTO.PrecioCompra &&
                   EqualityComparer<Ram>.Default.Equals(Ram, dTO.Ram) &&
                   EqualityComparer<Marca>.Default.Equals(Marca, dTO.Marca) &&
                   Nombre == dTO.Nombre &&
                   EqualityComparer<Procesador>.Default.Equals(Procesador, dTO.Procesador) &&
                   Stock == dTO.Stock;
        }
    }
}