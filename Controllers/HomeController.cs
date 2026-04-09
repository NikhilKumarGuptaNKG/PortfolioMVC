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
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                Skills = _context.Skills.ToList(),
                Projects = _context.Projects.ToList()
            };

            return View(vm);
        }
        public IActionResult Details(int id)
        {
            var projects = new List<Project>
            {
                new Project {
                    Id = 1,
                    Title = "Blast Furnace Monitoring System",
                    Description = "Real-time monitoring system using ASP.NET, WCF, and ADO.NET",
                    Technologies = "ASP.NET, WCF, SQL Server"
                },
                new Project {
                    Id = 2,
                    Title = "Liquid Level Monitoring System",
                    Description = "Industrial monitoring system using sensors and OPC integration",
                    Technologies = "C#, OPC, SQL"
                }
            };

            var project = projects.FirstOrDefault(p => p.Id == id);

            return View(project);
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
    }
}
