using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class Compra
    {
        public Compra()
        {

        }

        [Key]
        public int Id_Compra {  get; set; }

        
        public int Customer_Id {  get; set; }

        [Required]
        public DateTime Fecha {  get; set; }

        [Required]
        public int Total {  get; set; }

        
        public String Direccion {  get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Compra compra &&
                   Id_Compra == compra.Id_Compra &&
                   Customer_Id == compra.Customer_Id &&
                   Fecha == compra.Fecha &&
                   Total == compra.Total &&
                   Direccion == compra.Direccion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Compra, Customer_Id, Fecha, Total, Direccion);
        }
    }
}
