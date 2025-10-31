using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
           List<Slider> sliders = _db.Sliders.Where(x=>x.IsActive).ToList();
            Contact contact = _db.Contacts.FirstOrDefault();
            ContactMessage contactMessages = _db.contactMessages.FirstOrDefault();
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Contact = contact,
                ContactMessage = null
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

            return View("Index", model);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
