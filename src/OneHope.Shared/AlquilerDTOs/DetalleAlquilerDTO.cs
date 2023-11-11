using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OneHope.Shared.AlquilerDTOs
{
    public class DetalleAlquilerDTO : AlquilerParaCrearDTO
    {
        public DetalleAlquilerDTO(int id, DateTime fechaAlquiler, DateTime fechaInAlquiler, DateTime fechaFinAlquiler,
            string emailCliente, string nombreCliente, string apellidosCliente,
            string direccionEnvio, int? telefonoCliente, TipoMetodoPago tipoMetodoPago,
             IList<LineaAlquilerDTO> lineasAlquiler)
            : base(fechaInAlquiler, fechaFinAlquiler, emailCliente,
                  nombreCliente, apellidosCliente, direccionEnvio, telefonoCliente, 
                  lineasAlquiler, tipoMetodoPago)
        {
            Id = id;
            FechaAlquiler = fechaAlquiler;
        }

        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("FechaAlquiler")]
        public DateTime FechaAlquiler { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetalleAlquilerDTO dTO &&
                   base.Equals(obj) &&
                   Id == dTO.Id &&
                   CompararFechas(FechaAlquiler, dTO.FechaAlquiler);
        }
    }
}
