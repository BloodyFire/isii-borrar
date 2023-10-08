namespace OneHope.API.Models
{
    public class MetodoPago
    {
        //Constructores
        public MetodoPago(int ID)
        {
            this.ID = ID;
        }
        //Atributos
        [Key]
        public int ID { get; set; }
    }
}
