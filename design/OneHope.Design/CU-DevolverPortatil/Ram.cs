using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class Ram
    {

        public Ram() { }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(60, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Nombre { get; set; }

        public IList<Portatil> Portatiles { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Ram ram &&
                   Id == ram.Id &&
                   Nombre == ram.Nombre;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre);
        }
    }
}
