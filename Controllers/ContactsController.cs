using PRACTICA__.Data;
using PRACTICA__.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PRACTICA__.Controllers
{
    public class ContactsController : Controller
    {
        // GET: ContactsController

        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ContactsController
        public ActionResult Index()
        {
            var Datos = _context.contacts.Include(c => c.Customers).ToList();
            return View(Datos);
        }

        // GET: ContactsController/Create
        public async Task<IActionResult> Create()
        {
            // Obtener la lista de regiones desde la base de datos
            var countries = await _context.customers.ToListAsync();

            // Convertir la lista de regiones a una lista de objetos SelectListItem
            var countryItems = countries.Select(r => new SelectListItem
            {
                Value = r.CUSTOMER_ID.ToString(), // El valor de la opción será el ID de la región
                Text = $"{r.CUSTOMER_ID} - {r.NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();

            // Agregar una opción por defecto al inicio del menú desplegable
            countryItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a customer --" });

            // Pasar la lista de opciones de regiones a la vista
            ViewBag.CUSTOMER_ID = countryItems;

            return View();
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                _context.contacts.Add(contacts);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contacts);
        }

        // GET: ContactsController/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            // Obtener la lista de regiones disponibles
            var customers = await _context.customers.ToListAsync();

            // Convertir la lista de regiones a una lista de objetos SelectListItem
            var customersItems = customers.Select(r => new SelectListItem
            {
                Value = r.CUSTOMER_ID.ToString(), // El valor de la opción será el ID de la región
                Text = $"{r.CUSTOMER_ID} - {r.NAME}" // El texto de la opción será el nombre de la región
            }).ToList();

            // Pasar la lista de opciones de regiones a la vista
            ViewBag.CUSTOMER_ID = customersItems;

            return View(contact);
        }

        // POST: ContactsController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contacts contacts)
        {
            if (id != contacts.CONTACT_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists((int)contacts.CONTACT_ID))
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
            return View(contacts);
        }

        // GET: ContactsController/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el país con la región asociada
            var contact = await _context.contacts
                                    .Include(c => c.Customers)
                                    .FirstOrDefaultAsync(c => c.CONTACT_ID == id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: ContactsController/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el producto con la categoria asociada
            var contact = await _context.contacts
                                    .Include(c => c.Customers)
                                    .FirstOrDefaultAsync(c => c.CONTACT_ID == id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }


        // POST: ContactsController/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.contacts.FindAsync(id);
            _context.contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar si existe un país
        private bool ContactExists(int id)
        {
            return _context.contacts.Any(e => e.CONTACT_ID == id);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerDatos()
        {
            var todos = await _context.contacts.Include(c => c.Customers).ToListAsync();
            return Json(new { data = todos });
        }
    }
}
