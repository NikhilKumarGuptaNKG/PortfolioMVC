using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Data;
using PortfolioMVC.Models;


public class SkillController : Controller
{
    private readonly AppDbContext _context;

    public SkillController(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public IActionResult Index()
    {
        return View(_context.Skills.ToList());
    }
    // DETAILS
    public IActionResult Details(int id)
    {
        var skill = _context.Skills.Find(id);
        return View(skill);
    }
    // CREATE
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return PartialView("_Create");
    }

    [HttpPost]
    public IActionResult Create(Skill skill)
    {
        _context.Skills.Add(skill);
        _context.SaveChanges();
        return Ok();
    }

    // EDIT
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var skill = _context.Skills.Find(id);
        return PartialView("_Edit", skill);
    }

    [HttpPost]
    public IActionResult Edit(Skill skill)
    {
        _context.Skills.Update(skill);
        _context.SaveChanges();
        return Ok();
    }

    // DELETE
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var skill = _context.Skills.Find(id);
        return PartialView("_Delete", skill);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var data = _context.Skills.Find(id);
        _context.Skills.Remove(data);
        _context.SaveChanges();
        return Ok();
    }





}