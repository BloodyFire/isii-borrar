using OneHope.Design.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.CompraDTOs
{
    public class DetallesCompraDTO : CompraPorCrearDTO
    {
        public DetallesCompraDTO() { }

        public DetallesCompraDTO(int id, string nombreUsuario, string apellidosUsuario, 
            string direccion, IList<CompraPortatilDTO> portatilesComprados, TipoMetodoPago metodoPago,
            DateTime fechaCompra) : 
            base(direccion, portatilesComprados, nombreUsuario, apellidosUsuario, metodoPago)
            {
                id = id;
                fechaCompra = fechaCompra;
            }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fechaCompra")]
        public DateTime FechaCompra { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetallesCompraDTO dTO &&
                   base.Equals(obj) &&
                   direccion == dTO.direccion &&
                   EqualityComparer<IList<CompraPortatilDTO>>.Default.Equals(compraPortatiles, dTO.compraPortatiles) &&
                   PrecioTotal == dTO.PrecioTotal &&
                   nombreUsuario == dTO.nombreUsuario &&
                   apellidosUsuario == dTO.apellidosUsuario &&
                   MetodoPago == dTO.MetodoPago &&
                   Id == dTO.Id &&
                   FechaCompra == dTO.FechaCompra;
        }
    }
}
