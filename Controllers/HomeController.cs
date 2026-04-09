using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Models;
using PortfolioMVC.ViewModels;
using System.Diagnostics;

namespace PortfolioMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                Skills = new List<Skill>
                {
                    new Skill { Id = 1, Name = "C#" },
                    new Skill { Id = 2, Name = "ASP.NET" },
                    new Skill { Id = 3, Name = "SQL" },
                    new Skill { Id = 4, Name = "JavaScript" }
                },

                Projects = new List<Project>
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
                }
            };

            return View(vm);
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
