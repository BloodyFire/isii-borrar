using System.ComponentModel.DataAnnotations;

namespace OneHope.API.Models
{
    public class Procesador
    {

        public Procesador() { }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(60, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Nombre { get; set; }

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
