namespace OneHope.UT.PedidosController_test
{
    public class CreatePedido_test : OneHope4SqliteUT
    {
        public CreatePedido_test()
        {
            var procesadores = new List<Procesador>() {
                new Procesador("Intel I7 13700")
            };
            var rams = new List<Ram>() {
                new Ram("8Gb")
            };
            var marcas = new List<Marca>() {
                new Marca("HP"),
                new Marca("DELL")
            };
            var proveedores = new List<Proveedor>() {
                new Proveedor(1, "Proveedores S.L.", "44444444A", "Calle providia numero 75", "proveemos@proveedores.com", "600000000"),
                new Proveedor(2, "Portatiles Mayorista", "12345678T", "Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000", "pormay@yahoorespuestas.com", "999555666")
            };

            var portatiles = new List<Portatil>()
            {
                new Portatil(id: 1, modelo: "HP-1151", procesador: procesadores[0], ram: rams[0], marca: marcas[0], nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 50.00, stock: 0, stockAlquilar: 5, proveedor: proveedores[0]),
                new Portatil(2, "DELL-1244", procesadores[0], rams[0], marcas[1], "DELL R5 gama alta", 1999.95, 66.66, 500.00, 1, 1, proveedores[0]),
                new Portatil(3, "ASUS-1362", procesadores[0], rams[0], marcas[0], "portatilote grandote marca asus perfecto para ir de camping.", 699.95, 23.33, 175.00, 5, 5, proveedores[1]),
                new Portatil(4, "TOASTER-1421", procesadores[0], rams[0], marcas[1], "Portatil de 82 pulgadas. (Para empotrar en la pared)", 1299.95, 43.33, 325.00, 15, 6, proveedores[1])
            };
            _context.AddRange(procesadores);
            _context.AddRange(rams);
            _context.AddRange(marcas);
            _context.AddRange(proveedores);
            _context.AddRange(portatiles);
            _context.SaveChanges(); //maybe async?
        }

    }
}
