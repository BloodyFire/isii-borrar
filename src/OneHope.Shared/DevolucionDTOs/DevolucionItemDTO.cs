using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.DevolucionDTOs
{
    public class DevolucionItemDTO
    {
        public DevolucionItemDTO( int idPortatil,int cantidad, string modelo, int idCompra, int idLineaCompra, double precioUnitario)
        {
            IdPortatil = idPortatil;
            Cantidad = cantidad;
            Modelo = modelo;
            IdCompra = idCompra;
            IdLineaCompra = idLineaCompra;
            PrecioUnitario = precioUnitario;
        }


        [JsonPropertyName("IdPortatil")]
        public int IdPortatil { get; set; }

        [Required]
        [JsonPropertyName("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [JsonPropertyName("PrecioUnitario")]
        public double PrecioUnitario { get; set; }


        [StringLength(50, ErrorMessage = "El modelo del portatil no puede tener más de 50 characters.")]
        [JsonPropertyName("Modelo")]
        public string Modelo { get; set; } = string.Empty;

        [JsonPropertyName("IdCompra")]
        public int IdCompra { get; set; }

        [JsonPropertyName("IdLineaCompra")]
        public int IdLineaCompra { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DevolucionItemDTO dTO &&
                   IdPortatil == dTO.IdPortatil &&
                   Cantidad == dTO.Cantidad &&
                   PrecioUnitario == dTO.PrecioUnitario &&
                   Modelo == dTO.Modelo &&
                   IdCompra == dTO.IdCompra &&
                   IdLineaCompra == dTO.IdLineaCompra;
        }
    }
}
