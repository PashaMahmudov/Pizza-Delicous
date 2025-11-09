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
    public class MealController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MealController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index
        public IActionResult Index()
        {
            var meals = _db.meals.ToList();
            return View(meals);
        }

        // Read
        public IActionResult Read(int id)
        {
            var meal = _db.meals.Find( id);
            if (meal == null) return NotFound();
            return View(meal);
        }

        // Create GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Meal meal)
        {
            if (ModelState.IsValid)
            {
                if (meal.FormFile != null)
                {
                    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/meals");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(meal.FormFile.FileName);
                    string filePath = Path.Combine(folderPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        meal.FormFile.CopyTo(stream);
                    }

                    meal.ImageUrl = "/uploads/meals/" + uniqueFileName;
                }

                _db.meals.Add(meal);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meal);
        }

        // Edit GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var meal = _db.meals.Find(id);
            if (meal == null) return NotFound();
            return View(meal);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Meal meal)
        {
            if (ModelState.IsValid)
            {
                var existing = _db.meals.Find(meal.Id);
                if (existing == null) return NotFound();

                existing.Name = meal.Name;
                existing.Description = meal.Description;
                existing.Price = meal.Price;
                existing.IsActive = meal.IsActive;

                if (meal.FormFile != null)
                {
                    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/meals");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(meal.FormFile.FileName);
                    string filePath = Path.Combine(folderPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        meal.FormFile.CopyTo(stream);
                    }

                    existing.ImageUrl = "/uploads/meals/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meal);
        }

        // Delete GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var meal = _db.meals.Find(id);
            if (meal == null) return NotFound();
            return View(meal);
        }

        // Delete POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var meal = _db.meals.Find(id);
            if (meal == null) return NotFound();

            _db.meals.Remove(meal);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Toggle Active/Passive
        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var meal = _db.meals.Find(id);
            if (meal == null) return NotFound();

            meal.IsActive = !meal.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
