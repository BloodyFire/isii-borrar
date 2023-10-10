﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class LineaDevolucion
    {

        public LineaDevolucion() { }

        [Key]
        public int IdLinea { get; set; }

        

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public LineaCompra LineaCompra { get; set; }

        public int LineaCompraId { get; set; }

       
        public Portatil Portatil { get; set; }
        public int? PortatilId { get; set; }

        
        public Devolucion Devolucion { get; set; }

        public int IdDevolucion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is LineaDevolucion devolucion &&
                   IdLinea == devolucion.IdLinea &&
                   Cantidad == devolucion.Cantidad &&
                   EqualityComparer<LineaCompra>.Default.Equals(LineaCompra, devolucion.LineaCompra) &&
                   LineaCompraId == devolucion.LineaCompraId &&
                   EqualityComparer<Portatil>.Default.Equals(Portatil, devolucion.Portatil) &&
                   PortatilId == devolucion.PortatilId &&
                   EqualityComparer<Devolucion>.Default.Equals(Devolucion, devolucion.Devolucion) &&
                   IdDevolucion == devolucion.IdDevolucion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdLinea, Cantidad, LineaCompra, LineaCompraId, Portatil, PortatilId, Devolucion, IdDevolucion);
        }
    }
}
