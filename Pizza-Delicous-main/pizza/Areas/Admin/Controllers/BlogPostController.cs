using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using System;
using System.IO;
using System.Linq;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Blogs")]
    public class BlogPostController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogPostController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Index
        [HttpGet("")]
        public IActionResult Index()
        {
            var blogs = _db.blogs.ToList();
            return View(blogs);
        }

        // Toggle Active/Inactive
        [HttpPost("ToggleStatus/{id}")]
        public IActionResult ToggleStatus(int id)
        {
            var blog = _db.blogs.Find(id);
            if (blog == null) return NotFound();

            blog.IsActive = !blog.IsActive;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Read
        [HttpGet("Read/{id}")]
        public IActionResult Read(int id)
        {
            var blog = _db.blogs.Find(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        // Create GET
        [HttpGet("Create")]
        public IActionResult Create() => View();

        // Create POST
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost blog, IFormFile ImageFile)
        {
            // BU SƏTRLƏRI ƏLAVƏ EDİN - başlanğıcda, if-dən əvvəl
            ModelState.Remove("BlogDetail");
            ModelState.Remove("ImageUrl");
            ModelState.Remove("Date");
            ModelState.Remove("FormFile");

            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string folder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/blogs");
                    Directory.CreateDirectory(folder);

                    string fileName = Guid.NewGuid() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(folder, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    ImageFile.CopyTo(stream);

                    blog.ImageUrl = "/uploads/blogs/" + fileName;
                }
                else
                {
                    blog.ImageUrl = "/images/default_blog.png";
                }

                blog.Date = DateTime.Now;
                blog.IsActive = true;

                _db.blogs.Add(blog);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }


        // Edit GET
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _db.blogs.Find(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogPost blog, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                var existing = _db.blogs.Find(blog.Id);
                if (existing == null) return NotFound();

                existing.Title = blog.Title;
                existing.Description = blog.Description;
                existing.Author = blog.Author;
                existing.Slug = blog.Slug;
                existing.CommentCount = blog.CommentCount;
                existing.IsActive = blog.IsActive;

                if (ImageFile != null)
                {
                    string folder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/blogs");
                    Directory.CreateDirectory(folder);

                    string fileName = Guid.NewGuid() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(folder, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    ImageFile.CopyTo(stream);
                    existing.ImageUrl = "/uploads/blogs/" + fileName;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // Delete GET
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var blog = _db.blogs.Find(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        // Delete POST
        [HttpPost("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var blog = _db.blogs.Find(id);
            if (blog == null) return NotFound();

            _db.blogs.Remove(blog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
