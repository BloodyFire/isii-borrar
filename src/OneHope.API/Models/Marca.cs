namespace OneHope.API.Models
{
    public class Marca
    {

        public Marca() { }

        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.Currency), Range(1, 100, ErrorMessage = "Minimum is 1 and 100, respectively")]
        public string Nombre { get; set; }

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
