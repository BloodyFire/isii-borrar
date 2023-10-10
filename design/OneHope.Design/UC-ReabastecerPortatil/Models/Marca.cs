using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.Models
{
    public class Marca
    {
        public Marca() {}

        public Marca(string nombre) {
            Nombre = nombre;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La marca no puede ser superior a 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();
        public override bool Equals(object? obj)
        {
            return obj is Marca proc &&
                   Id == proc.Id &&
                   Nombre == proc.Nombre;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre);
        }

    }
}
