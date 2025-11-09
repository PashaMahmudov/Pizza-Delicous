using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pizza.DAL;
using pizza.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Abouts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Abouts.ToListAsync());
        }

        // GET: Admin/Abouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // GET: Admin/Abouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Abouts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            // Bu yoxlama keçmirsə, datanı bazaya əlavə etmir.
            // Əgər formda Title və ya Description boşdursa, lakin modeldə [Required] varsa, burada dayanacaq.
            if (!ModelState.IsValid) //nidan elave et
            {
                // Şəkil yükləmə
                if (about.formFile != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + about.formFile.FileName;
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await about.formFile.CopyToAsync(stream);
                    }

                    about.Img = "/uploads/" + uniqueFileName;
                }

                _context.Add(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Əgər ModelState keçmirsə, validation xətalarını göstərmək üçün View-a geri qayıdır.
            return View(about);
        }

        // GET: Admin/Abouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: Admin/Abouts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, About about)
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existAbout = await _context.Abouts.FindAsync(id);
                    if (existAbout == null)
                    {
                        return NotFound();
                    }

                    existAbout.Title = about.Title;
                    existAbout.Description = about.Description;
                    existAbout.IsActive = about.IsActive;

                    // Yeni şəkil yüklənibsə
                    if (about.formFile != null)
                    {
                        // Köhnə şəkli sil
                        if (!string.IsNullOrEmpty(existAbout.Img))
                        {
                            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existAbout.Img.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Yeni şəkil yüklə
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        if (!Directory.Exists(uploadDir))
                            Directory.CreateDirectory(uploadDir);

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + about.formFile.FileName;
                        string filePath = Path.Combine(uploadDir, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await about.formFile.CopyToAsync(stream);
                        }

                        existAbout.Img = "/uploads/" + uniqueFileName;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: Admin/Abouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: Admin/Abouts/Delete/5
        // Düzəliş: Artıq olan 'string confirmDelete' parametri silindi
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about != null)
            {
                // Şəkli sil
                if (!string.IsNullOrEmpty(about.Img))
                {
                    string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, about.Img.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _context.Abouts.Remove(about);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Toggle Status
        [HttpGet]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }

            about.IsActive = !about.IsActive;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(int id)
        {
            return _context.Abouts.Any(e => e.Id == id);
        }
    }
}