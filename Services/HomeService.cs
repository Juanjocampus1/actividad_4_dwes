using Actividad_4.Models;
using Actividad_4.Data;
using Microsoft.EntityFrameworkCore;

namespace Actividad_4.Services
{
    public interface IHomeService
    {
        Task<HomeViewModel> GetHomeViewModelAsync();
    }

    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var totalInstituciones = await _context.Instituciones.CountAsync();
            var totalInvestigadores = await _context.Investigadores.CountAsync();
            var totalProyectos = await _context.Proyectos.CountAsync();
            var totalPublicaciones = await _context.Publicaciones.CountAsync();
            var presupuestoTotal = await _context.Proyectos.SumAsync(p => p.Presupuesto);
            var publicacionesUltimoAño = await _context.Publicaciones.CountAsync(p => p.Año == DateTime.Now.Year);
            var promedioPresupuesto = totalProyectos > 0 ? await _context.Proyectos.AverageAsync(p => p.Presupuesto) : 0;
            var proyectosActivos = await _context.Proyectos.CountAsync(p => !p.FechaFin.HasValue || p.FechaFin > DateTime.Now);

            return new HomeViewModel
            {
                TotalInstituciones = totalInstituciones,
                TotalInvestigadores = totalInvestigadores,
                TotalProyectos = totalProyectos,
                TotalPublicaciones = totalPublicaciones,
                PresupuestoTotal = presupuestoTotal,
                PublicacionesUltimoAño = publicacionesUltimoAño,
                PromedioPresupuesto = promedioPresupuesto,
                ProyectosActivos = proyectosActivos
            };
        }
    }
}