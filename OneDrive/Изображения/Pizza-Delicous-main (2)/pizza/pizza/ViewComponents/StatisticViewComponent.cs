using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using System.Linq;

namespace pizza.ViewComponents
{
    public class StatisticViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public StatisticViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            // Aktiv statistikaları DB-dən çəkirik
            var statistics = _db.Statistics
                                .Where(s => s.IsActive)
                                .ToList();

            return View(statistics);
        }
    }
}
