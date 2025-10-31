using Microsoft.AspNetCore.Mvc;

namespace pizza.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
