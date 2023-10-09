using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
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
