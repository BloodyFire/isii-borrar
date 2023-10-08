namespace OneHope.API.Models
{
    public class LineaAlquiler
    {
        //Atributos
        [Key]
        public int ID { get; set; }

        public int PortatilID { get; set; }
        public float PortatilPrecioAlquiler { get; set; }


        public int AlquilerID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima para alquilar es 1.")]
        public int Cantidad { get; set; }

        //Constructores
        public LineaAlquiler() { }

        public LineaAlquiler(int ID, int Cantidad, float PortatilPrecioAlquiler)
        {
            this.ID = ID;
            this.Cantidad = Cantidad;
            this.PortatilPrecioAlquiler = PortatilPrecioAlquiler;
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
