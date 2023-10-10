namespace OneHope.API.Models
{
    public class Proveedor
    {
        public Proveedor(int id, string nombre, string cIF, string direccion, string email, string telefono)
        {
            Id = id;
            Nombre = nombre;
            CIF = cIF;
            Direccion = direccion;
            Email = email;
            Telefono = telefono;
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Nombre Comercial")]
        public string Nombre { get; set; }

        [Required]
        public string CIF { get; set; }

        [Required]
        [DataType(DataType.MultilineText), Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Telefono { get; set; } = string.Empty;

        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        public override bool Equals(object? obj)
        {
            return obj is Proveedor proveedor &&
                   Id == proveedor.Id &&
                   Nombre == proveedor.Nombre &&
                   CIF == proveedor.CIF &&
                   Direccion == proveedor.Direccion &&
                   Email == proveedor.Email &&
                   Telefono == proveedor.Telefono;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, CIF, Direccion, Email, Telefono);
        }
    }
}
