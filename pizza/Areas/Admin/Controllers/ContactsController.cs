using Microsoft.AspNetCore.Mvc;
using pizza.DAL;
using pizza.Models;

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
        public IActionResult Index()
        {
            Contact contact = _db.Contacts.FirstOrDefault();
            return View(contact);
        }
        //read bolmesi
        public IActionResult Read(int id) 
        {
            var contact = _db.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }   
            return View(contact);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

       //edit

        //delete bolmesi
        public IActionResult Delete(int id)
        {
            var contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
        //yenilenme qarsini alir 
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _db.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            _db.Contacts.Remove(contact);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    //view isle hamsinin sabah
}

