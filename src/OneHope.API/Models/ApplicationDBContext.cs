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
            builder.Entity<Procesador>().HasAlternateKey(p => new { p.Nombre });
            builder.Entity<Ram>().HasAlternateKey(r => new { r.Nombre });
            builder.Entity<Marca>().HasAlternateKey(m => new { m.Nombre });
            builder.Entity<Portatil>().HasAlternateKey(m => new { m.Modelo });
            //Clave compuesta para las lineas de pedido.
            builder.Entity<LineaPedido>().HasKey(lp => new { lp.PortatilId, lp.PedidoId });
            //Clave alternativa para los proveedores
            builder.Entity<Proveedor>().HasAlternateKey(p => new { p.CIF });
            
            builder.Entity<MetodoPago>().
                HasDiscriminator<string>("TipoMetodoPago")
                .HasValue<MetodoPago>("MetodoPago")
                .HasValue<TarjetaCredito>("MetodoPago_Tarjeta")
                .HasValue<PayPal>("MetodoPago_PayPal")
                .HasValue<Transferencia>("MetodoPago_Transferencia");
        }

        public DbSet<Procesador> Procesadores { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Portatil> Portatiles { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
    }
}

