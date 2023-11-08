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
        public PortatilParaAlquilerDTO(int id, string modelo, string marca, string procesador, int stockAlquilar, double precioAlquiler)
        {
            Id = id;
            Modelo = modelo;
            Marca = marca;
            Procesador = procesador;
            StockAlquilar = stockAlquilar;
            PrecioAlquiler = precioAlquiler;
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public int StockAlquilar { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double PrecioAlquiler { get; set; }
        [Required]
        public string Procesador { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PortatilParaAlquilerDTO dTO &&
                   Id == dTO.Id && Modelo == dTO.Modelo &&
                   Marca == dTO.Marca && StockAlquilar == dTO.StockAlquilar &&
                   Procesador == dTO.Procesador &&
                   PrecioAlquiler == dTO.PrecioAlquiler;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Modelo, Marca, Procesador, StockAlquilar, PrecioAlquiler);
        }
    }
}
