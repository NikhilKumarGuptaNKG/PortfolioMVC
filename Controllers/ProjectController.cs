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
        public async Task<IActionResult> Create(Project project)
        {
            if (project.Images != null && project.Images.Count > 0)
            {
                var imagePaths = new List<string>();

                foreach (var file in project.Images)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        imagePaths.Add("/images/" + fileName);
                    }
                }

                project.ImageUrls = string.Join(",", imagePaths);
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

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
        public async Task<IActionResult> Edit(Project project)
        {
            var existingProject = _context.Projects.Find(project.Id);

            if (existingProject == null)
                return NotFound();

            // Update fields
            existingProject.Title = project.Title;
            existingProject.Description = project.Description;
            existingProject.Architecture = project.Architecture;
            existingProject.Contribution = project.Contribution;
            existingProject.Technologies = project.Technologies;

            var imagePaths = new List<string>();

            // ✅ Use only remaining images (after removal)
            if (!string.IsNullOrEmpty(project.RemainingImages))
            {
                imagePaths.AddRange(project.RemainingImages.Split(','));
            }

            // ✅ Add new images
            if (project.Images != null && project.Images.Count > 0)
            {
                foreach (var file in project.Images)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    imagePaths.Add("/images/" + fileName);
                }
            }

            existingProject.ImageUrls = string.Join(",", imagePaths);

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