using Microsoft.EntityFrameworkCore;
using Actividad_4.Models;

namespace Actividad_4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Investigador> Investigadores { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<InvestigadorProyecto> InvestigadorProyectos { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvestigadorProyecto>()
                .HasKey(ip => new { ip.InvestigadorId, ip.ProyectoId });

            modelBuilder.Entity<InvestigadorProyecto>()
                .HasOne(ip => ip.Investigador)
                .WithMany(i => i.InvestigadorProyectos)
                .HasForeignKey(ip => ip.InvestigadorId);

            modelBuilder.Entity<InvestigadorProyecto>()
                .HasOne(ip => ip.Proyecto)
                .WithMany(p => p.InvestigadorProyectos)
                .HasForeignKey(ip => ip.ProyectoId);
        }
    }
}