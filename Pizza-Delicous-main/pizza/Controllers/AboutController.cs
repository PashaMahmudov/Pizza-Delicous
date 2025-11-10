using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using pizza.ViewModels;

namespace pizza.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _db;


        public AboutController (ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var contact = _db.Contacts.FirstOrDefault();
            var contactMessage = _db.contactMessages.FirstOrDefault();
            var chefs = _db.chefs.ToList(); 

            var aboutVM = new AboutVM
            {
                Contact = contact,
                ContactMessage = contactMessage,
                Chefs = chefs 
            };

            return View(aboutVM);
        }

    }
}

