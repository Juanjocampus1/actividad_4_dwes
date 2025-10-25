using Actividad_4.Models;
using Microsoft.EntityFrameworkCore;
using Actividad_4.Data;

namespace Actividad_4.Services
{
    public interface IInstitucionService
    {
        Task<IEnumerable<Institucion>> GetInstitucionesAsync(string? searchString);
        Task<Institucion?> GetInstitucionByIdAsync(int id);
    }

    public class InstitucionService : IInstitucionService
    {
        private readonly ApplicationDbContext _context;

        public InstitucionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Institucion>> GetInstitucionesAsync(string? searchString)
        {
            var query = _context.Instituciones.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(inst => inst.Nombre.Contains(searchString) || inst.Ubicacion.Contains(searchString));
            }

            return await query.Include(inst => inst.Investigadores).ToListAsync();
        }

        public async Task<Institucion?> GetInstitucionByIdAsync(int id)
        {
            return await _context.Instituciones
                .Include(inst => inst.Investigadores).ThenInclude(i => i.InvestigadorProyectos).ThenInclude(ip => ip.Proyecto)
                .FirstOrDefaultAsync(inst => inst.Id == id);
        }
    }
}