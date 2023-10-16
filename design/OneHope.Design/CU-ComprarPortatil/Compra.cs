﻿using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.CUComprarPortatil
{
    public class Compra
    {

        public Compra(int id, int customerId, DateTime fechaCompra, string direccion, MetodoPago metodosPagos, int total)
        {
            Id = id;
            CustomerId = customerId;
            FechaCompra = fechaCompra;
            Direccion = direccion;
            MetodosPagos = metodosPagos;
            Total = total;
        }

        public Compra()
        {
            ListaCompras = new List<LineaCompra>();
        }

        [Key]
        public int Id {  get; set; }

        
        public int CustomerId {  get; set; }

        [Required, StringLength(50, ErrorMessage = "El cliente no puede tener un nombre que supere los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public String NombreCliente { get; set; }

        [Required, StringLength(50, ErrorMessage = "El cliente no puede tener unos apellidos que superen los 50 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public String Apellidos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes introducir una cantidad válida.")]
        public int Cantidad {  get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra{  get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, escribe tu direccion de envio")]
        public String Direccion { get; set; }

        public IList<LineaCompra> ListaCompras { get; set; }


        [Display(Name = "Metodo Pago")]
        [Required()]
        public MetodoPago MetodosPagos { get; set; }

        [Required]
        public int Total {  get; set; }
        
        public enum MetodoPago
        {
            TarjetaCredito,
            PayPal,
            Transferencia
        }

        public override bool Equals(object? obj)
        {
            return obj is Compra compra &&
                   Id == compra.Id &&
                   CustomerId == compra.CustomerId &&
                   FechaCompra == compra.FechaCompra &&
                   Direccion == compra.Direccion &&
                   Total == compra.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CustomerId, FechaCompra, Direccion, ListaCompras, MetodosPagos, Total);
        }

    }
}
