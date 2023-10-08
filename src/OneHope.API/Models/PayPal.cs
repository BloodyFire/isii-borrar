namespace OneHope.API.Models
{
    public class PayPal : MetodoPago
    {
        public PayPal(int id) : base(id)
        {

        }

        public PayPal(int id, string email, int telefono) : base(id)
        {
            Email = email;
            Telefono = telefono;
        }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public int Telefono { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PayPal pal &&
                   Id == pal.Id &&
                   Email == pal.Email &&
                   Telefono == pal.Telefono;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Email, Telefono);
        }
    }
}