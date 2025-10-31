using Microsoft.AspNetCore.Mvc;

namespace pizza.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
