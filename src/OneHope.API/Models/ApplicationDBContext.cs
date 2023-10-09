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
            builder.Entity<LineaDevolucion>().HasAlternateKey(pi => new {pi.IdDevolucion, pi.PortatilId});
        }

        public DbSet<Portatil> Portatiles { get; set; }

        public DbSet<Devolucion> Devolucion { get; set; }

        public DbSet<Procesador> Procesador { get; set; }

        public DbSet<Ram> Ram { get; set; }

        public DbSet<Marca> Marca { get; set; }

        public DbSet<Compra> Compra { get; set; }

        public DbSet<LineaCompra> LineaCompra{ get; set; }

        public DbSet<MetodoPago> MetodoPago { get; set; }
    }

}

