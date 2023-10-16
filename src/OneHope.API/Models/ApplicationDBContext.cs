using Microsoft.EntityFrameworkCore;
namespace OneHope.API.Models
{
    public class ApplicationDBContext : DbContext
    {
        
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            //Claves alternativas de los atributos multievaluados. No se pueden repetir en nombre.
            builder.Entity<Procesador>().HasAlternateKey(p => new { p.ModeloProcesador });
            builder.Entity<Ram>().HasAlternateKey(r => new { r.Capacidad });
            builder.Entity<Marca>().HasAlternateKey(m => new { m.NombreMarca });
            builder.Entity<Portatil>().HasAlternateKey(m => new { m.Modelo });
            //Clave compuesta para las lineas de pedido.
            builder.Entity<LineaPedido>().HasKey(lp => new { lp.PortatilId, lp.PedidoId });
            //Clave alternativa para los proveedores
            builder.Entity<Proveedor>().HasAlternateKey(p => new { p.CIF });

            builder.Entity<LineaAlquiler>().HasAlternateKey(la => new { la.AlquilerID, la.PortatilID });
            builder.Entity<LineaCompra>().HasAlternateKey(pi => new { pi.IdProd, pi.IdCompra });
            
            builder.Entity<LineaDevolucion>().HasAlternateKey(pi => new {pi.IdDevolucion, pi.LineaCompraId});
        }

        public DbSet<Procesador> Procesadores { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Portatil> Portatiles { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
        public DbSet<Alquiler> Alquileres { get; set; }

    }

}

