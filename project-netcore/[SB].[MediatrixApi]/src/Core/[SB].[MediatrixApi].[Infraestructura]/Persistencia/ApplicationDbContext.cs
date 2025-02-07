using _SB_._MediatrixApi_._Dominio_.Entidades;
using Microsoft.EntityFrameworkCore;

namespace _SB_._MediatrixApi_._Infraestructura_.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoriaEntidad> CategoriasEntidad { get; set; }
        public DbSet<EntidadGubernamental> EntidadesGubernamentales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaEntidad>(entity =>
            {
                entity.HasKey(e => e.CategoriaId);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.EstaEliminado).HasDefaultValue(false);
                
                entity.HasQueryFilter(e => !e.EstaEliminado);
            });

            modelBuilder.Entity<EntidadGubernamental>(entity =>
            {
                entity.HasKey(e => e.EntidadId);
                entity.Property(e => e.Nombre).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Siglas).HasMaxLength(20);
                entity.Property(e => e.Direccion).HasMaxLength(255).IsRequired();
                entity.Property(e => e.NombreEncargado).HasMaxLength(200);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.EstaEliminado).HasDefaultValue(false);

                entity.HasQueryFilter(e => !e.EstaEliminado);

                entity.HasOne(e => e.Categoria)
                      .WithMany(c => c.EntidadesGubernamentales)
                      .HasForeignKey(e => e.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
} 