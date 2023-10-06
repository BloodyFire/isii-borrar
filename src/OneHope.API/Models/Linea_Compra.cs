using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class Linea_Compra
    {
        public Linea_Compra() { }

        public Linea_Compra(int id_Linea, int id_Prod, int id_Compra, int cantidad, double precio_Unitario)
        {
            Id_Linea = id_Linea;
            Id_Prod = id_Prod;
            Id_Compra = id_Compra;
            Cantidad = cantidad;
            Precio_Unitario = precio_Unitario;
        }

        [ForeignKey("Id_Prod")]
        public Portatil Portatil { get; set; }

        public int Id_Prod { get; set; }

        [ForeignKey("Id_Compra")]
        public Compra Compra { get; set; }

        public int Id_Compra { get; set; }

        [Key]
        public int Id_Linea {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes introducir una cantidad válida.")]
        public int Cantidad {  get; set; }

        public double Precio_Unitario {  get; set; }

        public List<Linea_Compra> Lista_Compra { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Linea_Compra compra &&
                   EqualityComparer<Portatil>.Default.Equals(Portatil, compra.Portatil) &&
                   Id_Prod == compra.Id_Prod &&
                   EqualityComparer<Compra>.Default.Equals(Compra, compra.Compra) &&
                   Id_Compra == compra.Id_Compra &&
                   Id_Linea == compra.Id_Linea &&
                   Cantidad == compra.Cantidad &&
                   Precio_Unitario == compra.Precio_Unitario &&
                   EqualityComparer<List<Linea_Compra>>.Default.Equals(Lista_Compra, compra.Lista_Compra);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Portatil, Id_Prod, Compra, Id_Compra, Id_Linea, Cantidad, Precio_Unitario, Lista_Compra);
        }
    }
}
