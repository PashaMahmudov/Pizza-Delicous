using Microsoft.AspNetCore.Mvc;

namespace pizza.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
