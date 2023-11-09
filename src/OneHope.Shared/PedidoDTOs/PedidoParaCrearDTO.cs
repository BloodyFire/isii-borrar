using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OneHope.Shared.PedidoDTOs
{
    public class PedidoParaCrearDTO
    {
        public PedidoParaCrearDTO()
        {
            LineasPedido = new List<LineaPedidoDTO>();
        }

        public PedidoParaCrearDTO(string direccion, IList<LineaPedidoDTO> lineasPedido, string codigoEmpleado, TipoMetodoPago tipoMetodoPago, string? comentarios)
        {
            Direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
            LineasPedido = lineasPedido ?? throw new ArgumentNullException(nameof(lineasPedido));
            CodigoEmpleado = codigoEmpleado ?? throw new ArgumentNullException(nameof(codigoEmpleado));
            TipoMetodoPago = tipoMetodoPago;
            Comentarios = comentarios;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, indica una dirección de entrega.")]
        [Display(Name = "Dirección de entrega")]
        [JsonPropertyName("Direccion")]
        public string Direccion { get; set; }

        [ValidateComplexType]
        [JsonPropertyName("LineasPedido")]
        public IList<LineaPedidoDTO> LineasPedido { get; set; }

        [Display(Name = "Precio Total")]
        [JsonPropertyName("Total")]
        public double Total
        {
            get
            {
                return LineasPedido.Sum(pi => pi.Cantidad * pi.PrecioUnitario);
            }
        }

        [JsonPropertyName("CodigoEmpleado")]
        [Required]
        public string CodigoEmpleado { get; set; }

        [Required]
        [JsonPropertyName("TipoMetodoPago")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoMetodoPago TipoMetodoPago { get; set; }

        [JsonPropertyName("Comentarios")]
        [DataType(DataType.MultilineText)]
        public string? Comentarios { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PedidoParaCrearDTO dTO &&
                   Direccion == dTO.Direccion &&
                   LineasPedido.SequenceEqual(dTO.LineasPedido) &&
                   Total == dTO.Total &&
                   CodigoEmpleado == dTO.CodigoEmpleado &&
                   TipoMetodoPago == dTO.TipoMetodoPago;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Direccion, LineasPedido, Total, CodigoEmpleado, TipoMetodoPago);
        }
    }
}


