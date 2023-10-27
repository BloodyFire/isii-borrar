using System.Numerics;

namespace OneHope.API.Models
{
    public class Alquiler
    {
        //Atributos
        [Key]
        public int ID { get; set; }

        [Required, DataType(DataType.Date), Display(Name ="Fecha de Alquiler")]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaAlquiler { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Fecha Inicio de Alquiler")]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaInAlquiler { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Fecha Fin de Alquiler")]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaFinAlquiler { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float Total { get; set; }

        [Required, StringLength(50, ErrorMessage = "El cliente no puede tener un nombre que supere los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public string NombreCliente { get; set; }

        [Required, StringLength(50, ErrorMessage = "El cliente no puede tener unos Apellidos que supere los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z\s]*$")]
        public string ApellidosCliente { get; set; }

        [Required, StringLength(75, ErrorMessage = "La calle no puede ser mayor de 75 caracteres.")]
        [RegularExpression(@"[a-zA-Z\s]*$")]
        public string DireccionEnvio { get; set; }

        [Required, EmailAddress]
        public string EmailCliente { get; set; }

        [Phone]
        public int? TelefonoCliente { get; set; }

        [Required]
        [Display(Name = "Metodo de Pago")]
        public TipoMetodoPago MetodoPago { get; set; }

        public IList<LineaAlquiler> LineasAlquiler { get; set; }

        //Constructores
        public Alquiler() 
        {
            LineasAlquiler = new List<LineaAlquiler>();
        }

        public Alquiler(int iD, DateTime fechaAlquiler, DateTime fechaInAlquiler, DateTime fechaFinAlquiler, float total, string nombreCliente, string apellidosCliente, string direccionEnvio, string emailCliente, int telefonoCliente, TipoMetodoPago metodoPago, IList<LineaAlquiler> lineasAlquiler)
        {
            ID = iD;
            FechaAlquiler = fechaAlquiler;
            FechaInAlquiler = fechaInAlquiler;
            FechaFinAlquiler = fechaFinAlquiler;
            Total = total;
            DireccionEnvio = direccionEnvio;
            EmailCliente = emailCliente;
            NombreCliente = nombreCliente;
            ApellidosCliente = apellidosCliente;
            TelefonoCliente = telefonoCliente;
            MetodoPago = metodoPago;
            LineasAlquiler = lineasAlquiler;

        }

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is Alquiler alq &&
                   ID == alq.ID &&
                   Total == alq.Total &&
                   FechaAlquiler == alq.FechaAlquiler &&
                   FechaInAlquiler == alq.FechaInAlquiler &&
                   FechaFinAlquiler == alq.FechaFinAlquiler &&
                   DireccionEnvio == alq.DireccionEnvio &&
                   MetodoPago == alq.MetodoPago;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Total, FechaAlquiler, FechaInAlquiler, FechaFinAlquiler, DireccionEnvio, MetodoPago, LineasAlquiler);
        }

        //Tipo de metodo de pago
        /*public enum TipoMetodoPago
        {
            TarjetaCredito,
            PayPal,
            Transferencia
        }*/
    }
}
