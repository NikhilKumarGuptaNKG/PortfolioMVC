using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Data;
using PortfolioMVC.Models;

namespace PortfolioMVC.Controllers
{
    
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var about = _context.Contacts.FirstOrDefault();
            return View(about);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Contact c)
        {
            _context.Contacts.Add(c);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            return View(_context.Contacts.Find(id));
        }

        [HttpPost]
        public IActionResult Edit([FromBody] Contact contact)
        {
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return Json(new { success = true });
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            return View(_context.Contacts.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _context.Contacts.Find(id);
            _context.Contacts.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
