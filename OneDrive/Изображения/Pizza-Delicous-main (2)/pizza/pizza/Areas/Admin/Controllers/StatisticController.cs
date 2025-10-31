using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using System.Linq;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StatisticController(ApplicationDbContext db) => _db = db;

        // Index - siyahı
        public IActionResult Index()
        {
            var list = _db.Statistics.ToList();
            return View(list);
        }

        // Create - GET
        [HttpGet]
        public IActionResult Create() => View();

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Statistic stat)
        {
            if (ModelState.IsValid)
            {
                _db.Statistics.Add(stat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stat);
        }

        // Edit - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var stat = _db.Statistics.Find(id);
            if (stat == null) return NotFound();
            return View(stat);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Statistic stat)
        {
            if (ModelState.IsValid)
            {
                var existing = _db.Statistics.Find(stat.Id);
                if (existing == null) return NotFound();

                existing.Icon = stat.Icon;
                existing.Number = stat.Number;
                existing.Title = stat.Title;
                existing.IsActive = stat.IsActive;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stat);
        }

        // Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var stat = _db.Statistics.Find(id);
            if (stat == null) return NotFound();
            return View(stat);
        }

        // Delete - POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var stat = _db.Statistics.Find(id);
            if (stat == null) return NotFound();

            _db.Statistics.Remove(stat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Read - GET
        [HttpGet]
        public IActionResult Read(int id)
        {
            var stat = _db.Statistics.Find(id);
            if (stat == null) return NotFound();
            return View(stat);
        }

        // Toggle Status (Active/Deactive)
        public IActionResult ToggleStatus(int id)
        {
            var stat = _db.Statistics.Find(id);
            if (stat == null) return NotFound();

            stat.IsActive = !stat.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
