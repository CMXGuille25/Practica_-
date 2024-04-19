using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRACTICA__.Data;
using PRACTICA__.Models;

namespace PRACTICA__.Controllers
{
    public class RegionsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegionsController
        public ActionResult Index()
        {
            IEnumerable<Regions> listaRegions = _context.regions;
            return View(listaRegions);
        }

        // GET: RegionsController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = _context.regions.FirstOrDefault(c => c.REGION_ID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: RegionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Regions regions)
        {
            if (ModelState.IsValid)
            {
                _context.regions.Add(regions);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(regions);
        }

        // GET: RegionsController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var cust = _context.regions.Find(id);
            return View(cust);
        }

        // POST: RegionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Regions regions)
        {
            if (ModelState.IsValid)
            {
                _context.regions.Update(regions);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: RegionsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.regions.FirstOrDefault(c => c.REGION_ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: RegionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var region = _context.regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            _context.regions.Remove(region);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
