using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.PortatilesDTOs
{
    public class PortatilesParaDevolverDTO
    {

        public PortatilesParaDevolverDTO() { 
        }

        public PortatilesParaDevolverDTO(int id, string marca, int cantidad, DateTime fechaCompra, int total)
        {
            Id = id;
            Marca = marca;
            Cantidad = cantidad;
            FechaCompra = fechaCompra;
            Total = total;
        }


        [JsonPropertyName("IdCompra")]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La marca no puede ser superior a 50 caracteres.")]
        [JsonPropertyName("Marca")]
        public string Marca { get; set; }

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
        public int Total { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PortatilesParaDevolverDTO dTO &&
                   Id == dTO.Id &&
                   Marca == dTO.Marca &&
                   Cantidad == dTO.Cantidad &&
                   FechaCompra == dTO.FechaCompra &&
                   Total == dTO.Total;
        }
    }
}
