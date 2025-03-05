using Microsoft.EntityFrameworkCore;
using TecnySystem.Models;

namespace TecnySystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // 🔹 Definición de tablas (DbSet)
        public DbSet<Desc_Prendas> DescPrendas { get; set; }
        public DbSet<Prendas> Prendas { get; set; }
        public DbSet<Prendas_Fallas> PrendasFallas { get; set; }
        public DbSet<Talla> Tallas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Configuración de relaciones con claves foráneas
            modelBuilder.Entity<Prendas>()
                .HasOne(p => p.DescPrenda)
                .WithMany(d => d.Prendas)
                .HasForeignKey(p => p.IdDescPrenda)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Talla>()
                .HasOne(t => t.DescPrenda)
                .WithMany(d => d.Tallas)
                .HasForeignKey(t => t.IdDescPrenda)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prendas_Fallas>()
                .HasOne(pf => pf.Prenda)
                .WithMany(p => p.PrendasFallas)
                .HasForeignKey(pf => pf.IdPrenda)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
