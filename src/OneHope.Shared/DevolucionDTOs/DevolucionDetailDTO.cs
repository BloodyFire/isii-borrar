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
    public class DevolucionDetailDTO : DevolucionForCreateDTO
    {

        public DevolucionDetailDTO() { }

        public DevolucionDetailDTO(int idDevolucion, string motivoDevolucion, string direccionRecogida,
            DateTime fecha, IList<DevolucionItemDTO> lineasDevoluciones, string notaRepartidor = "") : 
            base( motivoDevolucion, direccionRecogida, fecha, lineasDevoluciones, notaRepartidor)
        {
            IdDevolucion = idDevolucion;
        }

        [JsonPropertyName("IdDevolucion")]
        public int IdDevolucion { get; set; }



        public override bool Equals(object? obj)
        {
            return obj is DevolucionDetailDTO dTO &&
                   IdDevolucion == dTO.IdDevolucion;
        }

       
    }
}
