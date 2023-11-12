using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.PedidoDTOs
{
    public class DetallePedidoDTO : PedidoParaCrearDTO
    {
        public DetallePedidoDTO() { }

        public DetallePedidoDTO(int id, string direccion, IList<LineaPedidoDTO> lineasPedido, string codigoEmpleado, TipoMetodoPago tipoMetodoPago, DateTime fechaPedido, string comentarios = "") :
            base(direccion, lineasPedido, codigoEmpleado, tipoMetodoPago, comentarios)
        {
            Id = id;
            FechaPedido = fechaPedido;
        }

        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Display(Name = "Fecha de pedido")]
        [JsonPropertyName("FechaPedido")]
        public DateTime FechaPedido { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetallePedidoDTO dTO &&
                   base.Equals(obj) &&
                   (FechaPedido.Subtract(dTO.FechaPedido) < new TimeSpan(0, 1, 0)) &&
                   Id == dTO.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Direccion, LineasPedido, Total, CodigoEmpleado, TipoMetodoPago, Id, FechaPedido);
        }
    }
}
