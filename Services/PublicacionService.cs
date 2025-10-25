using Actividad_4.Models;
using Microsoft.EntityFrameworkCore;
using Actividad_4.Data;

namespace Actividad_4.Services
{
    public interface IPublicacionService
    {
        Task<IEnumerable<Publicacion>> GetPublicacionesAsync(string? searchString, int? añoDesde, int? añoHasta, int? proyectoId);
        Task<Publicacion?> GetPublicacionByIdAsync(int id);
        Task<IEnumerable<Proyecto>> GetProyectosAsync();
    }

    public class PublicacionService : IPublicacionService
    {
        private readonly ApplicationDbContext _context;

        public PublicacionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publicacion>> GetPublicacionesAsync(string? searchString, int? añoDesde, int? añoHasta, int? proyectoId)
        {
            var query = _context.Publicaciones.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(pub => pub.Titulo.Contains(searchString) || pub.Revista.Contains(searchString));
            }

            if (añoDesde.HasValue)
            {
                query = query.Where(pub => pub.Año >= añoDesde);
            }

            if (añoHasta.HasValue)
            {
                query = query.Where(pub => pub.Año <= añoHasta);
            }

            if (proyectoId.HasValue)
            {
                query = query.Where(pub => pub.ProyectoId == proyectoId);
            }

            return await query.Include(pub => pub.Proyecto).ToListAsync();
        }

        public async Task<Publicacion?> GetPublicacionByIdAsync(int id)
        {
            return await _context.Publicaciones
                .Include(pub => pub.Proyecto)
                .FirstOrDefaultAsync(pub => pub.Id == id);
        }

        public async Task<IEnumerable<Proyecto>> GetProyectosAsync()
        {
            return await _context.Proyectos.AsNoTracking().ToListAsync();
        }
    }
}