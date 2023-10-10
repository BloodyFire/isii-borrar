namespace OneHope.API.Models
{
    public class Transferencia : MetodoPago
    {
        //Constructores
        public Transferencia(int iD) : base(iD) { }

        //Atributos
        [Required]
        public int NumeroCuenta { get; set; }
    }
}
