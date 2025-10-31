using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using System.Linq;

namespace pizza.ViewComponents
{
    public class StatisticsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public StatisticsViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var list = _db.Statistics.Where(s => s.IsActive).ToList();
            return View(list);
        }
    }
}
