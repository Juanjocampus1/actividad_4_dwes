using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad_4.Data;
using Actividad_4.Models;
using Actividad_4.Services;

namespace Actividad_4.Controllers
{
    public class InstitucionesController : Controller
    {
        private readonly IInstitucionService _institucionService;

        public InstitucionesController(IInstitucionService institucionService)
        {
            _institucionService = institucionService;
        }

        // GET: Instituciones
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var instituciones = await _institucionService.GetInstitucionesAsync(searchString);
            return View(instituciones);
        }

        // GET: Instituciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institucion = await _institucionService.GetInstitucionByIdAsync(id.Value);
            if (institucion == null)
            {
                return NotFound();
            }

            return View(institucion);
        }
    }
}