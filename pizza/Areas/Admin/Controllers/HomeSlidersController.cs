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
    public class OurMenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OurMenuController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index - siyahı
        public IActionResult Index()
        {
            var menuList = _db.OurMenus.ToList();
            return View(menuList);
        }

        // Read - detallar
        public IActionResult Read(int id)
        {
            var menu = _db.OurMenus.FirstOrDefault(m => m.Id == id);
            if (menu == null) return NotFound();
            return View(menu);
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
        public IActionResult Create(OurMenu menu)
        {
            if (ModelState.IsValid)
            {
                // Şəkil upload
                if (menu.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(menu.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        menu.FormFile.CopyTo(stream);
                    }

                    menu.ImageUrl = "/uploads/" + uniqueFileName;
                }

                _db.OurMenus.Add(menu);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // Edit - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var menu = _db.OurMenus.Find(id);
            if (menu == null) return NotFound();
            return View(menu);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OurMenu menu)
        {
            if (ModelState.IsValid)
            {
                var existingMenu = _db.OurMenus.Find(menu.Id);
                if (existingMenu == null) return NotFound();

                existingMenu.Name = menu.Name;
                existingMenu.Description = menu.Description;
                existingMenu.Price = menu.Price;
                existingMenu.IsActive = menu.IsActive;

                if (menu.FormFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(menu.FormFile.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        menu.FormFile.CopyTo(stream);
                    }

                    existingMenu.ImageUrl = "/uploads/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // ToggleStatus - aktiv/deaktiv
        [HttpGet]
        public IActionResult ToggleStatus(int id)
        {
            var menu = _db.OurMenus.Find(id);
            if (menu == null) return NotFound();

            menu.IsActive = !menu.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Delete - GET
        public IActionResult Delete(int id)
        {
            var menu = _db.OurMenus.Find(id);
            if (menu == null) return NotFound();
            return View(menu);
        }

        // Delete - POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var menu = _db.OurMenus.Find(id);
            if (menu == null) return NotFound();

            _db.OurMenus.Remove(menu);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
