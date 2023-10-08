using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class LineaDevolucion
    {

        public LineaDevolucion() { }

        [Key]
        public int IdLinea { get; set; }

        [Required]
        public int IdDevolucion { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public int IdLineaCompra { get; set; }

        [ForeignKey("PortatilId")]
        public Portatil Portatil { get; set; }
        public int PortatilId { get; set; }

        [ForeignKey("DevolucionId")]
        public Devolucion Devolucion { get; set; }
        public int PedidoId { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is LineaDevolucion devolucion &&
                   IdLineaCompra == devolucion.IdLineaCompra;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdLineaCompra);
        }
    }
}
