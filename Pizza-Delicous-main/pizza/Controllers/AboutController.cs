using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using pizza.ViewModels;

namespace pizza.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AboutController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var contact = _db.Contacts.FirstOrDefault();
            var contactMessage = new ContactMessage(); // Boş model
            var chefs = _db.chefs.ToList();

            var aboutVM = new AboutVM
            {
                Contact = contact,
                ContactMessage = contactMessage,
                Chefs = chefs
            };

            return View(aboutVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(ContactMessage model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.DateTime = DateTime.Now;
                    _db.contactMessages.Add(model);
                    _db.SaveChanges();
                    TempData["Success"] = "Mesajınız uğurla göndərildi!";
                    return Redirect("~/About#contact");
                }

                TempData["Error"] = "Zəhmət olmasa bütün xanaları doldurun.";
                return Redirect("~/About#contact");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Xəta baş verdi: " + ex.Message;
                return Redirect("~/About#contact");
            }
        }
    }
}