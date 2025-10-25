using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad_4.Data;
using Actividad_4.Models;
using Actividad_4.Services;

namespace Actividad_4.Controllers
{
    public class InvestigadoresController : Controller
    {
        private readonly IInvestigadorService _investigadorService;

        public InvestigadoresController(IInvestigadorService investigadorService)
        {
            _investigadorService = investigadorService;
        }

        // GET: Investigadores
        public async Task<IActionResult> Index(string searchString, int? institucionId)
        {
            ViewBag.Instituciones = await _investigadorService.GetInstitucionesAsync();
            ViewData["CurrentFilter"] = searchString;
            ViewData["InstitucionId"] = institucionId;

            var investigadores = await _investigadorService.GetInvestigadoresAsync(searchString, institucionId);
            return View(investigadores);
        }

        // GET: Investigadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigador = await _investigadorService.GetInvestigadorByIdAsync(id.Value);
            if (investigador == null)
            {
                return NotFound();
            }

            return View(investigador);
        }
    }
}