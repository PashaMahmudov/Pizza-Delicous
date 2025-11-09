using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using pizza.ViewModels;

namespace pizza.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _db;


        public ServicesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var ServiceVM = new ServiceVM
            {
                meals = _db.meals.ToList() 
            };

            return View(ServiceVM);
        }
    }
}
