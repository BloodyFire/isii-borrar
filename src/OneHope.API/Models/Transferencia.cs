namespace OneHope.API.Models
{
    public class Transferencia : MetodoPago
    {
        public Transferencia(int id) : base(id)
        {

        }

        public override bool Equals(object? obj)
        {
            return obj is Transferencia transferencia &&
                   Id == transferencia.Id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}