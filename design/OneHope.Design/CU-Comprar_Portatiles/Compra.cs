using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class Compra
    {

        public Compra(int id, int customerId, DateTime fechaCompra, string direccion, MetodoPago metodosPagos, int total)
        {
            Id = id;
            CustomerId = customerId;
            FechaCompra = fechaCompra;
            Direccion = direccion;
            MetodosPagos = metodosPagos;
            Total = total;
        }

        public Compra()
        {
            ListaCompras = new List<LineaCompra>();
        }

        [Key]
        public int Id {  get; set; }

        
        public int CustomerId {  get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra{  get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, escribe tu direccion de envio")]
        public String Direccion { get; set; }

        public IList<LineaCompra> ListaCompras { get; set; }


        [Display(Name = "Metodo Pago")]
        [Required()]
        public MetodoPago MetodosPagos { get; set; }

        [Required]
        public int Total {  get; set; }
        
        public enum MetodPago
        {
            TarjetaCredito,
            PayPal,
            Transferencia
        }

        public override bool Equals(object? obj)
        {
            return obj is Compra compra &&
                   Id == compra.Id &&
                   CustomerId == compra.CustomerId &&
                   FechaCompra == compra.FechaCompra &&
                   Direccion == compra.Direccion &&
                   EqualityComparer<IList<LineaCompra>>.Default.Equals(ListaCompras, compra.ListaCompras) &&
                   MetodosPagos == compra.MetodosPagos &&
                   Total == compra.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CustomerId, FechaCompra, Direccion, ListaCompras, MetodosPagos, Total);
        }
    }
}
