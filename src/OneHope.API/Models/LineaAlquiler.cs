namespace OneHope.API.Models
{
    public class LineaAlquiler
    {
        [Key]
        public int ID { get; set; }

        public int PortatilID { get; set; }
        public int AlquilerID { get; set; }

        public int Cantidad { get; set; }
    }
}
