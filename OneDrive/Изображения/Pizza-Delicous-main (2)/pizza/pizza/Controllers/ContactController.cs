using Microsoft.AspNetCore.Mvc;

namespace pizza.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
