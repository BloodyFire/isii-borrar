using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class TarjetaCredito : MetodoPago
    {
        //Constructores
        public TarjetaCredito(int ID) : base(ID) { }

        public TarjetaCredito(int ID, int NumTC, int CCV, DateTime FechaCaducidad) : base(ID)
        {
            this.NumTC = NumTC;
            this.CCV = CCV;
            this.FechaCaducidad = FechaCaducidad;
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
