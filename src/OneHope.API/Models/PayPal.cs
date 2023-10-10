namespace OneHope.API.Models
{
    public class PayPal : MetodoPago
    {
        //Constructores
        public PayPal(int iD) : base(iD) { }

        public PayPal(int iD, string email, int telefono) : base(iD)
        {
            Email = email;
            Telefono = telefono;
        }

        //Atributos
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public int Telefono { get; set; }
    }
}
