using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.Models
{
    public class RAM
    {
        public RAM() {}

        public RAM(string nombre) {
            Nombre = nombre;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "La RAM no puede ser superior a 20 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        public override bool Equals(object? obj)
        {
            return obj is RAM rAM &&
                   Id == rAM.Id &&
                   Nombre == rAM.Nombre;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre);
        }
    }
}
