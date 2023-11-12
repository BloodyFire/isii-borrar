using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.PedidoDTOs
{
    public class LineaPedidoDTO
    {
        public LineaPedidoDTO(int portatilId, string modelo, double precioUnitario, int cantidad)
        {
            PortatilID = portatilId;
            Modelo = modelo ?? throw new ArgumentNullException(nameof(modelo));
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
        }

        [JsonPropertyName("PortatilID")]
        public int PortatilID { get; set; }


        [StringLength(50, ErrorMessage = "El modelo del portatil no puede tener más de 50 characters.")]
        [JsonPropertyName("Modelo")]
        public string Modelo { get; set; }


        [Display(Name = "Precio Unitario")]
        [JsonPropertyName("PrecioUnitario")]
        public double PrecioUnitario { get; set; }

        [Required]
        [JsonPropertyName("Cantidad")]
        [Range(1, Double.MaxValue, ErrorMessage = "No se ha introducido una cantidad válida.")]
        public int Cantidad { get; set; }

        public override bool Equals(object obj)
        {
            return obj is LineaPedidoDTO model &&
                PortatilID == model.PortatilID &&
                PrecioUnitario == model.PrecioUnitario &&
                Cantidad == model.Cantidad &&
                Modelo == model.Modelo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PortatilID, Modelo, PrecioUnitario, Cantidad);
        }
    }

}

