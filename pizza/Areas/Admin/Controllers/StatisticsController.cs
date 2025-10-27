using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using System;
using System.IO;
using System.Linq;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StatisticsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index - siyahı
        public IActionResult Index()
        {
            var statsList = _db.Statistics.ToList();
            return View(statsList);
        }

        // Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Statistics stat)
        {
            if (ModelState.IsValid)
            {
                // Şəkil upload
                if (stat.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(stat.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        stat.FormFile.CopyTo(stream);
                    }

                    stat.Icon = "/uploads/" + uniqueFileName;
                }

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
        public IActionResult Edit(Statistics stat)
        {
            if (ModelState.IsValid)
            {
                var existingStat = _db.Statistics.Find(stat.Id);
                if (existingStat == null) return NotFound();

                existingStat.Name = stat.Name;
                existingStat.Number = stat.Number;
                existingStat.IsActive = stat.IsActive;

                if (stat.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(stat.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        stat.FormFile.CopyTo(stream);
                    }

                    existingStat.Icon = "/uploads/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stat);
        }

        // ToggleStatus - aktiv/deaktiv
        [HttpGet]
        public IActionResult ToggleStatus(int id)
        {
            var stat = _db.Statistics.Find(id);
            if (stat == null) return NotFound();

            stat.IsActive = !stat.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Delete - GET
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
    }
}
