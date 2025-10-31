using Microsoft.AspNetCore.Mvc;
using pizza.DAL;

namespace pizza.ViewComponents
{
    public class GalleryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public GalleryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var galleries = _context.Galleries.ToList();
            return View(galleries);
        }
    }
}
