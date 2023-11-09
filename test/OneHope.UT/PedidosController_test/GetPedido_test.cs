namespace OneHope.UT.PedidosController_test
{
    public class GetPedido_test : OneHope4SqliteUT
    {
        public GetPedido_test()
        {
            var procesador = new Procesador("Intel I7 13700");
            var ram = new Ram("8Gb");
            var marca = new Marca("HP");
            var proveedor = new Proveedor(1, "Proveedores S.L.", "44444444A", "Calle providia numero 75", "proveemos@proveedor.com", "600000000");
            var portatil = new Portatil(id: 1, modelo: "HP-1151", procesador: procesador, ram: ram, marca: marca, nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 50.00, stock: 0, stockAlquilar: 5, proveedor: proveedor);
            var pedidos = new List<Pedido> {
                new Pedido(1, 50.0, new DateTime(1999, 1, 13), "Daniel.Tomas", "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedido>(), TipoMetodoPago.Transferencia),
                new Pedido(2, 100.0, new DateTime(2020, 10, 10), "Daniel.Tomas", "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedido>(), TipoMetodoPago.Transferencia, "Aqui un comentario.")
            };
            var linea1 = new LineaPedido(portatil, 1, pedidos[0], 50.0);
            pedidos[0].LineasPedido.Add(linea1);
            var linea2 = new LineaPedido(portatil, 1, pedidos[1], 50.0);
            pedidos[1].LineasPedido.Add(linea2);

            _context.Add(procesador);
            _context.Add(ram);
            _context.Add(marca);
            _context.Add(proveedor);
            _context.Add(portatil);
            _context.AddRange(pedidos);
            _context.SaveChanges(); //maybe async?
        }

    }
}
