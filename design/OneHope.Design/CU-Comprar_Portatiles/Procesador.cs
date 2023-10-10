
using System.ComponentModel.DataAnnotations;


namespace OneHope.Design
{
    public class Procesador
    {
        public Procesador() { }

        public Procesador(string nombre):base()
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
            return obj is Procesador procesador &&
                   Id == procesador.Id &&
                   Nombre == procesador.Nombre;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre);
        }
    }
}
