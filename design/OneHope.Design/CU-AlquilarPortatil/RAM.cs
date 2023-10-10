using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class RAM
    {
        //Constructores
        public RAM() { }
        public RAM(int iD, string nombre) 
        {
            ID = iD;
            Nombre = nombre;
        }

        //Atributos
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "La RAM no puede ser mayor a 25 caracteres.")]
        public string Nombre { get; set;}

        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is RAM rAM && ID == rAM.ID &&
                   Nombre == rAM.Nombre;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Nombre);
        }
    }
}
