using Actividad_4.Models;
using Microsoft.EntityFrameworkCore;
using Actividad_4.Data;

namespace Actividad_4.Services
{
    public interface IInvestigadorService
    {
        Task<IEnumerable<Investigador>> GetInvestigadoresAsync(string? searchString, int? institucionId);
        Task<Investigador?> GetInvestigadorByIdAsync(int id);
        Task<IEnumerable<Institucion>> GetInstitucionesAsync();
    }

    public class InvestigadorService : IInvestigadorService
    {
        private readonly ApplicationDbContext _context;

        public InvestigadorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Investigador>> GetInvestigadoresAsync(string? searchString, int? institucionId)
        {
            var query = _context.Investigadores.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(i => i.Nombre.Contains(searchString) || i.Apellido.Contains(searchString) || i.Email.Contains(searchString));
            }

            if (institucionId.HasValue)
            {
                query = query.Where(i => i.InstitucionId == institucionId);
            }

            return await query.Include(i => i.Institucion).ToListAsync();
        }

        public async Task<Investigador?> GetInvestigadorByIdAsync(int id)
        {
            return await _context.Investigadores
                .Include(i => i.Institucion)
                .Include(i => i.InvestigadorProyectos).ThenInclude(ip => ip.Proyecto)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Institucion>> GetInstitucionesAsync()
        {
            return await _context.Instituciones.AsNoTracking().ToListAsync();
        }
    }
}