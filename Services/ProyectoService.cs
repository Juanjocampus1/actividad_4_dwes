using Actividad_4.Models;
using Microsoft.EntityFrameworkCore;
using Actividad_4.Data;

namespace Actividad_4.Services
{
    public interface IProyectoService
    {
        Task<IEnumerable<Proyecto>> GetProyectosAsync(string? searchString, DateTime? fechaInicioDesde, DateTime? fechaInicioHasta);
        Task<Proyecto?> GetProyectoByIdAsync(int id);
    }

    public class ProyectoService : IProyectoService
    {
        private readonly ApplicationDbContext _context;

        public ProyectoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proyecto>> GetProyectosAsync(string? searchString, DateTime? fechaInicioDesde, DateTime? fechaInicioHasta)
        {
            var query = _context.Proyectos.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Titulo.Contains(searchString) || p.Descripcion.Contains(searchString));
            }

            if (fechaInicioDesde.HasValue)
            {
                query = query.Where(p => p.FechaInicio >= fechaInicioDesde);
            }

            if (fechaInicioHasta.HasValue)
            {
                query = query.Where(p => p.FechaInicio <= fechaInicioHasta);
            }

            return await query
                .Include(p => p.InvestigadorProyectos).ThenInclude(ip => ip.Investigador)
                .Include(p => p.Publicaciones)
                .ToListAsync();
        }

        public async Task<Proyecto?> GetProyectoByIdAsync(int id)
        {
            return await _context.Proyectos
                .Include(p => p.InvestigadorProyectos).ThenInclude(ip => ip.Investigador)
                .Include(p => p.Publicaciones)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}