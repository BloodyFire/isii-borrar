namespace OneHope.API.Models
{
    public class MetodoPago
    {
        //Constructores
        public MetodoPago(int iD)
        {
            ID = iD;
        }
        //Atributos
        [Key]
        public int ID { get; set; }
    }
}
