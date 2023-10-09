using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

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

            builder.Entity<LineaAlquiler>().HasAlternateKey(la => new { la.AlquilerID, la.PortatilID });

            builder.Entity<MetodoPago>().
                HasDiscriminator<string>("TipoMetodoPago")
                .HasValue<MetodoPago>("MetodoPago")
                .HasValue<TarjetaCredito>("Tarjeta")
                .HasValue<PayPal>("PayPal")
                .HasValue<Transferencia>("Transferencia");
        }

        public DbSet<LineaAlquiler> LineasAlquiler { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Alquiler> Alquilers { get; set; }
        public DbSet<Portatil> Portatiles { get; set; }
        public DbSet<RAM> RAMs { get; set; }
        public DbSet<Procesador> Procesadores { get; set; }
        public DbSet<Marca> Marcas { get; set; }
    }
}

