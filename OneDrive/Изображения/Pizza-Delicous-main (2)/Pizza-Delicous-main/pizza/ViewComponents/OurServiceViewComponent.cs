using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza.DAL;
using pizza.Models;
using System.Threading.Tasks;

namespace pizza.ViewComponents
{
    public class OurServiceViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public OurServiceViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync ()
        {
            var services = await _db.OurServis.ToListAsync();
            return View(services);
        }
    }
}
