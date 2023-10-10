namespace OneHope.API.Models
{
    public class Procesador
    {
        //Constructores
        public Procesador() { }

        public Procesador(int iD, string nombre) 
        {
            ID = iD;
            Nombre = nombre;
        }

        //Atributos
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El procesador no puede tener mas de 30 caracteres.")]
        public string Nombre { get; set;}

        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is Procesador proc && ID == proc.ID &&
                   Nombre == proc.Nombre;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Nombre);
        }
    }
}
