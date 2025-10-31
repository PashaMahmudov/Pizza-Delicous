using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Linq;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeSliderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeSliderController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index
        public IActionResult Index()
        {
            var sliders = _db.Sliders.ToList();
            return View(sliders);
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
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                // Şəkil yükləmə
                if (slider.formFile != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + slider.formFile.FileName;
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        slider.formFile.CopyTo(stream);
                    }

                    slider.Img = "/uploads/" + uniqueFileName;
                }

                _db.Sliders.Add(slider);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // Edit - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var slider = _db.Sliders.Find(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                var existSlider = _db.Sliders.Find(slider.Id);
                if (existSlider == null) return NotFound();

                existSlider.Title = slider.Title;
                existSlider.Subheading = slider.Subheading;
                existSlider.Description = slider.Description;
                existSlider.IsActive = slider.IsActive;

                if (slider.formFile != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + slider.formFile.FileName;
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        slider.formFile.CopyTo(stream);
                    }

                    existSlider.Img = "/uploads/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var slider = _db.Sliders.Find(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        // Delete - POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var slider = _db.Sliders.Find(id);
            if (slider == null) return NotFound();

            _db.Sliders.Remove(slider);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
