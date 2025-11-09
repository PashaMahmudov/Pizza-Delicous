using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using pizza.ViewModels;
using System.Security.Cryptography.Pkcs;

namespace pizza.Controllers
{
    

    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;


        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }
      
        public IActionResult Index()
        {
            //var AboutVM = new AboutVM
            //{
            //    ContactInfo = contactInfo
            //};


            return View();
        }
    }
}
