using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizza.DAL;
using pizza.Models;
using System.Threading.Tasks;

namespace pizza.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public AboutViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _db.Abouts.Where(x=>x.IsActive == true).FirstOrDefaultAsync();
            return View(about); 
        }
    }
}


        //private readonly ApplicationDbContext _db;




        //public ContactsController(ApplicationDbContext db)
        //{

        //    _db = db;

        //}


