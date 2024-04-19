using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRACTICA__.Data;
using PRACTICA__.Models;

namespace PRACTICA__.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: InventoriesController
        public ActionResult Index()
        {
            var Datos = _context.inventories.Include(c => c.Product).Include(c => c.Warehouses).ToList();
            return View(Datos);
        }

        // GET: InventoriesController/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el inventory con la región asociada
            var inventory = await _context.inventories
                                    .Include(c => c.Product)
                                    .Include(c => c.Warehouses)
                                    .FirstOrDefaultAsync(c => c.QUANTITY == id);

            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        /// GET: InventoriesController/Create
        public async Task<IActionResult> Create()
        {
            // Obtener la lista de productos desde la base de datos
            var products = await _context.products.ToListAsync();

            // Convertir la lista de productos a una lista de objetos SelectListItem
            var productItems = products.Select(p => new SelectListItem
            {
                Value = p.PRODUCT_ID.ToString(), // El valor de la opción será el ID del producto
                Text = $"{p.PRODUCT_ID} - {p.PRODUCT_NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();

            // Agregar una opción por defecto al inicio del menú desplegable
            productItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a product --" });

            // Pasar la lista de opciones de productos a la vista
            ViewBag.PRODUCT_ID = productItems;

            // Obtener la lista de almacenes desde la base de datos
            var warehouses = await _context.warehouses.ToListAsync();

            // Convertir la lista de almacenes a una lista de objetos SelectListItem
            var warehouseItems = warehouses.Select(w => new SelectListItem
            {
                Value = w.WAREHOUSE_ID.ToString(), // El valor de la opción será el ID del almacén
                Text = $"{w.WAREHOUSE_ID} - {w.WAREHOUSE_NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();

            // Agregar una opción por defecto al inicio del menú desplegable
            warehouseItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a warehouse --" });

            // Pasar la lista de opciones de almacenes a la vista
            ViewBag.WAREHOUSE_ID = warehouseItems;

            return View();
        }


        // POST: InventoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventories inventories)
        {
            if (ModelState.IsValid)
            {
                _context.inventories.Add(inventories);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(inventories);
        }

        // GET: InventoriesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            // Obtener la lista de productos desde la base de datos
            var products = await _context.products.ToListAsync();

            // Convertir la lista de productos a una lista de objetos SelectListItem
            var productItems = products.Select((p, index) => new SelectListItem
            {
                Value = p.PRODUCT_ID.ToString(),
                Text = $"{index + 1} - {p.PRODUCT_NAME}"
            }).ToList();

            // Obtener la lista de almacenes desde la base de datos
            var warehouses = await _context.warehouses.ToListAsync();

            // Convertir la lista de almacenes a una lista de objetos SelectListItem
            var warehouseItems = warehouses.Select((w, index) => new SelectListItem
            {
                Value = w.WAREHOUSE_ID.ToString(),
                Text = $"{index + 1} - {w.WAREHOUSE_NAME}"
            }).ToList();

            // Pasar las listas de opciones a la vista
            ViewBag.PRODUCT_ID = productItems;
            ViewBag.WAREHOUSE_ID = warehouseItems;

            return View(inventory);
        }

        // POST: InventoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Inventories inventory)
        {
            if (id != inventory.QUANTITY)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el inventario existente en lugar de insertar uno nuevo
                    _context.Entry(inventory).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.QUANTITY))
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
            return View(inventory);
        }




        private bool InventoryExists(int? QUANTITY)
        {
            throw new NotImplementedException();
        }

        private bool InventoryExists(int id)
        {
            return _context.inventories.Any(e => e.QUANTITY == id);
        }

        // GET: InventoriesController/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el país con la región asociada
            var inventory = await _context.inventories
                                    .Include(c => c.Product)
                                    .Include(c => c.Warehouses)
                                    .FirstOrDefaultAsync(c => c.QUANTITY == id);

            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }


        // POST: CountriesController/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.inventories.FindAsync(id);
            _context.inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
