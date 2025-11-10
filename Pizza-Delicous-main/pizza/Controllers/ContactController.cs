using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using pizza.ViewModels;

namespace pizza.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var contact = _db.Contacts.FirstOrDefault();

            var contactVM = new ContactVM
            {
                Contact = contact,
                ContactMessage = new ContactMessage()
            };

            return View(contactVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(ContactVM model)
        {
            // CRITICAL: Model binding üçün yoxlama
            if (model.ContactMessage == null)
            {
                ModelState.AddModelError("", "Mesaj məlumatları boşdur");
                model.Contact = _db.Contacts.FirstOrDefault();
                return View("Index", model);
            }

            // Validasiya yoxlaması
            if (string.IsNullOrWhiteSpace(model.ContactMessage.FirstName) ||
                string.IsNullOrWhiteSpace(model.ContactMessage.LastName) ||
                string.IsNullOrWhiteSpace(model.ContactMessage.Message))
            {
                ModelState.AddModelError("", "Bütün xanaları doldurun");
                model.Contact = _db.Contacts.FirstOrDefault();
                return View("Index", model);
            }

            try
            {
                model.ContactMessage.DateTime = DateTime.Now;
                _db.contactMessages.Add(model.ContactMessage);
                _db.SaveChanges();

                TempData["SuccessMessage"] = "Mesajınız uğurla göndərildi!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Xəta baş verdi: " + ex.Message);
                model.Contact = _db.Contacts.FirstOrDefault();
                return View("Index", model);
            }
        }
    }
}