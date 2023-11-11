using OneHope.Shared.CompraDTOs;
using OneHope.Shared.PortatilDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneHope.Shared.DevolucionDTOs
{
    public class DevolucionForCreateDTO
    {



        public DevolucionForCreateDTO() {

           LineasDevoluciones = new List<DevolucionItemDTO>();
           //LineasCompras  = new List<CompraPortatilDTO>();
        }

        public DevolucionForCreateDTO(/*int cantidad*/int idDevolucion, string motivoDevolucion, string direccionRecogida, 
            DateTime fecha, float cuantiaDevolucion, IList<DevolucionItemDTO> lineasDevoluciones, string notaRepartidor= "")
        {
            //Cantidad = cantidad;
            MotivoDevolucion = motivoDevolucion;
            DireccionRecogida = direccionRecogida;
            Fecha = fecha;
            //IdCompra = idCompra;
            LineasDevoluciones = lineasDevoluciones;
            NotaRepartidor = notaRepartidor;
        }
        /*
        [JsonPropertyName("Cantidad")]
        [Required]
        public int Cantidad { get; set; }
        */
        [JsonPropertyName("IdDevolucion")]
        public int IdDevolucion { get; set; }

        [Required, StringLength(100, ErrorMessage = "El motivo no puede exceder los 100 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        [Display(Name = "Motivo Devolucion")]
        [JsonPropertyName("MotivoDevolucion")]
        public string MotivoDevolucion { get; set; }

        [Display(Name = "Dirección de Recogida")]
        [JsonPropertyName("DireccionRecogida")]
        [Required]
        public string DireccionRecogida { get; set; }

        [Display(Name = "Nota para Repartidor")]
        [JsonPropertyName("NotaRepartidor")]
        public string? NotaRepartidor { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha Devolucion")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [JsonPropertyName("IdCompra")]
        public int IdCompra { get; set; }


        [StringLength(50, ErrorMessage = "El modelo del portatil no puede tener más de 50 characters.")]
        [JsonPropertyName("Modelo")]
        public string Modelo { get; set; } = string.Empty;

        [Display(Name = "Cuantia de Devolucion")]
        [JsonPropertyName("CuantiaDevolucion")]
        public float CuantiaDevolucion {
            get
            {
                return (float) Portatiles.Sum(p => p.Total - (LineasCompras.Sum(l => l.PrecioCompra * (LineasDevoluciones.Sum(c => c.Cantidad + 0)))));
            }
         }

        [JsonPropertyName("LineasDevoluciones")]
        public IList<DevolucionItemDTO> LineasDevoluciones { get; set; }

        [JsonPropertyName("LineasCompras")]
        public IList<CompraPortatilDTO> LineasCompras { get; set; }

        [JsonPropertyName("Portatiles")]
        public IList<PortatilesParaDevolverDTO> Portatiles { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DevolucionForCreateDTO dTO &&
                   MotivoDevolucion == dTO.MotivoDevolucion &&
                   DireccionRecogida == dTO.DireccionRecogida &&
                   NotaRepartidor == dTO.NotaRepartidor &&
                   Fecha == dTO.Fecha &&
                   IdCompra == dTO.IdCompra &&
                   Modelo == dTO.Modelo &&
                   CuantiaDevolucion == dTO.CuantiaDevolucion &&
                   EqualityComparer<IList<DevolucionItemDTO>>.Default.Equals(LineasDevoluciones, dTO.LineasDevoluciones);
                   
        }
    }
}
