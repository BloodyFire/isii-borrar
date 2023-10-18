using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.Models
{
    public class Ram
    {

        public Ram() { }

        public Ram(string capacidad) {
            Capacidad = capacidad;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "La RAM no puede ser superior a 20 caracteres.")]
        public string Capacidad { get; set; } = string.Empty;
        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        public override bool Equals(object? obj)
        {
            return obj is Ram ram &&
                   Id == ram.Id &&
                   Capacidad == ram.Capacidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Capacidad);
        }
    }
}
