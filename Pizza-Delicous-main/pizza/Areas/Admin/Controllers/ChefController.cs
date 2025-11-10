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
    public class ChefController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ChefController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index (siyahı)
        public IActionResult Index()
        {
            var chefs = _db.chefs.ToList();
            return View(chefs);
        }

        // Read
        public IActionResult Read(int id)
        {
            var chef = _db.chefs.Find(id);
            if (chef == null) return NotFound();
            return View(chef);
        }

        // Create (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Chef chef)
        {
            if (!ModelState.IsValid)
            {
                if (chef.FormFile != null)
                {
                    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/chefs");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(chef.FormFile.FileName);
                    string filePath = Path.Combine(folderPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        chef.FormFile.CopyTo(stream);
                    }

                    chef.Img = "/uploads/chefs/" + uniqueFileName;
                }

                _db.chefs.Add(chef);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chef);
        }

        // Edit (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var chef = _db.chefs.Find(id);
            if (chef == null) return NotFound();
            return View(chef);
        }

        // Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Chef chef)
        {
            if (ModelState.IsValid)
            {
                var existing = _db.chefs.Find(chef.Id);
                if (existing == null) return NotFound();

                existing.Name = chef.Name;
                existing.Title = chef.Title;
                existing.Description = chef.Description;
                existing.IsActive = chef.IsActive;

                if (chef.FormFile != null)
                {
                    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/chefs");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(chef.FormFile.FileName);
                    string filePath = Path.Combine(folderPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        chef.FormFile.CopyTo(stream);
                    }

                    existing.Img = "/uploads/chefs/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chef);
        }

        // Delete (GET)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var chef = _db.chefs.Find(id);
            if (chef == null) return NotFound();
            return View(chef);
        }

        // Delete (POST)
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var chef = _db.chefs.Find(id);
            if (chef == null) return NotFound();

            _db.chefs.Remove(chef);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Toggle Active/Passive
        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var chef = _db.chefs.Find(id);
            if (chef == null) return NotFound();

            chef.IsActive = !chef.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
