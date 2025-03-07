using Microsoft.EntityFrameworkCore;
using TecnySystem.Models;

namespace TecnySystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // 🔹 Definición de tablas (DbSet)
        public DbSet<TallaFallaTela> FTela { get; set; }
        public DbSet<TallaFallaLavanderia> FLavanderia { get; set; }
        public DbSet<Desc_Prendas> DescPrendas { get; set; }
        public DbSet<Prenda> Prendas { get; set; }
        public DbSet<FallaLavanderia> FallasLavanderia { get; set; }
        public DbSet<FallaTela> FallasTela { get; set; }
        public DbSet<Talla> Tallas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            // Relación entre Prenda y DescPrenda (1:N) - Se eliminan las prendas si se elimina el lote
            modelBuilder.Entity<Prenda>()
                .HasOne(p => p.DescPrenda)
                .WithMany(d => d.Prendas)
                .HasForeignKey(p => p.id_desc_prenda)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Talla y DescPrenda (1:N) - Se eliminan las tallas si se elimina el lote
            modelBuilder.Entity<Talla>()
                .HasOne(t => t.DescPrenda)
                .WithMany(d => d.Tallas)
                .HasForeignKey(t => t.id_desc_prenda)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre FallaTela y DescPrenda (1:N) - No se eliminan fallas si se elimina un lote
            modelBuilder.Entity<FallaTela>()
                .HasOne(ft => ft.DescPrenda)
                .WithMany(d => d.FallasTela)
                .HasForeignKey(ft => ft.id_desc_prenda)
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada

            // Relación entre FallaLavanderia y DescPrenda (1:N) - No se eliminan fallas si se elimina un lote
            modelBuilder.Entity<FallaLavanderia>()
                .HasOne(fl => fl.DescPrenda)
                .WithMany(d => d.FallasLavanderia)
                .HasForeignKey(fl => fl.id_desc_prenda)
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada

            // Relación entre TallaFallaTela y FallaTela (1:N) - No se eliminan registros de fallas si se elimina la falla
            modelBuilder.Entity<TallaFallaTela>()
                .HasOne(tft => tft.FTela)
                .WithMany(ft => ft.TallasFallaTela)
                .HasForeignKey(tft => tft.id_falla_tela)
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada

            // Relación entre TallaFallaLavanderia y FallaLavanderia (1:N) - No se eliminan registros de fallas si se elimina la falla
            modelBuilder.Entity<TallaFallaLavanderia>()
                .HasOne(tfl => tfl.FLavanderia)
                .WithMany(fl => fl.TallasFallaLavanderia)
                .HasForeignKey(tfl => tfl.id_falla_lavanderia)
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada

            modelBuilder.Entity<FallaTela>()
           .Property(f => f.estado)
           .HasConversion<string>();  // 👈 Esto soluciona el problema

        }


    }
}
