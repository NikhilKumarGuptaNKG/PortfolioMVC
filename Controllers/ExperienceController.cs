using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Data;
using PortfolioMVC.Models;


public class ExperienceController : Controller
{
    private readonly AppDbContext _context;

    public ExperienceController(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        var data = _context.Experiences.OrderBy(x => x.Order).ToList();
        return View(data);
    }
    // DETAILS
    public IActionResult Details(int id)
    {
        var Experience = _context.Experiences.Find(id);
        return View(Experience);
    }

    // CREATE
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Experience exp)
    {
        _context.Experiences.Add(exp);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // EDIT
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var data = _context.Experiences.Find(id);
        return View(data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(Experience exp)
    {
        _context.Experiences.Update(exp);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // DELETE
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var data = _context.Experiences.Find(id);
        return View(data);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var data = _context.Experiences.Find(id);
        _context.Experiences.Remove(data);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}