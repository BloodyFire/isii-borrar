namespace OneHope.API.Models
{
    public class Pedido
    {
        public Pedido()
        {
            ListaPedidos = new List<Linea_Pedido>();
        }

        [Key]
        public int Id { get; set; } 

        public int Id_Admin {  get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Pedido { get; set; }

        public double Total {  get; set; }

        public IList<Linea_Pedido> ListaPedidos { get; set; }


    }
}
