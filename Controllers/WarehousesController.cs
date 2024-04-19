using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRACTICA__.Data;
using PRACTICA__.Models;

namespace PRACTICA__.Controllers
{
    public class WarehousesController : Controller
    {
        // GET: WerehousesController

        private readonly ApplicationDbContext _context;
        public WarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var Datos = _context.warehouses.Include(c => c.Locations).ToList();
            return View(Datos);
        }

        // GET: WarehousesController/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el país con la región asociada
            var warehouse = await _context.warehouses
                                    .Include(c => c.Locations)
                                    .FirstOrDefaultAsync(c => c.WAREHOUSE_ID == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // POST: WarehousesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                _context.warehouses.Add(warehouses);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouses);
        }

        // GET: WarehousesController/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            // Obtener la lista de locations disponibles
            var locations = await _context.locations.ToListAsync();

            // Convertir la lista de locations a una lista de objetos SelectListItem
            var locationsItems = locations.Select(r => new SelectListItem
            {
                Value = r.LOCATION_ID.ToString(), // El valor de la opción será el ID del location
                Text = $"{r.LOCATION_ID}" // El texto de la opción será el nombre del location
            }).ToList();

            // Pasar la lista de opciones de regiones a la vista
            ViewBag.LOCATION_ID = locationsItems;

            return View(warehouse);
        }

        // POST: WarehousesController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Warehouses warehouses)
        {
            if (id != warehouses.WAREHOUSE_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists((int)warehouses.WAREHOUSE_ID))
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
            return View(warehouses);
        }

        // GET: WarehousesController/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el producto el warehouses asociada
            var warehouse = await _context.warehouses
                                    .Include(c => c.Locations)
                                    .FirstOrDefaultAsync(c => c.WAREHOUSE_ID == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }


        // POST: WarehousesController/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouse = await _context.warehouses.FindAsync(id);
            _context.warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar si existe un Warehouse
        private bool WarehouseExists(int id)
        {
            return _context.warehouses.Any(e => e.WAREHOUSE_ID == id);
        }
    }
}
