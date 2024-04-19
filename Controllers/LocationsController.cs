using PRACTICA__.Data;
using PRACTICA__.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PRACTICA__.Controllers
{
    public class LocationsController : Controller
    {
        // GET: LocationsController

        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: LocationsController
        public ActionResult Index()
        {
            var Datos = _context.locations.Include(c => c.Countries).ToList();
            return View(Datos);
        }

        // GET: LocationsController/Create
        public async Task<IActionResult> Create()
        {
            // Obtener la lista de regiones desde la base de datos
            var countries = await _context.countries.ToListAsync();

            // Convertir la lista de regiones a una lista de objetos SelectListItem
            var countryItems = countries.Select(r => new SelectListItem
            {
                Value = r.COUNTRY_ID, // El valor de la opción será el ID de la región
                Text = $"{r.COUNTRY_ID} - {r.COUNTRY_NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();

            // Agregar una opción por defecto al inicio del menú desplegable
            countryItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a country --" });

            // Pasar la lista de opciones de regiones a la vista
            ViewBag.COUNTRY_ID = countryItems;

            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Locations locations)
        {
            if (ModelState.IsValid)
            {
                _context.locations.Add(locations);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(locations);
        }

        // GET: ProductsController/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            // Obtener la lista de regiones disponibles
            var countries = await _context.countries.ToListAsync();

            // Convertir la lista de regiones a una lista de objetos SelectListItem
            var categoriesItems = countries.Select(r => new SelectListItem
            {
                Value = r.COUNTRY_ID, // El valor de la opción será el ID de la región
                Text = $"{r.COUNTRY_ID} - {r.COUNTRY_NAME}" // El texto de la opción será el nombre de la región
            }).ToList();

            // Pasar la lista de opciones de regiones a la vista
            ViewBag.COUNTRY_ID = categoriesItems;

            return View(location);
        }

        // POST: ProductsController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Locations locations)
        {
            if (id != locations.LOCATION_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists((int)locations.LOCATION_ID))
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
            return View(locations);
        }

        // GET: CountriesController/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el país con la región asociada
            var location = await _context.locations
                                    .Include(c => c.Countries)
                                    .FirstOrDefaultAsync(c => c.LOCATION_ID == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: CountriesController/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el producto con la categoria asociada
            var location = await _context.locations
                                    .Include(c => c.Countries)
                                    .FirstOrDefaultAsync(c => c.LOCATION_ID == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }


        // POST: CountriesController/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.locations.FindAsync(id);
            _context.locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar si existe un país
        private bool LocationExists(int id)
        {
            return _context.locations.Any(e => e.LOCATION_ID == id);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerDatos()
        {
            var todos = await _context.locations.Include(c => c.Countries).ToListAsync();
            return Json(new { data = todos });
        }
    }
}
