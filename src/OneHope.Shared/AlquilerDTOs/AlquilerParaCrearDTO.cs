using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OneHope.Shared.AlquilerDTOs
{
    public class AlquilerParaCrearDTO
    {
        public AlquilerParaCrearDTO() 
        { 
            LineasAlquiler = new List<LineaAlquilerDTO>();
        }

        public AlquilerParaCrearDTO(DateTime fechaInAlquiler, DateTime fechaFinAlquiler, string emailCliente, string nombreCliente, string apellidosCliente, string direccionEnvio, int? telefonoCliente, IList<LineaAlquilerDTO> lineasAlquiler, TipoMetodoPago tipoMetodoPago)
        {
            FechaInAlquiler = fechaInAlquiler; 
            FechaFinAlquiler = fechaFinAlquiler;
            EmailCliente = emailCliente ?? throw new ArgumentNullException(nameof(emailCliente));
            NombreCliente = nombreCliente ?? throw new ArgumentNullException(nameof(nombreCliente));
            ApellidosCliente = apellidosCliente ?? throw new ArgumentNullException(nameof(apellidosCliente));
            DireccionEnvio = direccionEnvio ?? throw new ArgumentNullException(nameof(direccionEnvio));
            TelefonoCliente = telefonoCliente;
            LineasAlquiler = lineasAlquiler ?? throw new ArgumentNullException(nameof(lineasAlquiler));
            TipoMetodoPago = tipoMetodoPago;
        }

        [JsonPropertyName("FechaInAlquiler")]
        public DateTime FechaInAlquiler { get; set; }
        [JsonPropertyName("FechaFinAlquiler")]
        public DateTime FechaFinAlquiler { get; set; }

        [JsonPropertyName("EmailCliente")]
        [EmailAddress]
        [Required]
        public string EmailCliente { get; set; }

        [JsonPropertyName("NombreCliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su nombre")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Su nombre debe tener al menos 3 caracteres")]
        public string NombreCliente { get; set; }
        [JsonPropertyName("ApellidosCliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca sus apellidos")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Sus apellidos deben tener al menos 5 caracteres")]
        public string ApellidosCliente { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, indica una dirección de entrega.")]
        [Display(Name = "Dirección de entrega")]
        [JsonPropertyName("Direccion")]
        public string DireccionEnvio { get; set; }

        [Phone]
        [Display(Name = "Telefono de cliente")]
        [JsonPropertyName("TelefonoCliente")]
        public int? TelefonoCliente { get; set; }


        [ValidateComplexType]
        [JsonPropertyName("LineasPedido")]
        public IList<LineaAlquilerDTO> LineasAlquiler { get; set; }

        [Required]
        [JsonPropertyName("TipoMetodoPago")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoMetodoPago TipoMetodoPago { get; set; }

        private int NumeroDias
        {
            get
            {
                return (FechaFinAlquiler - FechaInAlquiler).Days;
            }
        }

        protected bool CompararFechas(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        [Display(Name = "Precio total de alquiler")]
        [JsonPropertyName("Total")]
        public double Total
        {
            get
            {
                return LineasAlquiler.Sum(pi => pi.PrecioAlquiler * NumeroDias);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is AlquilerParaCrearDTO dTO &&
                   EmailCliente == dTO.EmailCliente &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidosCliente == dTO.ApellidosCliente &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   TipoMetodoPago == dTO.TipoMetodoPago &&
                   CompararFechas(FechaFinAlquiler, dTO.FechaFinAlquiler) &&
                   CompararFechas(FechaInAlquiler, dTO.FechaInAlquiler) &&
                   Total == dTO.Total &&
                   LineasAlquiler.SequenceEqual(dTO.LineasAlquiler);
        }

    }
}
