using Microsoft.AspNetCore.Mvc;
using pizza.DAL;

namespace pizza.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public FooterViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var footer = _db.Footers.FirstOrDefault();
            return View(footer);
        }
    }
}
