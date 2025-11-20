using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        public IActionResult BlogDetail(int id)
        {
            var blogPost = _db.blogs.Include(x => x.BlogDetail).FirstOrDefault(x => x.Id == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

    }
}
