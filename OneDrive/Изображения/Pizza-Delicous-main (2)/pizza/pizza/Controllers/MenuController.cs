using Microsoft.AspNetCore.Mvc;

namespace pizza.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
