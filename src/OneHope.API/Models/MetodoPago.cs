namespace OneHope.API.Models
{
    public class MetodoPago
    {
        public MetodoPago(int id)
        {
            Id = id;
        }

        [Key]
        public int Id { get; set; }

    }
}