using PRACTICA__.Data;
using PRACTICA__.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PRACTICA__.Controllers
{
    public class Product_CategoriesController : Controller
    {
        // GET: Product_CategoriesController

        private readonly ApplicationDbContext _context;

        public Product_CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            IEnumerable<Product_categories> listaCategories = _context.product_categories;
            return View(listaCategories);
        }

        // GET: Product_CategoriesController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.product_categories.FirstOrDefault(c => c.CATEGORY_ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Product_CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product_categories categories)
        {
            if (ModelState.IsValid)
            {
                _context.product_categories.Add(categories);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        // GET: Product_CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var cust = _context.product_categories.Find(id);
            return View(cust);
        }

        // POST: Product_CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_categories categories)
        {
            if (ModelState.IsValid)
            {
                _context.product_categories.Update(categories);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Product_CategoriesController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.product_categories.FirstOrDefault(c => c.CATEGORY_ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Product_CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = _context.product_categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.product_categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
