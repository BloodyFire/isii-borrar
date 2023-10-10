namespace OneHope.API.Models
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

        public LineaAlquiler(int iD, int cantidad, Portatil portatil, Alquiler alquiler)
        {
            ID = iD;
            Portatil = portatil;
            Alquiler = alquiler;
            PortatilID = Portatil.ID;
            AlquilerID = Alquiler.ID;
            Cantidad = cantidad;
            PortatilPrecioAlq = Portatil.PrecioAlq;
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
