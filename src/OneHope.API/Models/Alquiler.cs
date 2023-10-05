namespace OneHope.API.Models
{
    public class Alquiler
    {
        [Key]
        public int ID { get; set; }

        [Required, DataType(DataType.Date), Display(Name ="Fecha de Alquiler")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaAlquiler { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Fecha Inicio de Alquiler")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaInAlquiler { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Fecha Fin de Alquiler")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime FechaFinAlquiler { get; set; }
        
        public int Total { get; set; }

        [Required, StringLength(75, ErrorMessage = "La calle no puede ser mayor de 75 caracteres.")]
        [RegularExpression(@"[a-zA-Z\s]*$")]
        public string DireccionEnvio { get; set; }
    }
}
