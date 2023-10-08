namespace OneHope.API.Models
{
    public class Alquiler
    {
        //Atributos
        [Key]
        public int ID { get; set; }

        [Required, DataType(DataType.Date), Display(Name ="Fecha de Alquiler")]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaAlquiler { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Fecha Inicio de Alquiler")]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaInAlquiler { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Fecha Fin de Alquiler")]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaFinAlquiler { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float Total { get; set; }

        [Required, StringLength(75, ErrorMessage = "La calle no puede ser mayor de 75 caracteres.")]
        [RegularExpression(@"[a-zA-Z\s]*$")]
        public string DireccionEnvio { get; set; }

        [Required]
        [Display(Name = "Metodo de Pago")]
        public MetodoPago MetodoPago { get; set; }

        //Constructores
        public Alquiler() 
        {

        }

        public Alquiler(int ID, DateTime FechaAlquiler, DateTime FechaInAlquiler, DateTime FechaFinAlquiler, float Total, string DireccionEnvio, MetodoPago MetodoPago)
        {
            this.ID = ID;
            this.FechaAlquiler = FechaAlquiler;
            this.FechaInAlquiler = FechaInAlquiler;
            this.FechaFinAlquiler = FechaFinAlquiler;
            this.Total = Total;
            this.DireccionEnvio = DireccionEnvio;
            this.MetodoPago = MetodoPago;

        }

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is Alquiler alq &&
                   ID == alq.ID &&
                   Total == alq.Total &&
                   FechaAlquiler == alq.FechaAlquiler &&
                   FechaInAlquiler == alq.FechaInAlquiler &&
                   FechaFinAlquiler == alq.FechaFinAlquiler &&
                   DireccionEnvio == alq.DireccionEnvio &&
                   EqualityComparer<MetodoPago>.Default.Equals(MetodoPago, alq.MetodoPago);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Total, FechaAlquiler, FechaInAlquiler, FechaFinAlquiler, DireccionEnvio, MetodoPago);
        }
    }
}
