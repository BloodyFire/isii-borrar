using System.ComponentModel.DataAnnotations;

namespace OneHope.Design.Models
{
    public class Procesador
    {

        public Procesador() { }

        public Procesador(string modeloProcesador) {
            ModeloProcesador = modeloProcesador;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El procesador no puede ser superior a 20 caracteres.")]
        public string ModeloProcesador { get; set; } = string.Empty;
        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        public override bool Equals(object? obj)
        {
            return obj is Procesador procesador &&
                   Id == procesador.Id &&
                   ModeloProcesador == procesador.ModeloProcesador;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ModeloProcesador);
        }

    }
}
