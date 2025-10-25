using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad_4.Data;
using Actividad_4.Models;
using Actividad_4.Services;

namespace Actividad_4.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly IProyectoService _proyectoService;

        public ProyectosController(IProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index(string searchString, DateTime? fechaInicioDesde, DateTime? fechaInicioHasta)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["FechaInicioDesde"] = fechaInicioDesde?.ToString("yyyy-MM-dd");
            ViewData["FechaInicioHasta"] = fechaInicioHasta?.ToString("yyyy-MM-dd");

            var proyectos = await _proyectoService.GetProyectosAsync(searchString, fechaInicioDesde, fechaInicioHasta);
            return View(proyectos);
        }

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _proyectoService.GetProyectoByIdAsync(id.Value);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }
    }
}