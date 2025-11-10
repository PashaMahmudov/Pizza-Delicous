using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using System.Linq;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactMessageController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactMessageController(ApplicationDbContext db)
        {
            _db = db;
        }

        // READ - List all messages
        public IActionResult Index()
        {
            var messages = _db.contactMessages.OrderByDescending(x => x.DateTime).ToList();
            return View(messages);
        }

        // READ - Details
        public IActionResult Read(int id)
        {
            var message = _db.contactMessages.FirstOrDefault(x => x.Id == id);
            if (message == null) return NotFound();
            return View(message);
        }

        // DELETE - GET
        public IActionResult Delete(int id)
        {
            var message = _db.contactMessages.FirstOrDefault(x => x.Id == id);
            if (message == null) return NotFound();
            return View(message);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var message = _db.contactMessages.FirstOrDefault(x => x.Id == id);
            if (message == null) return NotFound();

            _db.contactMessages.Remove(message);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
