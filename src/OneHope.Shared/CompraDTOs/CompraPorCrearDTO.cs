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
            LineasCompra = new List<LineaCompraDTO>();
        }

        public CompraPorCrearDTO(string direccion, IList<LineaCompraDTO> lineasCompra, string nombreUsuario,
            string apellidosUsuario,OneHope.Shared.TipoMetodoPago metodoPago)
        {
            Direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
            LineasCompra = lineasCompra ?? throw new ArgumentException(nameof(lineasCompra));
            NombreUsuario = nombreUsuario ?? throw new ArgumentNullException(nameof(nombreUsuario));
            ApellidosUsuario = apellidosUsuario ?? throw new ArgumentException(nameof(apellidosUsuario));
            MetodoPago = metodoPago;
        }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Por favor, introduzca su dirección de envío.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage ="La dirección de envío tiene que tener al menos 10 caracteres.")]
        [Display(Name ="Dirección De Envío.")]
        [JsonPropertyName("direccionEnvio")]
        public string Direccion { get; set; }

        [ValidateComplexType]
        [JsonPropertyName("lineaCompra")]
        public IList<LineaCompraDTO> LineasCompra { get; set; }

        [Display(Name ="Precio Total")]
        [JsonPropertyName("precioTotal")]
        public double PrecioTotal
        {
            get
            {
                return LineasCompra.Sum(pi => pi.Cantidad * pi.PrecioUnitario);
            }
        }

        [JsonPropertyName("nombreUsuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca su nombre.")]
        [StringLength(50, ErrorMessage = "No puedes tener un nombre que supere los 50 caracteres.")]
        public string NombreUsuario { get; set;}

        [JsonPropertyName("apellidosUsuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, introduzca sus apellidos.")]
        [StringLength(50, ErrorMessage = "No puedes tener apellidos que superen los 50 caracteres.")]
        public string ApellidosUsuario { get; set;}

        [Required]
        [JsonPropertyName("metodoDePago")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoMetodoPago MetodoPago { get; set;}

        public override bool Equals(object? obj)
        {
            return obj is CompraPorCrearDTO dTO &&
                   Direccion == dTO.Direccion &&
                   LineasCompra.SequenceEqual(dTO.LineasCompra) &&
                   PrecioTotal == dTO.PrecioTotal &&
                   NombreUsuario == dTO.NombreUsuario &&
                   ApellidosUsuario == dTO.ApellidosUsuario &&
                   MetodoPago == dTO.MetodoPago;
        }
    }
}
