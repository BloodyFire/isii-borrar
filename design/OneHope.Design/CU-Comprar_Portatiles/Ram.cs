namespace OneHope.API.Models
{
    public class Ram
    {
        public Ram() { }

        public Ram(string nombre):base()
        {
            Nombre = nombre;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        
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
