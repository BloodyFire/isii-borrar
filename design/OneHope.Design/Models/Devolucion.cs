using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.Models
{
    public class Devolucion
    {

        public Devolucion() { }

        public Devolucion(int id, DateTime fecha, float cuantiaDevolucion, string direccionRecogida, string nota, string motivo) {
            IdDevolucion = id; 
            Fecha = fecha;
            CuantiaDevolucion = cuantiaDevolucion;
            DireccionRecogida = direccionRecogida;
            NotaRepartidor = nota;
            MotivoDevolucion = motivo;
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

        [Required, StringLength(100, ErrorMessage = "El motivo no puede exceder los 100 caracteres.")]
        [RegularExpression(@"[a-zA-Z]*$")]
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
