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
            var galleryList = _db.Galleries.ToList();
            return View(galleryList);
        }

        // Read - detallar
        public IActionResult Read(int id)
        {
            var gallery = _db.Galleries.FirstOrDefault(m => m.Id == id);
            if (gallery == null) return NotFound();
            return View(gallery);
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
        public IActionResult Create(Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                if (gallery.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(gallery.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        gallery.FormFile.CopyTo(stream);
                    }

                    gallery.ImageUrl = "/uploads/" + uniqueFileName;
                }

                _db.Galleries.Add(gallery);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // Edit - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var gallery = _db.Galleries.Find(id);
            if (gallery == null) return NotFound();
            return View(gallery);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                var existingGallery = _db.Galleries.Find(gallery.Id);
                if (existingGallery == null) return NotFound();

                if (gallery.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(gallery.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        gallery.FormFile.CopyTo(stream);
                    }

                    existingGallery.ImageUrl = "/uploads/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // Delete - GET
        public IActionResult Delete(int id)
        {
            var gallery = _db.Galleries.Find(id);
            if (gallery == null) return NotFound();
            return View(gallery);
        }

        // Delete - POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var gallery = _db.Galleries.Find(id);
            if (gallery == null) return NotFound();

            _db.Galleries.Remove(gallery);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
