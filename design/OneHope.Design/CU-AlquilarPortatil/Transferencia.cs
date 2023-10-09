using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class Transferencia : MetodoPago
    {
        //Constructores
        public Transferencia(int ID) : base(ID) { }

        //Atributos
        [Required]
        public int NumeroCuenta { get; set; }
    }
}
