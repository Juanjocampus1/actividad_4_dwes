using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad_4.Data;
using Actividad_4.Models;
using Actividad_4.Services;

namespace Actividad_4.Controllers
{
    public class PublicacionesController : Controller
    {
        private readonly IPublicacionService _publicacionService;

        public PublicacionesController(IPublicacionService publicacionService)
        {
            _publicacionService = publicacionService;
        }

        // GET: Publicaciones
        public async Task<IActionResult> Index(string searchString, int? añoDesde, int? añoHasta, int? proyectoId)
        {
            ViewBag.Proyectos = await _publicacionService.GetProyectosAsync();
            ViewData["CurrentFilter"] = searchString;
            ViewData["AñoDesde"] = añoDesde;
            ViewData["AñoHasta"] = añoHasta;
            ViewData["ProyectoId"] = proyectoId;

            var publicaciones = await _publicacionService.GetPublicacionesAsync(searchString, añoDesde, añoHasta, proyectoId);
            return View(publicaciones);
        }

        // GET: Publicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _publicacionService.GetPublicacionByIdAsync(id.Value);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }
    }
}