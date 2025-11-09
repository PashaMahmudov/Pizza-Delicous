using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;
using System.Linq;

namespace pizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ContactsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Index
        public IActionResult Index()
        {
            Contact contact = _db.Contacts.FirstOrDefault();
            return View(contact);
        }

        // Read
        [HttpGet]
        public IActionResult Read(int id)
        {
            var contact = _db.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _db.Contacts.Add(contact);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // Edit - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _db.Contacts.Update(contact);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // Delete - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, string confirmDelete)
        {
            var contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            _db.Contacts.Remove(contact);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Toggle Status (Əgər lazımdırsa)
        [HttpGet]
        public IActionResult ToggleStatus(int id)
        {
            var contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            // Əgər Contact modelində IsActive property varsa:
            // contact.IsActive = !contact.IsActive;
            // _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}