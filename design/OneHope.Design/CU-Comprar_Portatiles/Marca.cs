
using System.ComponentModel.DataAnnotations;


namespace OneHope.Design
{
    public class Marca
    {
        public Marca() { }

        public Marca(string nombre) : base()
        {
            Nombre = nombre;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public virtual string Nombre { get; set; } = string.Empty;

        public IList<Portatil> Portatiles { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Marca marca &&
                   Id == marca.Id &&
                   Nombre == marca.Nombre;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre);
        }
    }
}
