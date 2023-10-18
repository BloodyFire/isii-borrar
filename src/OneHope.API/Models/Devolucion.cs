namespace OneHope.API.Models
{
    public class Devolucion
    {

        public Devolucion() { }

        public Devolucion(int id, DateTime fecha, float total, string direccion, string reseña) {
            IdDevolucion = id; 
            Fecha = fecha;
            Total = total;
            Direccion = direccion;
            Reseña = reseña;
        }

        [Key]
        public int IdDevolucion { get; set; }


        [DataType(DataType.Date), Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public float Total { get; set; }

        [Required]
        public string Direccion { get; set; }

        public string Reseña { get; set; }

        public IList<LineaDevolucion> LineaDevolucion
        { 
            get; set;
        }

        public override bool Equals(object? obj)
        {
            return obj is Devolucion devolucion &&
                   IdDevolucion == devolucion.IdDevolucion &&
                   Fecha == devolucion.Fecha &&
                   Total == devolucion.Total &&
                   Direccion == devolucion.Direccion &&
                   Reseña == devolucion.Reseña &&
                   EqualityComparer<IList<LineaDevolucion>>.Default.Equals(LineaDevolucion, devolucion.LineaDevolucion);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdDevolucion, Fecha, Total, Direccion, Reseña, LineaDevolucion);
        }
    }
}
