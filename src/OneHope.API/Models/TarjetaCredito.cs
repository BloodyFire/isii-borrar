namespace OneHope.API.Models
{
    public class TarjetaCredito : MetodoPago
    {
        //Constructores
        public TarjetaCredito(int iD) : base(iD) { }

        public TarjetaCredito(int iD, int numTC, int cCV, DateTime fechaCaducidad) : base(iD)
        {
            NumTC = numTC;
            CCV = cCV;
            FechaCaducidad = fechaCaducidad;
        }

        //Atributos
        [Required]
        [CreditCard]
        [Display(Name = "Número de la tarjeta de credito")]
        public int NumTC {  get; set; }

        [Required]
        public int CCV { get; set; }

        [Required]
        [DataType(DataType.Date), Display(Name = "Fecha de caducidad de la tarjeta")]
        [DisplayFormat(DataFormatString = "{0:MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCaducidad { get; set; }
    }
}
