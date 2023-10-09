
using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class MetodoPago
    {
        public MetodoPago(int id)
        {
            Id = id;
        }

        [Key]
        public int Id {  get; set; }

    }
}
