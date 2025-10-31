//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using pizza.DAL;

//namespace pizza.ViewComponents
//{
//    public class BlogViewComponent : ViewComponent
//    {
//        private readonly ApplicationDbContext _db;

//        public BlogViewComponent(ApplicationDbContext db)
//        {
//            _db = db;
//        }

//        public async Task<IActionResult> InvokeAsync() 
//        {
//            var blogs = await _db.blogPosts.Include(c => c.Blogdetail).ToListAsync();
//            return View(blogs);
//        }

//        public async Task<IActionResult> BlogDetail()
//        {
           
//            return View();
//        }



//    }
//}
