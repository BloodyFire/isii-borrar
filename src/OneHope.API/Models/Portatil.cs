namespace OneHope.API.Models
{
    public class Portatil
    {
        //Atributos
        [Key]
        public int ID { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public float PrecioCompra { get; set; }
        [Required]
        public float PrecioAlq { get; set;}
        [Required]
        public float PrecioCoste { get; set; }
        [Required]
        public int Stock {  get; set; }
        [Required]
        public int StockAlq { get; set; }

        public int Proveedor { get; set;}
        //Constructores
        public Portatil() { }

        public Portatil(int iD, string modelo, float precioCompra, float precioAlq, float precioCoste, int stock, int stockAlq, int proveedor)
        {
            ID = iD;
            Modelo = modelo;
            PrecioCompra = precioCompra;
            PrecioAlq = precioAlq;
            PrecioCoste = precioCoste;
            Stock = stock;
            StockAlq = stockAlq;
            Proveedor = proveedor;
        }
    }
}
