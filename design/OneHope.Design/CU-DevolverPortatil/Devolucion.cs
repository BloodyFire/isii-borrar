using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class Devolucion
    {

        public Devolucion() { }

        [Key]
        public int IdDevolucion { get; set; }

        public int IdCompra { get; set; }

        [DataType(DataType.Date), Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public float Total { get; set; }

        public IList<LineaDevolucion> LineaDevolucion
        {
            get; set;
        }



        public override bool Equals(object? obj)
        {
            return obj is Devolucion devolucion &&
                   IdDevolucion == devolucion.IdDevolucion &&
                   IdCompra == devolucion.IdCompra &&
                   Fecha == devolucion.Fecha &&
                   Total == devolucion.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdDevolucion, IdCompra, Fecha, Total);
        }
    }
}
