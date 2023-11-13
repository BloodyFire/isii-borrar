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
          
        }

        public DevolucionForCreateDTO( string motivoDevolucion, string direccionRecogida, 
            DateTime fecha, IList<DevolucionItemDTO> lineasDevoluciones, string notaRepartidor= "")
        {
            MotivoDevolucion = motivoDevolucion;
            DireccionRecogida = direccionRecogida;
            Fecha = fecha;
            LineasDevoluciones = lineasDevoluciones;
            NotaRepartidor = notaRepartidor;
        }


        [Required]
        [StringLength(100, ErrorMessage = "El motivo de la devolución no puede exceder los 100 caracteres.")]
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


        [Display(Name = "Cuantia de Devolucion")]
        [JsonPropertyName("CuantiaDevolucion")]
        public float CuantiaDevolucion {
            get
            {
                return (float)LineasDevoluciones.Sum(ld => ld.Cantidad * ld.PrecioUnitario );
            }
         }

        [JsonPropertyName("LineasDevoluciones")]
        public IList<DevolucionItemDTO> LineasDevoluciones { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is DevolucionForCreateDTO dTO &&
                   MotivoDevolucion == dTO.MotivoDevolucion &&
                   DireccionRecogida == dTO.DireccionRecogida &&
                   NotaRepartidor == dTO.NotaRepartidor &&
                   Fecha == dTO.Fecha &&
                   CuantiaDevolucion == dTO.CuantiaDevolucion &&
                   EqualityComparer<IList<DevolucionItemDTO>>.Default.Equals(LineasDevoluciones, dTO.LineasDevoluciones);
                   
        }
    }
}
