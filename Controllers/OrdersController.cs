using PRACTICA__.Data;
using PRACTICA__.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PRACTICA__.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrdersController
        public ActionResult Index()
        {
            var Datos = _context.orders.Include(c => c.Customers).Include(c => c.Employees).ToList();
            return View(Datos);
        }

        // GET: OrdersController/Create
        public async Task<IActionResult> Create()
        {
            var customers = await _context.customers.ToListAsync();
            var customerItems = customers.Select(c => new SelectListItem
            {
                Value = c.CUSTOMER_ID.ToString(), // El valor de la opción será el ID del cliente
                Text = $"{c.CUSTOMER_ID} - {c.NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();
            customerItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a customer --" });
            ViewBag.CUSTOMER_ID = customerItems;

            var empleados = await _context.employees.ToListAsync();
            var empleadoItems = empleados.Select(c => new SelectListItem
            {
                Value = c.EMPLOYEE_ID.ToString(), // El valor de la opción será el ID del cliente
                Text = $"{c.EMPLOYEE_ID} - {c.FIRST_NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();
            empleadoItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a empleado --" });
            ViewBag.SALESMAN_ID = empleadoItems;
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.orders.Add(orders);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: OrdersController/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var customers = await _context.customers.ToListAsync();
            var customerItems = customers.Select(c => new SelectListItem
            {
                Value = c.CUSTOMER_ID.ToString(), // El valor de la opción será el ID del cliente
                Text = $"{c.CUSTOMER_ID} - {c.NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();
            customerItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a customer --" });
            ViewBag.CUSTOMER_ID = customerItems;

            var empleados = await _context.employees.ToListAsync();
            var empleadoItems = empleados.Select(c => new SelectListItem
            {
                Value = c.EMPLOYEE_ID.ToString(), // El valor de la opción será el ID del cliente
                Text = $"{c.EMPLOYEE_ID} - {c.FIRST_NAME}" // El texto de la opción será "ID - Nombre"
            }).ToList();
            empleadoItems.Insert(0, new SelectListItem { Value = "", Text = "-- Select a empleado --" });
            ViewBag.SALESMAN_ID = empleadoItems;

            return View(order);
        }

        // POST: OrdersController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Orders orders)
        {
            if (id != orders.ORDER_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists((int)orders.ORDER_ID))
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
            return View(orders);
        }

        // GET: OrdersController/Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el país con la región asociada
            var order = await _context.orders
                                    .Include(c => c.Customers).Include(c => c.Employees)
                                    .FirstOrDefaultAsync(c => c.ORDER_ID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: OrdersController/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.orders
                                    .Include(c => c.Customers).Include(c => c.Employees)
                                    .FirstOrDefaultAsync(c => c.ORDER_ID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // POST: OrdersController/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.orders.FindAsync(id);
            _context.orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para verificar si existe un país
        private bool OrderExists(int id)
        {
            return _context.orders.Any(e => e.ORDER_ID == id);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerDatos()
        {
            var todos = await _context.orders.Include(c => c.Customers).Include(c => c.Employees).ToListAsync();
            return Json(new { data = todos });
        }
    }
}
