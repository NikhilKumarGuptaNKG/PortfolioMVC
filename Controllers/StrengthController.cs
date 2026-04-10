using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Data;
using PortfolioMVC.Models;

namespace PortfolioMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StrengthController : Controller
    {
        private readonly AppDbContext _context;

        public StrengthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Strengths.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Strength s)
        {
            _context.Strengths.Add(s);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_context.Strengths.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Strength s)
        {
            _context.Strengths.Update(s);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(_context.Strengths.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _context.Strengths.Find(id);
            _context.Strengths.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
