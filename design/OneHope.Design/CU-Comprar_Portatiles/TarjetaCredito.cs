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
        public int NumTarjetaCredito { get; set; }

        [Required]
        public int CCV {  get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaCaducidad { get; set; }

    }
}
