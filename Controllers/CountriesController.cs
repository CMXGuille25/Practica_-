using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRACTICA__.Data;
using PRACTICA__.Models;

namespace PRACTICA__.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CountriesController
        public ActionResult Index()
        {
            var Datos = _context.countries.Include(c =>c.Regions).ToList();
            return View(Datos);
        }


        // GET: CountriesController/Create
        public async Task<IActionResult> Create()
        {
            // Obtener la lista de regiones desde la base de datos
            var regions = await _context.regions.ToListAsync();

            // Convertir la lista de regiones a una lista de objetos SelectListItem
            var regionItems = regions.Select((r, index) => new SelectListItem
            {
                Value = r.REGION_ID.ToString(), // El valor de la opción será el ID de la región
                Text = $"{index + 1} - {r.REGION_NAME}" // El texto de la opción será "Número - Nombre"
            }).ToList();

            // Agregar una opción por defecto al inicio del menú desplegable
            regionItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a region --" });
            
            // Pasar la lista de opciones de regiones a la vista
            ViewBag.regions = regionItems;

            return View();
        }





        // POST: CountriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Countries countries)
        {
            if (ModelState.IsValid)
            {
                _context.countries.Add(countries);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(countries);
        }


        // GET: CountriesController/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            // Obtener la lista de regiones disponibles
            var regions = await _context.regions.ToListAsync();

            // Convertir la lista de regiones a una lista de objetos SelectListItem
            var regionItems = regions.Select((r, index) => new SelectListItem
            {
                Value = r.REGION_ID.ToString(), // El valor de la opción será el ID de la región
                Text = $"{index + 1} - {r.REGION_NAME}" // El texto de la opción será "Número - Nombre"
            }).ToList();

            // Pasar la lista de opciones de regiones a la vista
            ViewBag.regions = regionItems;

            return View(country);
        }


        // POST: CountriesController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Countries country)
        {
            if (id != country.COUNTRY_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.COUNTRY_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: CountriesController/Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el país con la región asociada
            var country = await _context.countries
                                    .Include(c => c.Regions)
                                    .FirstOrDefaultAsync(c => c.COUNTRY_ID == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: CountriesController/Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el país con la región asociada
            var country = await _context.countries
                                    .Include(c => c.Regions)
                                    .FirstOrDefaultAsync(c => c.COUNTRY_ID == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }


        // POST: CountriesController/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var country = await _context.countries.FindAsync(id);
            _context.countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar si existe un país
        private bool CountryExists(string id)
        {
            return _context.countries.Any(e => e.COUNTRY_ID == id);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerDatos()
        {
            var todos = await _context.countries.Include(c => c.Regions).ToListAsync();
            return Json(new { data = todos });
        }

    }
}
