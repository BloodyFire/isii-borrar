using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class LineaCompra
    {
        public LineaCompra() { }

        public LineaCompra(int idLinea, int idPortatil, int idCompra, int cantidad, double precioUnitario)
        {
            IdLinea = idLinea;
            IdPortatil = idPortatil;
            IdCompra = idCompra;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
        public LineaCompra(int idLinea, Portatil portatil, Compra compra, int cantidad, double precioUnitario)
        {
            IdLinea = idLinea;
            Portatil = portatil;
            IdPortatil = portatil.Id;
            Compra = compra;
            IdCompra = compra.Id;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        [Required]
        [ForeignKey("IdPortatil")]
        public Portatil Portatil { get; set; }

        public int IdPortatil {  get; set; }

        [Required]
        [ForeignKey("IdCompra")]
        public Compra Compra { get; set; }

        public int IdCompra { get; set; }

        [Key]
        public int IdLinea {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes introducir una cantidad válida.")]
        public int Cantidad {  get; set; }

        public double PrecioUnitario {  get; set; }


        public IList<LineaDevolucion>? LineaDevolucion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is LineaCompra compra &&
                   EqualityComparer<Portatil>.Default.Equals(Portatil, compra.Portatil) &&
                   IdPortatil == compra.IdPortatil &&
                   IdLinea == compra.IdLinea &&
                   Cantidad == compra.Cantidad &&
                   PrecioUnitario == compra.PrecioUnitario;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdPortatil, IdLinea);
        }
    }
}
