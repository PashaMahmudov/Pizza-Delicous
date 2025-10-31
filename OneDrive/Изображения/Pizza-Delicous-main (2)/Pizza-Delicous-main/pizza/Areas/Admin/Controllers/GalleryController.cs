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
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GalleryController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index - siyahı
        public IActionResult Index()
        {
            var gallery = _db.GalleryImages.ToList();
            return View(gallery);
        }

        // Read - detallar
        public IActionResult Read(int id)
        {
            var image = _db.GalleryImages.Find(id);
            if (image == null) return NotFound();
            return View(image);
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
        public IActionResult Create(GalleryImage image)
        {
            if (ModelState.IsValid)
            {
                if (image.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/gallery");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.FormFile.CopyTo(stream);
                    }

                    image.Img = "/uploads/gallery/" + uniqueFileName;
                }

                _db.GalleryImages.Add(image);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(image);
        }

        // Edit - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var image = _db.GalleryImages.Find(id);
            if (image == null) return NotFound();
            return View(image);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GalleryImage image)
        {
            if (ModelState.IsValid)
            {
                var existing = _db.GalleryImages.Find(image.Id);
                if (existing == null) return NotFound();

                existing.IsActive = image.IsActive;

                if (image.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/gallery");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.FormFile.CopyTo(stream);
                    }

                    existing.Img = "/uploads/gallery/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(image);
        }

        // Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var image = _db.GalleryImages.Find(id);
            if (image == null) return NotFound();
            return View(image);
        }

        // Delete - POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var image = _db.GalleryImages.Find(id);
            if (image == null) return NotFound();

            _db.GalleryImages.Remove(image);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var image = _db.GalleryImages.Find(id);
            if (image == null) return NotFound();

            image.IsActive = !image.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
