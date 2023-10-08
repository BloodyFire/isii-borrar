using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class LineaCompra
    {
        public LineaCompra() { }

        public LineaCompra(int idLinea, int idProd, int idCompra, int cantidad, double precioUnitario)
        {
            IdLinea = idLinea;
            IdProd = idProd;
            IdCompra = idCompra;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        [Required]
        public Portatil Portatil { get; set; }

        public int IdProd {  get; set; }

        [Required]
        public Compra Compra { get; set; }

        public int IdCompra { get; set; }

        [Key]
        public int IdLinea {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes introducir una cantidad válida.")]
        public int Cantidad {  get; set; }

        public double PrecioUnitario {  get; set; }

        public List<LineaCompra> Lista_Compra { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is LineaCompra compra &&
                   EqualityComparer<Portatil>.Default.Equals(Portatil, compra.Portatil) &&
                   IdProd == compra.IdProd &&
                   IdLinea == compra.IdLinea &&
                   Cantidad == compra.Cantidad &&
                   PrecioUnitario == compra.PrecioUnitario;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdProd, IdLinea);
        }
    }
}
