using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioMVC.Models
{
    public class About
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;
        public string WorkLocation { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string ResumeUrl { get; set; }
       

        [NotMapped]
        public IFormFile ResumeFile { get; set; } 

    }
}
