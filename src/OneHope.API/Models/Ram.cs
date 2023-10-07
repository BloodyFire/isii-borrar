namespace OneHope.API.Models
{
    public class Ram
    {
        public Ram() { }

        public Ram(int id, string nombre, IList<Portatil> portatiles)
        {
            Id = id;
            Nombre = nombre;
            Portatiles = portatiles;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        
        public IList<Portatil> Portatiles { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Ram ram &&
                   Id == ram.Id &&
                   Nombre == ram.Nombre &&
                   EqualityComparer<IList<Portatil>>.Default.Equals(Portatiles, ram.Portatiles);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Portatiles);
        }
    }
}
