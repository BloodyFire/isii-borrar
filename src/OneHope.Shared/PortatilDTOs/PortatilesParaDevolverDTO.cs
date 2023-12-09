using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.PortatilDTOs
{
    public class PortatilesParaDevolverDTO
    {

        public PortatilesParaDevolverDTO() { 
        }

        public PortatilesParaDevolverDTO(int idCompra, int idPortatil, int idLineaCompra, string marca, string modelo, int cantidad, DateTime fechaCompra, double total)
        {
            IdCompra = idCompra;
            IdPortatil = idPortatil;
            IdLineaCompra = idLineaCompra;
            Marca = marca;
            Modelo = modelo;
            Cantidad = cantidad;
            FechaCompra = fechaCompra;
            Total = total;
        }


        [JsonPropertyName("IdCompra")]
        public int IdCompra { get; set; }
        [JsonPropertyName("IdPortatil")]
        public int IdPortatil { get; set; }
        [JsonPropertyName("IdLineaCompra")]
        public int IdLineaCompra { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La marca no puede ser superior a 50 caracteres.")]
        [JsonPropertyName("Marca")]
        public string Marca { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El modelo no puede ser superior a 50 caracteres.")]
        [JsonPropertyName("Modelo")]
        public string Modelo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debes introducir una cantidad válida.")]
        [JsonPropertyName("Cantidad")]
        public int Cantidad { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Compra")]
        [JsonPropertyName("FechaCompra")]
        public DateTime FechaCompra { get; set; }

        [Required]
        [JsonPropertyName("Total")]
        public double Total { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PortatilesParaDevolverDTO dTO &&
                   IdCompra == dTO.IdCompra &&
                   IdPortatil == dTO.IdPortatil &&
                   IdLineaCompra == dTO.IdLineaCompra &&
                   Marca == dTO.Marca &&
                   Cantidad == dTO.Cantidad &&
                   FechaCompra == dTO.FechaCompra &&
                   Total == dTO.Total;
        }
    }
}
