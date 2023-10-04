namespace OneHope.API.Models
{
    public class Procesador
    {
        public Procesador() {}

        public Procesador(string nombre) {
            Nombre = nombre;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "El procesador no puede ser superior a 20 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        public override bool Equals(object? obj)
        {
            return obj is Procesador proc &&
                   Id == proc.Id &&
                   Nombre == proc.Nombre;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre);
        }

    }
}
