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
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Contact = contact
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    
        public IActionResult Error()
        {
            return View();
        }
    }
}
