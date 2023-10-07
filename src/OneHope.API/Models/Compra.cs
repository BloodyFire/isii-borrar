using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class Compra
    {

        public Compra(int id_Compra, int customer_Id, DateTime fecha_Compra, string direccion, Metodo_Pago metodos_Pagos, int total)
        {
            Id_Compra = id_Compra;
            Customer_Id = customer_Id;
            Fecha_Compra = fecha_Compra;
            Direccion = direccion;
            Metodos_Pagos = metodos_Pagos;
            Total = total;
        }

        public Compra()
        {
            Lista_Compras = new List<Linea_Compra>();
        }

        [Key]
        public int Id_Compra {  get; set; }

        
        public int Customer_Id {  get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Compra{  get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, escribe tu direccion de envio")]
        public String Direccion { get; set; }

        public IList<Linea_Compra> Lista_Compras { get; set; }


        [Display(Name = "Metodo Pago")]
        [Required()]
        public Metodo_Pago Metodos_Pagos { get; set; }

        [Required]
        public int Total {  get; set; }
        
        public enum Metodo_Pago
        {
            TarjetaCredito,
            PayPal,
            Transferencia
        }

        public override bool Equals(object? obj)
        {
            return obj is Compra compra &&
                   Id_Compra == compra.Id_Compra &&
                   Customer_Id == compra.Customer_Id &&
                   Fecha_Compra == compra.Fecha_Compra &&
                   Direccion == compra.Direccion &&
                   EqualityComparer<IList<Linea_Compra>>.Default.Equals(Lista_Compras, compra.Lista_Compras) &&
                   Metodos_Pagos == compra.Metodos_Pagos &&
                   Total == compra.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Compra, Customer_Id, Fecha_Compra, Direccion, Lista_Compras, Metodos_Pagos, Total);
        }
    }
}
