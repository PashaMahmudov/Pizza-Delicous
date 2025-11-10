using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using System.Linq;
using System.Threading.Tasks;

namespace pizza.ViewComponents
{
    public class OurMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public OurMenuViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menus = _db.OurMenus.Where(p => p.IsActive).ToList();
            return View(menus);
        }
    }
}
