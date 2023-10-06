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

        [Key]
        public int Id_Linea {  get; set; }

        public int Id_Prod {  get; set; }

        public int Id_Compra {  get; set; }

        public int Cantidad {  get; set; }

        public double Precio_Unitario {  get; set; }

        public List<Linea_Compra> Lista_Compra { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Linea_Compra compra &&
                   Id_Linea == compra.Id_Linea &&
                   Id_Prod == compra.Id_Prod &&
                   Id_Compra == compra.Id_Compra;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Linea, Id_Prod, Id_Compra, Cantidad, Precio_Unitario, Lista_Compra);
        }
    }
}
