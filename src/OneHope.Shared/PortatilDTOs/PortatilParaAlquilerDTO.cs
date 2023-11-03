using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.Shared.PortatilDTOs
{
    public class PortatilParaAlquilerDTO
    {
        public PortatilParaAlquilerDTO(int id, string nombre, string marca, string procesador, int cantidad, double precioAlquiler)
        {
            Id = id;
            Nombre = nombre;
            Marca = marca;
            Procesador = procesador;
            Cantidad = cantidad;
            PrecioAlquiler = precioAlquiler;
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double PrecioAlquiler { get; set; }
        [Required]
        public string Procesador { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PortatilParaAlquilerDTO dTO &&
                   Id == dTO.Id && Nombre == dTO.Nombre &&
                   Marca == dTO.Marca && Cantidad == dTO.Cantidad &&
                   Procesador == dTO.Procesador &&
                   PrecioAlquiler == dTO.PrecioAlquiler;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Marca, Procesador, Cantidad, PrecioAlquiler);
        }
    }
}
