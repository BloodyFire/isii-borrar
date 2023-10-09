using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class LineaAlquiler
    {
        //Atributos
        [Key]
        public int ID { get; set; }

        public Portatil Portatil { get; set; }
        public int PortatilID { get; set; }
        public float PortatilPrecioAlq { get; set; }

        public Alquiler Alquiler { get; set; }
        public int AlquilerID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima para alquilar es 1.")]
        public int Cantidad { get; set; }

        //Constructores
        public LineaAlquiler() { }

        public LineaAlquiler(int ID, int Cantidad, Portatil Portatil, Alquiler Alquiler)
        {
            this.ID = ID;
            this.Portatil = Portatil;
            this.Alquiler = Alquiler;
            this.PortatilID = Portatil.ID;
            this.AlquilerID = Alquiler.ID;
            this.Cantidad = Cantidad;
            this.PortatilPrecioAlq = Portatil.PrecioAlq;
        }

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is LineaAlquiler item &&
                   PortatilID == item.PortatilID &&
                   AlquilerID == item.AlquilerID &&
                   Cantidad == item.Cantidad &&
                   PortatilPrecioAlq == item.PortatilPrecioAlq;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PortatilID, AlquilerID, Cantidad, PortatilPrecioAlq);
        }
    }
}
