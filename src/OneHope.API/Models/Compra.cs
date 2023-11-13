using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class Compra
    {

        public Compra(int id, int customerId, DateTime fechaCompra, string direccion, TipoMetodoPago metodosPagos, double total)
        {
            Id = id;
            CustomerId = customerId;
            FechaCompra = fechaCompra;
            Direccion = direccion;
            MetodoPago = metodosPagos;
            Total = total;
        }

        public Compra(int id, int customerId, DateTime fechaCompra, string direccion, TipoMetodoPago metodosPagos, double total, string nombreCliente, string apellidos)
        {
            Id = id;
            CustomerId = customerId;
            FechaCompra = fechaCompra;
            Direccion = direccion;
            MetodoPago = metodosPagos;
            Total = total;
            NombreCliente = nombreCliente;
            Apellidos = apellidos;
            LineasCompra = new List<LineaCompra>();
        }

        public Compra()
        {
            LineasCompra = new List<LineaCompra>();
        }

        public Compra(string nombreCliente, string apellidosCliente, string direccion, DateTime fechaCompra,
            IList<LineaCompra> lineasCompra, TipoMetodoPago metodoPago, double precioTotal)
        {
            Total = precioTotal;
            FechaCompra = fechaCompra;
            Direccion = direccion;
            MetodoPago = metodoPago;
            LineasCompra = lineasCompra;
            NombreCliente = nombreCliente;
            Apellidos = apellidosCliente;
        }

        [Key]
        public int Id {  get; set; }

        
        public int CustomerId {  get; set; }

        [Required, StringLength(50, ErrorMessage = "El cliente no puede tener un nombre que supere los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public string NombreCliente { get; set; }

        [Required, StringLength(50, ErrorMessage = "El cliente no puede tener unos apellidos que superen los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public string Apellidos { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra{  get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, escribe tu direccion de envio")]
        public string Direccion { get; set; }

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
                   EqualityComparer<IList<LineaCompra>>.Default.Equals(LineasCompra, compra.LineasCompra) &&
                   Total == compra.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CustomerId, FechaCompra, Direccion, LineasCompra, MetodoPago, Total);
        }
    }
}
