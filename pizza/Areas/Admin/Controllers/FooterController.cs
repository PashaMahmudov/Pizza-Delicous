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
    public class FooterController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FooterController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index - siyahı
        public IActionResult Index()
        {
            var footers = _db.Footers.ToList();
            return View(footers);
        }

        // Read - detallar
        public IActionResult Read(int id)
        {
            var footer = _db.Footers.FirstOrDefault(f => f.Id == id);
            if (footer == null) return NotFound();
            return View(footer);
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
        public IActionResult Create(Footer footer)
        {
            if (ModelState.IsValid)
            {
                // Blog şəkillərini upload et
                if (footer.FormFile1 != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(footer.FormFile1.FileName);
                    string filePath1 = Path.Combine(uploadFolder, uniqueFileName1);

                    using (var stream = new FileStream(filePath1, FileMode.Create))
                    {
                        footer.FormFile1.CopyTo(stream);
                    }

                    footer.BlogImage1 = "/uploads/" + uniqueFileName1;
                }

                if (footer.FormFile2 != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(footer.FormFile2.FileName);
                    string filePath2 = Path.Combine(uploadFolder, uniqueFileName2);

                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        footer.FormFile2.CopyTo(stream);
                    }

                    footer.BlogImage2 = "/uploads/" + uniqueFileName2;
                }

                _db.Footers.Add(footer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(footer);
        }

        // Edit - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var footer = _db.Footers.Find(id);
            if (footer == null) return NotFound();
            return View(footer);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Footer footer)
        {
            if (ModelState.IsValid)
            {
                var existingFooter = _db.Footers.Find(footer.Id);
                if (existingFooter == null) return NotFound();

                // About & Contact
                existingFooter.AboutText = footer.AboutText;
                existingFooter.Address = footer.Address;
                existingFooter.Phone = footer.Phone;
                existingFooter.Email = footer.Email;

                // Socials
                existingFooter.Twitter = footer.Twitter;
                existingFooter.Facebook = footer.Facebook;
                existingFooter.Instagram = footer.Instagram;

                // Blog info
                existingFooter.BlogTitle1 = footer.BlogTitle1;
                existingFooter.BlogDate1 = footer.BlogDate1;
                existingFooter.BlogAuthor1 = footer.BlogAuthor1;
                existingFooter.BlogComment1 = footer.BlogComment1;

                existingFooter.BlogTitle2 = footer.BlogTitle2;
                existingFooter.BlogDate2 = footer.BlogDate2;
                existingFooter.BlogAuthor2 = footer.BlogAuthor2;
                existingFooter.BlogComment2 = footer.BlogComment2;

                // Blog şəkilləri upload
                if (footer.FormFile1 != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(footer.FormFile1.FileName);
                    string filePath1 = Path.Combine(uploadFolder, uniqueFileName1);

                    using (var stream = new FileStream(filePath1, FileMode.Create))
                    {
                        footer.FormFile1.CopyTo(stream);
                    }

                    existingFooter.BlogImage1 = "/uploads/" + uniqueFileName1;
                }

                if (footer.FormFile2 != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(footer.FormFile2.FileName);
                    string filePath2 = Path.Combine(uploadFolder, uniqueFileName2);

                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        footer.FormFile2.CopyTo(stream);
                    }

                    existingFooter.BlogImage2 = "/uploads/" + uniqueFileName2;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(footer);
        }

        // Delete - GET
        public IActionResult Delete(int id)
        {
            var footer = _db.Footers.Find(id);
            if (footer == null) return NotFound();
            return View(footer);
        }

        // Delete - POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var footer = _db.Footers.Find(id);
            if (footer == null) return NotFound();

            _db.Footers.Remove(footer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
