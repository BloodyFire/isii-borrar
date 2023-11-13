namespace OneHope.API.Models
{
    public class Devolucion
    {

        public Devolucion() { }

        public Devolucion(int idDevolucion, DateTime fecha, float cuantiaDevolucion, string direccionRecogida, string notaRepartidor, string motivoDevolucion)
        {
            IdDevolucion = idDevolucion;
            Fecha = fecha;
            CuantiaDevolucion = cuantiaDevolucion;
            DireccionRecogida = direccionRecogida;
            NotaRepartidor = notaRepartidor;
            MotivoDevolucion = motivoDevolucion;
            LineaDevolucion = new List<LineaDevolucion>();

        }
        public Devolucion( DateTime fecha, float cuantiaDevolucion, string direccionRecogida, string notaRepartidor, string motivoDevolucion)
        {
            Fecha = fecha;
            CuantiaDevolucion = cuantiaDevolucion;
            DireccionRecogida = direccionRecogida;
            NotaRepartidor = notaRepartidor;
            MotivoDevolucion = motivoDevolucion;
            LineaDevolucion = new List<LineaDevolucion>();
        }

        [Key]
        public int IdDevolucion { get; set; }


        [DataType(DataType.Date), Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public float CuantiaDevolucion { get; set; }

        [Required]
        public string DireccionRecogida { get; set; }

        public string NotaRepartidor { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El motivo de la devolución no puede exceder los 100 caracteres.")]
        public string MotivoDevolucion { get; set; }

        public IList<LineaDevolucion> LineaDevolucion
        {
            get; set;
        }

        public override bool Equals(object? obj)
        {
            return obj is Devolucion devolucion &&
                   IdDevolucion == devolucion.IdDevolucion &&
                   Fecha == devolucion.Fecha &&
                   CuantiaDevolucion == devolucion.CuantiaDevolucion &&
                   DireccionRecogida == devolucion.DireccionRecogida &&
                   NotaRepartidor == devolucion.NotaRepartidor &&
                   MotivoDevolucion == devolucion.MotivoDevolucion &&
                   EqualityComparer<IList<LineaDevolucion>>.Default.Equals(LineaDevolucion, devolucion.LineaDevolucion);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdDevolucion, Fecha, CuantiaDevolucion, DireccionRecogida, NotaRepartidor, MotivoDevolucion, LineaDevolucion);
        }
    }
}
