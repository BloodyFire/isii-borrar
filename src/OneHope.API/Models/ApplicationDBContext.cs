using Microsoft.EntityFrameworkCore;
namespace OneHope.API.Models
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Linea_Compra> Linea_Compras { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Linea_Compra>().HasAlternateKey(pi => new { pi.Id_Prod, pi.Id_Compra });

            builder.Entity<Linea_Pedido>().HasAlternateKey(pi => new { pi.Id_Prod, pi.Id_Linea });
        }

        public DbSet<Compra> Compras { get; set; }
        public DbSet<Portatil> Portatiles { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Procesador> Procesadores { get; set; }
        public DbSet<Marca> Marcas { get; set; }

    }
}

