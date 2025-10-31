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
    public class OurMenusController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OurMenusController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // INDEX - siyahı
        public IActionResult Index()
        {
            var menuList = _db.OurMenus.ToList();
            return View(menuList);
        }

        // READ - detallar
        public IActionResult Read(int id)
        {
            var menu = _db.OurMenus.FirstOrDefault(x => x.Id == id);
            if (menu == null) return NotFound();
            return View(menu);
        }

        // CREATE - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OurMenu menu)
        {
            if (ModelState.IsValid)
            {
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

        // EDIT - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var menu = _db.OurMenus.Find(id);
            if (menu == null) return NotFound();
            return View(menu);
        }

        // EDIT - POST
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

                // Yeni şəkil varsa
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

        // TOGGLE STATUS - aktiv/deaktiv
        [HttpGet]
        public IActionResult ToggleStatus(int id)
        {
            var menu = _db.OurMenus.Find(id);
            if (menu == null) return NotFound();

            menu.IsActive = !menu.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var menu = _db.OurMenus.Find(id);
            if (menu == null) return NotFound();
            return View(menu);
        }

        // DELETE - POST
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
