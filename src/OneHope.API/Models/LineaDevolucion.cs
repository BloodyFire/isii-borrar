﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class LineaDevolucion
    {

        public LineaDevolucion() { }

        public LineaDevolucion( int cantidad, LineaCompra lineaCompra, Devolucion devolucion)
        {
            Cantidad = cantidad;
            LineaCompra = lineaCompra;
            LineaCompraId = lineaCompra.IdLinea;
            Devolucion = devolucion;
            IdDevolucion = devolucion.IdDevolucion;
        }

        public LineaDevolucion(int idLinea, int cantidad, LineaCompra lineaCompra, Devolucion devolucion)
        {
            IdLinea = idLinea;
            Cantidad = cantidad;
            LineaCompra = lineaCompra;
            LineaCompraId = lineaCompra.IdLinea;
            Devolucion = devolucion;
            IdDevolucion = devolucion.IdDevolucion;
        }

        [Key]
        public int IdLinea { get; set; }

        

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [ForeignKey("LineaCompraId")]
        public LineaCompra LineaCompra { get; set; }

        public int LineaCompraId { get; set; }



        [Required]
        [ForeignKey("IdDevolucion")]
        public Devolucion Devolucion { get; set; }

        public int IdDevolucion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is LineaDevolucion devolucion &&
                   IdLinea == devolucion.IdLinea &&
                   Cantidad == devolucion.Cantidad &&
                   EqualityComparer<LineaCompra>.Default.Equals(LineaCompra, devolucion.LineaCompra) &&
                   LineaCompraId == devolucion.LineaCompraId &&
                   EqualityComparer<Devolucion>.Default.Equals(Devolucion, devolucion.Devolucion) &&
                   IdDevolucion == devolucion.IdDevolucion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdLinea, Cantidad, LineaCompra, LineaCompraId, Devolucion, IdDevolucion);
        }
    }
}
