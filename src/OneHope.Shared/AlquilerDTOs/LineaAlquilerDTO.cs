using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.AlquilerDTOs
{
    public class LineaAlquilerDTO
    {
        public LineaAlquilerDTO( int portatilID, double precioAlquiler, int cantidad) 
        { 
            PortatilID = portatilID;
            PrecioAlquiler = precioAlquiler;
            Cantidad = cantidad;
        }

        [JsonPropertyName("PortatilID")]
        public int PortatilID { get; set; }

        [Display(Name = "Precio Alquiler")]
        [JsonPropertyName("PrecioAlquiler")]
        public double PrecioAlquiler { get; set; }

        [Required]
        [JsonPropertyName("Cantidad")]
        [Range(1, Double.MaxValue, ErrorMessage = "No se ha introducido una cantidad válida.")]
        public int Cantidad { get; set; }

        public override bool Equals(object obj)
        {
            return obj is LineaAlquilerDTO model &&
                PortatilID == model.PortatilID &&
                PrecioAlquiler == model.PrecioAlquiler &&
                Cantidad == model.Cantidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( PortatilID, PrecioAlquiler, Cantidad);
        }
    }
}
