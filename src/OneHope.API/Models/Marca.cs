namespace OneHope.API.Models
{
    public class Marca
    {
        //Constructores
        public Marca() { }

        public Marca(int ID, string Nombre) 
        {
            this.ID = ID;
            this.Nombre = Nombre;
        }

        //Atributos
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "La marca no puede tener mas de 75 caracteres.")]
        public string Nombre { get; set;}

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is Marca proc && ID == proc.ID &&
                Nombre == proc.Nombre;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Nombre);
        }

    }
}
