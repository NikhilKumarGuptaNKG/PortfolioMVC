using System.Collections.Generic;
using PortfolioMVC.Models;

namespace PortfolioMVC.ViewModels
{
    public class HomeViewModel
    {
        public About? About { get; set; }
        public List<Experience> Experiences { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public List<Skill> Skills { get; set; } = new();
        public List<Strength> Strengths { get; set; } = new();
        public Contact? Contact { get; set; }
        public List<Education> Educations { get; set; } = new();
    }
}
