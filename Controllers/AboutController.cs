using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Data;
using PortfolioMVC.Models;


public class AboutController : Controller
{
    private readonly AppDbContext _context;

    public AboutController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var about = _context.Abouts.FirstOrDefault();
        return View(about);
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(About about)
    {
        _context.Abouts.Add(about);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        return View(_context.Abouts.Find(id));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(About about)
    {
        _context.Abouts.Update(about);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        return View(_context.Abouts.Find(id));
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var data = _context.Abouts.Find(id);
        _context.Abouts.Remove(data);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult AddEducation(Education edu)
    {
        _context.Educations.Add(edu);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult EditEducation(Education edu)
    {
        _context.Educations.Update(edu);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteEducation(int id)
    {
        var data = _context.Educations.Find(id);
        _context.Educations.Remove(data);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}