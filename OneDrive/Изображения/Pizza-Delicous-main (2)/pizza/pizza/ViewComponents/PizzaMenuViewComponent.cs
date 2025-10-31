using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza.DAL;
using pizza.Models; 
using System.Threading.Tasks;

namespace pizza.ViewComponents
{
    public class PizzaMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public PizzaMenuViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pizzas = await _db.PizzaMenus.ToListAsync();
            return View(pizzas);
        }
    }
}
