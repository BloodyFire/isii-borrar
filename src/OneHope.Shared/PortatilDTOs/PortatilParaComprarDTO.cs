using OneHope.Design.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace OneHope.Shared.PortatilDTOs
{
    public class PortatilParaComprarDTO
    {
        public PortatilParaComprarDTO() {}
        public PortatilParaComprarDTO(int id, string modelo, double precioCompra, string ram, string marca, string nombre, string procesador, int cantidad)
        {
            Id = id;
            Modelo = modelo ?? throw new ArgumentNullException(nameof(modelo));
            PrecioCompra = precioCompra;
            Ram = ram ?? throw new ArgumentNullException(nameof(ram));
            Marca = marca ?? throw new ArgumentNullException(nameof(marca));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Procesador = procesador ?? throw new ArgumentNullException(nameof(procesador));
            Stock = cantidad;
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
        public string Ram { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre del portatil no puede tener más de 100 characters.")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("procesador")]
        public string Procesador { get; set; }

        [Required]
        [Display(Name = "Stock")]
        [JsonPropertyName("stock")]
        public int Stock { get; set; } = 0;

        public override bool Equals(object? obj)
        {
            return obj is PortatilParaComprarDTO dTO &&
                   Id == dTO.Id &&
                   Modelo == dTO.Modelo &&
                   PrecioCompra == dTO.PrecioCompra &&
                   Ram == dTO.Ram &&
                   Marca == dTO.Marca &&
                   Nombre == dTO.Nombre &&
                   Procesador == dTO.Procesador &&
                   Stock == dTO.Stock;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre);
        }
    }
}