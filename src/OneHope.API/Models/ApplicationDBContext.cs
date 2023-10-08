using Microsoft.EntityFrameworkCore;
namespace OneHope.API.Models
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<LineaAlquiler> LineasAlquiler { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LineaAlquiler>().HasAlternateKey(la => new { la.AlquilerID, la.PortatilID });
        }
    }
}

