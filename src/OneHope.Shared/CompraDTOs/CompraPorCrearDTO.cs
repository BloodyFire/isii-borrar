using OneHope.Design.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.CompraDTOs
{
    public class CompraPorCrearDTO
    {
        public CompraPorCrearDTO()
        {
            compraPortatiles = new List<CompraPortatilDTO>();
        }

        public CompraPorCrearDTO(string direccion, IList<CompraPortatilDTO> compraPortatiles, string nombreUsuario,
            string apellidosUsuario, TipoMetodoPago metodoPago)
        {
            direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
            compraPortatiles = compraPortatiles ?? throw new ArgumentException(nameof(compraPortatiles));
            nombreUsuario = nombreUsuario ?? throw new ArgumentNullException(nameof(nombreUsuario));
            apellidosUsuario = apellidosUsuario ?? throw new ArgumentException(nameof(apellidosUsuario));
            metodoPago = metodoPago;
        }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Por favor, introduzca su dirección de envío.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage ="La dirección de envío tiene que tener al menos 10 caracteres.")]
        [Display(Name ="Dirección De Envío.")]
        [JsonPropertyName("direccionEnvio")]
        public string direccion { get; set; }

        [ValidateComplexType]
        [JsonPropertyName("compraPortatiles")]
        public IList<CompraPortatilDTO> compraPortatiles { get; set; }

        [Display(Name ="Precio Total")]
        [JsonPropertyName("PrecioTotal")]
        public double PrecioTotal
        {
            get
            {
                return compraPortatiles.Sum(p => p.Cantidad * p.precioCompra);
            }
        }

        [JsonPropertyName("nombreUsuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su nombre.")]
        [StringLength(50, ErrorMessage = "No puedes tener un nombre que supere los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public string nombreUsuario { get; set;}

        [JsonPropertyName("apellidosUsuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca sus apellidos.")]
        [StringLength(50, ErrorMessage = "No puedes tener apellidos que superen los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public string apellidosUsuario { get; set;}

        [Required]
        [JsonPropertyName("metodoDePago")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoMetodoPago MetodoPago { get; set;}

        public override bool Equals(object? obj)
        {
            return obj is CompraPorCrearDTO dTO &&
                   direccion == dTO.direccion &&
                   EqualityComparer<IList<CompraPortatilDTO>>.Default.Equals(compraPortatiles, dTO.compraPortatiles) &&
                   PrecioTotal == dTO.PrecioTotal &&
                   nombreUsuario == dTO.nombreUsuario &&
                   apellidosUsuario == dTO.apellidosUsuario &&
                   MetodoPago == dTO.MetodoPago;
        }
    }
}
