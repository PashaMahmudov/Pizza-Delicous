using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza.DAL;

namespace pizza.ViewComponents
{
    public class GalleryImageViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public GalleryImageViewComponent(ApplicationDbContext db) 
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var gallery = await _db.GalleryImages
                 .Where(x => x.IsActive == true)
                 .ToListAsync();

            return View(gallery);
        }
    }
}
