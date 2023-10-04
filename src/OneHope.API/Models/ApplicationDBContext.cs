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
            builder.Entity<RAM>().HasAlternateKey(r => new { r.Nombre });
            builder.Entity<Marca>().HasAlternateKey(m => new { m.Nombre });
        }

        public DbSet<Procesador> Procesadores { get; set; }
        public DbSet<RAM> RAMs { get; set; }
        public DbSet<Marca> Marcas { get; set; }
    }
}

