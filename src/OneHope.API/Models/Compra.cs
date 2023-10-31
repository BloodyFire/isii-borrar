using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class Compra
    {

        public Compra(int id, int customerId, DateTime fechaCompra, string direccion, TipoMetodoPago metodosPagos, int total)
        {
            Id = id;
            CustomerId = customerId;
            FechaCompra = fechaCompra;
            Direccion = direccion;
            MetodoPago = metodosPagos;
            Total = total;
        }

        public Compra()
        {
            LineasCompra = new List<LineaCompra>();
        }

        [Key]
        public int Id {  get; set; }

        
        public int CustomerId {  get; set; }

        [Required, StringLength(50, ErrorMessage = "El cliente no puede tener un nombre que supere los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public String NombreCliente { get; set; }

        [Required, StringLength(50, ErrorMessage = "El cliente no puede tener unos apellidos que superen los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public String Apellidos { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra{  get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, escribe tu direccion de envio")]
        public String Direccion { get; set; }

        public IList<LineaCompra> LineasCompra { get; set; }


        [Display(Name = "Metodo Pago")]
        [Required()]
        public TipoMetodoPago MetodoPago { get; set; }

        [Required]
        public double Total {  get; set; }
        
        /*public enum MetodoPago
        {
            TarjetaCredito,
            PayPal,
            Transferencia
        }*/

        public override bool Equals(object? obj)
        {
            return obj is Compra compra &&
                   Id == compra.Id &&
                   CustomerId == compra.CustomerId &&
                   FechaCompra == compra.FechaCompra &&
                   Direccion == compra.Direccion &&
                   Total == compra.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CustomerId, FechaCompra, Direccion, LineasCompra, MetodoPago, Total);
        }

    }
}
