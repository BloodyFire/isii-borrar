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
        public string CodigoEmpleado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, indica la dirección del almacén de entrega.")]
        [DataType(DataType.MultilineText), Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public IList<LineaPedido> LineasPedido { get; set; } = new List<LineaPedido>();

        [Required]
        [Display(Name = "Método de Pago")]
        public TipoMetodoPago TipoMetodoPago { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; } = string.Empty;

        public Pedido()
        {
            LineasPedido = new List<LineaPedido>();
        }

        public Pedido(int id, double total, DateTime fechaPedido, string codigoEmpleado, string direccion, IList<LineaPedido> lineasPedido, TipoMetodoPago tipoMetodoPago, string comentarios = "")
        {
            Id = id;
            Total = total;
            FechaPedido = fechaPedido;
            CodigoEmpleado = codigoEmpleado;
            Direccion = direccion;
            LineasPedido = lineasPedido;
            TipoMetodoPago = tipoMetodoPago;
            Comentarios = comentarios;
        }

        public Pedido( double total, DateTime fechaPedido, string codigoEmpleado, string direccion, IList<LineaPedido> lineasPedido, TipoMetodoPago tipoMetodoPago, string comentarios = "")
        {
            Total = total;
            FechaPedido = fechaPedido;
            CodigoEmpleado = codigoEmpleado;
            Direccion = direccion;
            LineasPedido = lineasPedido;
            TipoMetodoPago = tipoMetodoPago;
            Comentarios = comentarios;
        }

        public override bool Equals(object? obj)
        {
            return obj is Pedido pedido &&
                   Id == pedido.Id &&
                   Total == pedido.Total &&
                   FechaPedido == pedido.FechaPedido &&
                   CodigoEmpleado == pedido.CodigoEmpleado &&
                   Direccion == pedido.Direccion &&
                   EqualityComparer<IList<LineaPedido>>.Default.Equals(LineasPedido, pedido.LineasPedido) &&
                   TipoMetodoPago == pedido.TipoMetodoPago;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Total, FechaPedido, CodigoEmpleado, Direccion, LineasPedido, TipoMetodoPago);
        }
    }

}

