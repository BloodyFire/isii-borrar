namespace OneHope.API.Models
{
    public class Procesador
    {
        public Procesador() { }

        public Procesador(int id, string nombre, IList<Portatil> portatiles)
        {
            Id = id;
            Nombre = nombre;
            Portatiles = portatiles;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public virtual string Nombre { get; set; }

        public IList<Portatil> Portatiles { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Procesador procesador &&
                   Id == procesador.Id &&
                   Nombre == procesador.Nombre &&
                   EqualityComparer<IList<Portatil>>.Default.Equals(Portatiles, procesador.Portatiles);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Portatiles);
        }
    }
}
