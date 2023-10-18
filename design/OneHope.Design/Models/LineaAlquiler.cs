using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.Models
{
    public class LineaAlquiler
    {
        //Atributos
        [Key]
        public int ID { get; set; }

        public Portatil Portatil { get; set; }
        public int PortatilID { get; set; }
        public double PortatilPrecioAlquiler { get; set; }

        public Alquiler Alquiler { get; set; }
        public int AlquilerID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima para alquilar es 1.")]
        public int Cantidad { get; set; }

        //Constructores
        public LineaAlquiler() { }

        public LineaAlquiler(int iD, int cantidad, Portatil portatil, Alquiler alquiler)
        {
            ID = iD;
            Portatil = portatil;
            Alquiler = alquiler;
            PortatilID = Portatil.Id;
            AlquilerID = Alquiler.ID;
            Cantidad = cantidad;
            PortatilPrecioAlquiler = Portatil.PrecioAlquiler;
        }

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is LineaAlquiler item &&
                   PortatilID == item.PortatilID &&
                   AlquilerID == item.AlquilerID &&
                   Cantidad == item.Cantidad &&
                   PortatilPrecioAlquiler == item.PortatilPrecioAlquiler;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PortatilID, AlquilerID, Cantidad, PortatilPrecioAlquiler);
        }
    }
}
