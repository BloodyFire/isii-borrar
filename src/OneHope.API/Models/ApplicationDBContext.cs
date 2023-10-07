﻿using Microsoft.EntityFrameworkCore;
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
        }

        public DbSet<Portatil> Portatiles { get; set; }

        public DbSet<Devolucion> Devolucion { get; set; }

        public DbSet<LineaDevolucion> LineaDevolucion { get; set; }
    }

}

