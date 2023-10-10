using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.Models
{
    public class Marca
    {
        //Constructores
        public Marca() { }

        public Marca(string nombre) {
            Nombre = nombre;
        }

        //Atributos
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La marca no puede ser superior a 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        //Metodos
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
