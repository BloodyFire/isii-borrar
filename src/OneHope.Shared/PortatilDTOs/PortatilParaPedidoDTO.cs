using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.Shared.PortatilDTOs
{
    public class PortatilParaPedidoDTO
    {
        public PortatilParaPedidoDTO(int id, string modelo, string marca, int stock, double precioCoste, string proveedor)
        {
            Id = id;
            Modelo = modelo;
            Marca = marca;
            Stock = stock;
            PrecioCoste = precioCoste;
            Proveedor = proveedor;
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double PrecioCoste { get; set; }
        [Required]
        public string Proveedor { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PortatilParaPedidoDTO dTO &&
                   Id == dTO.Id &&
                   Modelo == dTO.Modelo &&
                   Marca == dTO.Marca &&
                   Stock == dTO.Stock &&
                   PrecioCoste == dTO.PrecioCoste &&
                   Proveedor == dTO.Proveedor;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Modelo, Marca, Stock, PrecioCoste, Proveedor);
        }
    }

   


}
