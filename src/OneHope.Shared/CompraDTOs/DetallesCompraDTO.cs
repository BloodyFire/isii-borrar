using OneHope.Design.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.CompraDTOs
{
    public class DetallesCompraDTO: CompraPorCrearDTO
    { 
        public DetallesCompraDTO() { }

        public DetallesCompraDTO(int id, string nombreUsuario, string apellidosUsuario, 
            string direccion, IList<LineaCompraDTO> lineasCompra, TipoMetodoPago metodoPago,
            DateTime fechaCompra):
            base(direccion, lineasCompra, nombreUsuario, apellidosUsuario, metodoPago)
            {
                Id = id;
                FechaCompra = fechaCompra;

            }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fechaCompra")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetallesCompraDTO dTO &&
                   base.Equals(obj) &&
                   Id == dTO.Id &&
                   FechaCompra == dTO.FechaCompra;
        }
    }
}
