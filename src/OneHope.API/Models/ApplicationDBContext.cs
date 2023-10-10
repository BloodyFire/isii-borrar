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

            builder.Entity<LineaCompra>().HasAlternateKey(pi => new { pi.IdProd, pi.IdCompra });
            
        }

        public DbSet<Compra> Compras { get; set; }
        public DbSet<Portatil> Portatiles { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Procesador> Procesadores { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        
    }
}

