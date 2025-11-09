using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;

namespace pizza.Controllers
{
    public class BlogController : Controller
    {

        private readonly ApplicationDbContext _db;

        public BlogController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

          List<BlogPost> posts = _db.blogs.ToList();
            return View(posts);
        }
    }
}
