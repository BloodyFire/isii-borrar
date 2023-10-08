namespace OneHope.API.Models
{
    public class TarjetaCredito : MetodoPago
    {
        public TarjetaCredito(int id) : base(id)
        {

        }

        public TarjetaCredito(int id, int numTarjetaCredito, int cCV, DateTime fechaCaducidad) : base(id)
        {
            NumTarjetaCredito = numTarjetaCredito;
            CCV = cCV;
            FechaCaducidad = fechaCaducidad;
        }

        [Required]
        [CreditCard]
        [Display(Name = "Número de tarjeta")]
        public int NumTarjetaCredito { get; set; }

        [Required]
        public int CCV { get; set; }

        [Required]
        [DataType(DataType.Date), Display(Name = "Fecha de caducidad")]
        [DisplayFormat(DataFormatString = "{0:MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCaducidad { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is TarjetaCredito credito &&
                   Id == credito.Id &&
                   NumTarjetaCredito == credito.NumTarjetaCredito &&
                   CCV == credito.CCV &&
                   FechaCaducidad == credito.FechaCaducidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, NumTarjetaCredito, CCV, FechaCaducidad);
        }
    }
}