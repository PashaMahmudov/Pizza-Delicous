using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza.DAL;
using pizza.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace pizza.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public MenuViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _db.categories.Include(c => c.Products).ToListAsync();

            return View(categories);
        }
    }
}
