using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class PayPal : MetodoPago
    {
        //Constructores
        public PayPal(int ID) : base(ID) { }

        public PayPal(int ID, string Email, int Telefono) : base(ID)
        {
            this.Email = Email;
            this.Telefono = Telefono;
        }

        //Atributos
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public int Telefono { get; set; }
    }
}
