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
        public LineaAlquilerDTO( int portatilID, double precioAlquiler, int cantidad, string marca, string modelo, string procesador, string ram) 
        { 
            PortatilID = portatilID;
            PrecioAlquiler = precioAlquiler;
            Cantidad = cantidad;
            Marca = marca;
            Modelo = modelo;
            Procesador = procesador;
            Ram = ram;
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

        [Display(Name = "Marca")]
        [JsonPropertyName("Marca")]
        public string Marca { get; set; }

        [Display(Name = "Modelo")]
        [JsonPropertyName("Modelo")]
        public string Modelo { get; set; }

        [Display(Name = "Procesador")]
        [JsonPropertyName("Procesador")]
        public string Procesador { get; set; }

        [Display(Name = "Memoria RAM")]
        [JsonPropertyName("Ram")]
        public string Ram { get; set; }

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
