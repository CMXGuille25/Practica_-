using PRACTICA__.Data;
using PRACTICA__.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PRACTICA__.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            var Datos = _context.products.Include(c => c.Product_categories).ToList();
            return View(Datos);
        }

        // GET: ProductsController/Create
        public async Task<IActionResult> Create()
        {
            // Obtener la lista de regiones desde la base de datos
            var categories = await _context.product_categories.ToListAsync();

            // Convertir la lista de regiones a una lista de objetos SelectListItem
            var categoryItems = categories.Select(r => new SelectListItem
            {
                Value = r.CATEGORY_ID.ToString(), // El valor de la opción será el ID de la región
                Text = $"{r.CATEGORY_ID} - {r.CATEGORY_NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();

            // Agregar una opción por defecto al inicio del menú desplegable
            categoryItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a category --" });

            // Pasar la lista de opciones de regiones a la vista
            ViewBag.CATEGORY_ID = categoryItems;

            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                _context.products.Add(products);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: ProductsController/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Obtener la lista de regiones disponibles
            var categories = await _context.product_categories.ToListAsync();

            // Convertir la lista de regiones a una lista de objetos SelectListItem
            var categoriesItems = categories.Select(r => new SelectListItem
            {
                Value = r.CATEGORY_ID.ToString(), // El valor de la opción será el ID de la región
                Text = $"{r.CATEGORY_ID} - {r.CATEGORY_NAME}" // El texto de la opción será el nombre de la región
            }).ToList();

            // Pasar la lista de opciones de regiones a la vista
            ViewBag.CATEGORY_ID = categoriesItems;

            return View(product);
        }

        // POST: ProductsController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Products products)
        {
            if (id != products.PRODUCT_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists((int)products.PRODUCT_ID))
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
            return View(products);
        }

        // GET: CountriesController/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el país con la región asociada
            var product = await _context.products
                                    .Include(c => c.Product_categories)
                                    .FirstOrDefaultAsync(c => c.PRODUCT_ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: CountriesController/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el producto con la categoria asociada
            var product = await _context.products
                                    .Include(c => c.Product_categories)
                                    .FirstOrDefaultAsync(c => c.PRODUCT_ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // POST: CountriesController/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.products.FindAsync(id);
            _context.products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar si existe un país
        private bool ProductExists(int id)
        {
            return _context.products.Any(e => e.PRODUCT_ID == id);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerDatos()
        {
            var todos = await _context.products.Include(c => c.Product_categories).ToListAsync();
            return Json(new { data = todos });
        }
    }
}
