namespace OneHope.API.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Total { get; set; }

        [Required]
        [DataType(DataType.Date), Display(Name = "Fecha de pedido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaPedido { get; set; }

        [Required]
        [Display(Name = "Código de empleado")]
        public string CódigoEmpleado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, indica la dirección del almacén de entrega.")]
        [DataType(DataType.MultilineText), Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public IList<LineaPedido> LineasPedido { get; set; } = new List<LineaPedido>();

        [Required]
        [Display(Name = "Método de Pago")]
        public TipoMetodoPago TipoMetodoPago { get; set; }

        [Required]
        public MetodoPago MetodoPago {get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; } = string.Empty;

        public Pedido()
        {
            LineasPedido = new List<LineaPedido>();
        }

        public Pedido(int id, double total, DateTime fechaPedido, string códigoEmpleado, string direccion, IList<LineaPedido> lineasPedido, TipoMetodoPago tipoMetodoPago, MetodoPago metodoPago, string comentarios)
        {
            Id = id;
            Total = total;
            FechaPedido = fechaPedido;
            CódigoEmpleado = códigoEmpleado;
            Direccion = direccion;
            LineasPedido = lineasPedido;
            TipoMetodoPago = tipoMetodoPago;
            MetodoPago = metodoPago;
            Comentarios = comentarios;
        }

        public override bool Equals(object? obj)
        {
            return obj is Pedido pedido &&
                   Id == pedido.Id &&
                   Total == pedido.Total &&
                   FechaPedido == pedido.FechaPedido &&
                   CódigoEmpleado == pedido.CódigoEmpleado &&
                   Direccion == pedido.Direccion &&
                   EqualityComparer<IList<LineaPedido>>.Default.Equals(LineasPedido, pedido.LineasPedido) &&
                   TipoMetodoPago == pedido.TipoMetodoPago &&
                   EqualityComparer<MetodoPago>.Default.Equals(MetodoPago, pedido.MetodoPago);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Total, FechaPedido, CódigoEmpleado, Direccion, LineasPedido, TipoMetodoPago, MetodoPago);
        }
    }
    public enum TipoMetodoPago
    {
        TarjetaCredito,
        PayPal,
        Transferencia
    }
}

