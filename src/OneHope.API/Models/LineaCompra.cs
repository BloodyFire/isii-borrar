﻿using System.ComponentModel.DataAnnotations.Schema;

namespace OneHope.API.Models
{
    public class LineaCompra
    {
        public LineaCompra() { }

        public LineaCompra(int idLinea, Portatil portatil, Compra compra, int cantidad, double precioUnitario)
        {
            IdLinea = idLinea;
            IdPortatil = portatil.Id;
            IdCompra = compra.Id;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        [Required]
        [ForeignKey("IdPortatil")]
        public Portatil Portatil { get; set; }

        public int IdPortatil {  get; set; }

        [Required]
        [ForeignKey("IdCompra")]
        public Compra Compra { get; set; }

        public int IdCompra { get; set; }

        [Key]
        public int IdLinea {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes introducir una cantidad válida.")]
        public int Cantidad {  get; set; }

        public double PrecioUnitario {  get; set; }

        public LineaDevolucion? LineaDevolucion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is LineaCompra compra &&
                   EqualityComparer<Portatil>.Default.Equals(Portatil, compra.Portatil) &&
                   IdPortatil == compra.IdPortatil &&
                   IdLinea == compra.IdLinea &&
                   Cantidad == compra.Cantidad &&
                   PrecioUnitario == compra.PrecioUnitario;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdPortatil, IdLinea);
        }
    }
}
