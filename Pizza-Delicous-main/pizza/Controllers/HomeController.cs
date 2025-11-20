using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza.DAL;
using pizza.Models;
using pizza.ViewModels;

namespace pizza.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ApplicationDbContext _db;
        

        public HomeController(ApplicationDbContext db) 
        {
            _db = db;
        }

        public IActionResult Index()
        {
         
            ContactMessage contactMessages = _db.contactMessages.FirstOrDefault();
            
            HomeVM homeVM = new HomeVM
            {
                Sliders = _db.Sliders.Where(x => x.IsActive).ToList(),
                Contact = _db.Contacts.FirstOrDefault(),
                ContactMessage = null,
                BlogPosts = _db.blogs.Take(3).ToList(),
                
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                _db.contactMessages.Add(model);
                _db.SaveChanges();
                TempData["Success"] = "Your message has been sent!";
                return RedirectToAction("Index");
            }

            // ✅ DÜZGÜN HƏLL - HomeVM yaradıb göndəririk
            HomeVM homeVM = new HomeVM
            {
                Sliders = _db.Sliders.Where(x => x.IsActive).ToList(),
                Contact = _db.Contacts.FirstOrDefault(),
                ContactMessage = model, // Xətaları göstərmək üçün
                BlogPosts = _db.blogs.Take(3).ToList()
            };

            return View("Index", homeVM);
        }

        public IActionResult BlogDetail(int id)
        {

            if (id == null)
            {
                return BadRequest();
            }
            BlogPost blogPost = _db.blogs.Include(x=>x.BlogDetail).FirstOrDefault(x=>x.Id == id);

            if(blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
