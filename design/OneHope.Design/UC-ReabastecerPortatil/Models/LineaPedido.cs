using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.Design.Models
{
    public class LineaPedido
    {
        public LineaPedido()
        {
        }

        public LineaPedido(Portatil portatil, int cantidad, Pedido pedido, double precioUnitario)
        {
            Portatil = portatil;
            Cantidad = cantidad;
            PortatilId = portatil.Id;
            Pedido = pedido;
            PedidoId = pedido.Id;
            PrecioUnitario = precioUnitario;
        }

        [ForeignKey("PortatilId")]
        public Portatil Portatil { get; set; }

        public int PortatilId { get; set; }


        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        public int PedidoId { get; set; }

        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "La cantidad no puede ser inferior a 1")]
        public int Cantidad { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio unitario mínimo es 1")]
        [Display(Name = "Precio unitario")]
        public double PrecioUnitario { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is LineaPedido item &&
                   EqualityComparer<Portatil>.Default.Equals(Portatil, item.Portatil) &&
                   PortatilId == item.PortatilId &&
                   //No comprobamos el pedido por que generaríamos un bucle infinito.
                   PedidoId == item.PedidoId &&
                   Cantidad == item.Cantidad &&
                   PrecioUnitario == item.PrecioUnitario;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PortatilId, PedidoId, Cantidad, PrecioUnitario);
        }
    }
}