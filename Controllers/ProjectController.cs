using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Data;
using PortfolioMVC.Models;

namespace PortfolioMVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }
        // DETAILS
        public IActionResult Details(int id)
        {
            var project = _context.Projects.Find(id);
            return View(project);
        }

        // CREATE (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // EDIT (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.Find(id);
            return View(project);
        }

        // EDIT (POST)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // DELETE (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.Find(id);
            return View(project);
        }

        // DELETE (POST)
        
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _context.Projects.Find(id);
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}