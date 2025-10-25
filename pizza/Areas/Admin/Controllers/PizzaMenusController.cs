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
    public class PizzaMenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaMenuController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index: bütün pizzalar
        public IActionResult Index()
        {
            var pizzas = _db.PizzaMenus.ToList();
            return View(pizzas);
        }

        // Read: pizza detallarını göstər
        public IActionResult Read(int id)
        {
            var pizza = _db.PizzaMenus.FirstOrDefault(x => x.Id == id);
            if (pizza == null) return NotFound();
            return View(pizza);
        }

        // Create: GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaMenu pizzaMenu)
        {
            if (ModelState.IsValid)
            {
                if (pizzaMenu.formFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid() + "_" + pizzaMenu.formFile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        pizzaMenu.formFile.CopyTo(stream);
                    }

                    pizzaMenu.ImageUrl = "/uploads/" + uniqueFileName;
                }

                _db.PizzaMenus.Add(pizzaMenu);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pizzaMenu);
        }

        // Edit: GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var pizza = _db.PizzaMenus.Find(id);
            if (pizza == null) return NotFound();
            return View(pizza);
        }

        // Edit: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PizzaMenu pizzaMenu)
        {
            if (ModelState.IsValid)
            {
                var existingPizza = _db.PizzaMenus.Find(pizzaMenu.Id);
                if (existingPizza == null) return NotFound();

                existingPizza.Name = pizzaMenu.Name;
                existingPizza.Description = pizzaMenu.Description;
                existingPizza.Price = pizzaMenu.Price;

                if (pizzaMenu.formFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid() + "_" + pizzaMenu.formFile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        pizzaMenu.formFile.CopyTo(stream);
                    }

                    existingPizza.ImageUrl = "/uploads/" + uniqueFileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizzaMenu);
        }

        // Toggle Status
        [HttpGet]
        public IActionResult ToggleStatus(int id)
        {
            var pizza = _db.PizzaMenus.Find(id);
            if (pizza == null) return NotFound();

            pizza.IsActive = !pizza.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Delete: GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var pizza = _db.PizzaMenus.Find(id);
            if (pizza == null) return NotFound();
            return View(pizza);
        }

        // Delete: POST
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pizza = _db.PizzaMenus.FirstOrDefault(x => x.Id == id);
            if (pizza == null) return NotFound();

            _db.PizzaMenus.Remove(pizza);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
