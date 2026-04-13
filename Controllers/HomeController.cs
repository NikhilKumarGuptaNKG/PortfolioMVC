using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioMVC.Data;
using PortfolioMVC.Models;
using PortfolioMVC.ViewModels;
using System.Diagnostics;

namespace PortfolioMVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
            InsertHeroSection();
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                Hero = _context.HeroSections.FirstOrDefault(), 

                About = _context.Abouts.FirstOrDefault(),
                Experiences = _context.Experiences.OrderBy(x => x.Order).ToList(),
                Strengths = _context.Strengths.ToList(),
                Contact = _context.Contacts.FirstOrDefault(),
                Skills = _context.Skills.ToList(),
                Projects = _context.Projects.ToList(),
                Educations = _context.Educations.OrderBy(x => x.Order).ToList()
            };
            
            return View(vm);
        }
        public void InsertHeroSection()
        {
            var obj = _context.HeroSections.FirstOrDefault();

            if (obj == null) // ✅ correct check
            {
                _context.HeroSections.Add(new HeroSection
                {
                    Name = "Nikhil Gupta",
                    Role = "ASP.NET Developer",
                    Company = "Napasoft",
                    CurrentAddress = "Bengaluru",

                    ProfileImageUrl = "/images/profile/NikhilKumarGupta_Resume_09Apr2026.pdf", // ✅ FIXED
                    CoverImageUrl = "/images/profile/NikhilKumarGupta_Resume_09Apr2026.pdf"        // ✅ FIXED
                });

                _context.SaveChanges();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CreateEducation()
        {
            return PartialView("_CreateEducation");
        }

        [HttpPost]
        public IActionResult CreateEducation(Education model)
        {
            if (ModelState.IsValid)
            {
                _context.Educations.Add(model);
                _context.SaveChanges();
            }
            return Ok();
        }
        public IActionResult EditEducation(int id)
        {
            var data = _context.Educations.Find(id);
            return PartialView("_EditEducation", data);
        }

        [HttpPost]
        public IActionResult EditEducation(Education model)
        {
            if (ModelState.IsValid)
            {
                _context.Educations.Update(model);
                _context.SaveChanges();
            }
            return Ok();
        }

        public IActionResult DeleteEducation(int id)
        {
            var data = _context.Educations.Find(id);
            return PartialView("_DeleteEducation", data);
        }

        [HttpPost]
        public IActionResult DeleteEducationConfirmed(int id)
        {
            var data = _context.Educations.Find(id);
            if (data != null)
            {
                _context.Educations.Remove(data);
                _context.SaveChanges();
            }
            return Ok();
        }
        

        

        
    }
}
