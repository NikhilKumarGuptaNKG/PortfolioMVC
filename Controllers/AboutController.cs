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
    public async Task<IActionResult> Edit(About about)
    {
        var existing = _context.Abouts.Find(about.Id);

        if (existing == null)
            return NotFound();

        // Update fields
        existing.Title = about.Title;
        existing.Content = about.Content;
        existing.Company = about.Company;
        existing.WorkLocation = about.WorkLocation;
        existing.Role = about.Role;
        existing.Overview = about.Overview;

        // HANDLE RESUME UPLOAD
        if (about.ResumeFile != null && about.ResumeFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files");

            // ✅ Ensure folder exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = "resume_" + Guid.NewGuid() + Path.GetExtension(about.ResumeFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await about.ResumeFile.CopyToAsync(stream);
            }

            existing.ResumeUrl = "/Files/" + fileName;
        }
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
}