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
        public DevolucionItemDTO(int idDevolucion, int idPortatil,int cantidad, string modelo, int idCompra)
        {
            IdPortatil = idPortatil;
            Cantidad = cantidad;
            Modelo = modelo;
            IdCompra = idCompra;
        }

        [JsonPropertyName("IdDevolucion")]
        public int IdDevolucion { get; set; }

        [JsonPropertyName("IdPortatil")]
        public int IdPortatil { get; set; }

        [Required]
        [JsonPropertyName("Cantidad")]
        public int Cantidad { get; set; }

        
        [StringLength(50, ErrorMessage = "El modelo del portatil no puede tener más de 50 characters.")]
        [JsonPropertyName("Modelo")]
        public string Modelo { get; set; } = string.Empty;

        [JsonPropertyName("IdCompra")]
        public int IdCompra {  get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DevolucionItemDTO dTO &&
                   IdPortatil == dTO.IdPortatil &&
                   Cantidad == dTO.Cantidad &&
                   Modelo == dTO.Modelo &&
                   IdCompra == dTO.IdCompra;
        }
    }
}
