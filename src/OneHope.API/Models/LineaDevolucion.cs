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
