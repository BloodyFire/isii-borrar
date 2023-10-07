using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace OneHope.API.Models
{
    public class Linea_Pedido
    {
        public Linea_Pedido() 
        { 

        }

        public Linea_Pedido(int id_Linea, Portatil portatil, int id_Prod, Pedido pedido, int id_Pedido, int cantidad, int id_Proveedor)
        {
            Id_Linea = id_Linea;
            Portatil = portatil;
            Id_Prod = id_Prod;
            Pedido = pedido;
            Id_Pedido = id_Pedido;
            Cantidad = cantidad;
            Id_Proveedor = id_Proveedor;
        }

        [Key]
        public int Id_Linea {  get; set; }


        [ForeignKey("Id_Prod")]
        public Portatil Portatil { get; set; }

        public int Id_Prod {  get; set; }

        [ForeignKey("Id_Pedido")]
        public Pedido Pedido { get; set; }

        public int Id_Pedido {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad a introducir debe ser válida.")]
        public int Cantidad {  get; set; }

        public int Id_Proveedor {  get; set; }

        public double Precio_U {  get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Linea_Pedido pedido &&
                   EqualityComparer<Portatil>.Default.Equals(Portatil, pedido.Portatil) &&
                   Id_Prod == pedido.Id_Prod &&
                   Id_Pedido == pedido.Id_Pedido &&
                   Cantidad == pedido.Cantidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Prod, Id_Pedido);
        }
    }
}
