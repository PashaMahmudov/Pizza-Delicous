using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurServicesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OurServicesController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // ✅ LIST
        public IActionResult Index()
        {
            List<OurService> services = _db.OurServis.ToList();
            return View(services);
        }

        // ✅ READ (Details)
        public IActionResult Read(int id)
        {
            var service = _db.OurServis.FirstOrDefault(x => x.Id == id);
            if (service == null)
                return NotFound();
            return View(service);
        }

        // ✅ CREATE (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ✅ CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OurService service)
        {
            if (ModelState.IsValid)
            {
                if (service.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(service.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        service.FormFile.CopyTo(stream);
                    }

                    service.Img = "/uploads/" + uniqueFileName;
                }

                _db.OurServis.Add(service);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // ✅ EDIT (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var service = _db.OurServis.FirstOrDefault(x => x.Id == id);
            if (service == null)
                return NotFound();
            return View(service);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OurService service)
        {
            var existing = _db.OurServis.FirstOrDefault(x => x.Id == service.Id);
            if (existing == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                existing.Title = service.Title;
                existing.Description = service.Description;
                existing.Icon = service.Icon;
                existing.IsActive = service.IsActive;

                if (service.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(service.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        service.FormFile.CopyTo(stream);
                    }

                    existing.Img = "/uploads/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // ✅ DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var service = _db.OurServis.FirstOrDefault(x => x.Id == id);
            if (service == null)
                return NotFound();

            _db.OurServis.Remove(service);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ✅ TOGGLE STATUS (IsActive)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var service = _db.OurServis.FirstOrDefault(x => x.Id == id);
            if (service == null)
                return NotFound();

            service.IsActive = !service.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
