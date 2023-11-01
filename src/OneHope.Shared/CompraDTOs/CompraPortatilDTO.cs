using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.CompraDTOs
{
    public class CompraPortatilDTO
    {
        public CompraPortatilDTO(int portatilID, string nombre, double precioCompra,
            string marca, string procesador, string ram, int cantidad)
        {
            portatilID = portatilID;
            nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            precioCompra = precioCompra;
            marca = marca ?? throw new ArgumentNullException(nameof (marca));
            procesador = procesador ?? throw new ArgumentNullException(nameof(procesador));
            ram = ram ?? throw new ArgumentNullException(nameof(ram));
            cantidad = cantidad;
        }

        [JsonPropertyName("portatilID")]
        public int PortatilID { get; set; }

        [StringLength(50, ErrorMessage ="El nombre del portátil no puede ser más de 50 caracteres.")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set;}

        [Display(Name = "Precio de la Compra")]
        [JsonPropertyName("precioDeLaCompra")]
        public double precioCompra { get; set; }

        [JsonPropertyName("ram")]
        public string Ram { get; set; }

        [JsonPropertyName("procesador")]
        public string Procesador { get; set; }

        [JsonPropertyName("marca")]
        public string Marca { get; set; }

        [Required]
        [JsonPropertyName("Cantidad")]
        [Range(1, Double.MaxValue, ErrorMessage ="Debes de indicar una cantidad válida.")]
        public int Cantidad { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CompraPortatilDTO dTO &&
                   PortatilID == dTO.PortatilID &&
                   Nombre == dTO.Nombre &&
                   precioCompra == dTO.precioCompra &&
                   Ram == dTO.Ram &&
                   Procesador == dTO.Procesador &&
                   Marca == dTO.Marca &&
                   Cantidad == dTO.Cantidad;
        }
    }
}
