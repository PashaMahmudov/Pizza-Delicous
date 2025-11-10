using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza.DAL;
using pizza.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // ✅ Index (Read all)
        public async Task<IActionResult> Index()
        {
            var products = await _db.products.Include(p => p.Category).ToListAsync();
            return View(products);
        }

        // ✅ Create GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _db.categories.ToList();
            return View();
        }

        // ✅ Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile formFile) // Product Modelini 'product' adlandırmaq daha yaxşıdır
        {
            // 1. Faylın mütləq yüklənməsi yoxlanışı
            if (formFile == null)
            {
                // Əgər fayl yoxdursa, ModelState-ə xəta əlavə et
                ModelState.AddModelError("formFile", "Məhsul şəkli mütləqdir.");
            }

            // 2. Digər Model sahələrinin (Title, Price və s.) yoxlanışı
            // Fayl yoxdursa və ya digər sahələrdə xəta varsa, aşağıdakı blok işləyəcək
            if (!ModelState.IsValid)
            {
                // Səhv varsa, kateqoriyaları yenidən doldurub View-a qayıt
                ViewBag.Categories = _db.categories.ToList();
                return View(product); // 'menu' əvəzinə 'product' istifadə edilir
            }

            // 3. Şəkil yükləmə məntiqi (ModelState.IsValid olduğu üçün formFile artıq null deyil)
            // Şəkli yaddaşa yazın
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string uniqueFileName = Guid.NewGuid() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            // DB-yə yazılacaq URL-i Product obyektinə əlavə et
            product.Img = "/uploads/" + uniqueFileName;

            // 4. Məhsulu DB-yə əlavə et
            _db.products.Add(product);
            await _db.SaveChangesAsync();

            // 5. Uğurlu əməliyyatdan sonra Index səhifəsinə yönləndir
            return RedirectToAction("Index");
        }


        // ✅ Edit GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _db.products.Find(id);
            if (product == null) return NotFound();

            ViewBag.Categories = _db.categories.ToList();
            return View(product);
        }

        // ✅ Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _db.products.Find(product.Id);
                if (existingProduct == null) return NotFound();

                existingProduct.Title = product.Title;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.IsActive = product.IsActive;

                if (formFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid() + "_" + formFile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    existingProduct.Img = "/uploads/" + uniqueFileName;
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _db.categories.ToList();
            return View(product);
        }

        // ✅ Read (Details)
        public IActionResult Read(int id)
        {
            var product = _db.products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // ✅ Delete GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _db.products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // ✅ Delete POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _db.products.Find(id);
            if (product == null) return NotFound();

            _db.products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ✅ ToggleStatus
        [HttpGet]
        public IActionResult ToggleStatus(int id)
        {
            var product = _db.products.Find(id);
            if (product == null) return NotFound();

            product.IsActive = !product.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
