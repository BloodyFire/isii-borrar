using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.CompraDTOs
{
    public class LineaCompraDTO
    {
        public LineaCompraDTO(int portatilID, string nombre, double precioUnitario,
            string marca, string procesador, string ram, int cantidad)
        {
            PortatilID = portatilID;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            PrecioUnitario = precioUnitario;
            Marca = marca ?? throw new ArgumentNullException(nameof (marca));
            Procesador = procesador ?? throw new ArgumentNullException(nameof(procesador));
            Ram = ram ?? throw new ArgumentNullException(nameof(ram));
            Cantidad = cantidad;
        }

        [JsonPropertyName("portatilID")]
        public int PortatilID { get; set; }

        [StringLength(50, ErrorMessage ="El nombre del portátil no puede ser más de 50 caracteres.")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set;}

        [Display(Name = "Precio unitario de la Compra")]
        [JsonPropertyName("precioUnitario")]
        [Required]
        public double PrecioUnitario { get; set; }

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
            return obj is LineaCompraDTO dTO &&
                   PortatilID == dTO.PortatilID &&
                   Nombre == dTO.Nombre &&
                   PrecioUnitario == dTO.PrecioUnitario &&
                   Ram == dTO.Ram &&
                   Procesador == dTO.Procesador &&
                   Marca == dTO.Marca &&
                   Cantidad == dTO.Cantidad;
        }
    }
}
