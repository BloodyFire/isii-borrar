using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.CUComprarPortatil
{
    public class Marca
    {
        //Constructores
        public Marca() { }

        public Marca(string valor) {
            Valor = valor;
        }

        //Atributos
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La marca no puede ser superior a 50 caracteres.")]
        public string Valor { get; set; } = string.Empty;
        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is Marca marca &&
                   Id == marca.Id &&
                   Valor == marca.Valor;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Valor);
        }

    }
}
