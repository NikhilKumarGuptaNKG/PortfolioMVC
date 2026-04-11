using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PortfolioMVC.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        // Architecture explanation
        public string Architecture { get; set; }

        // Your role
        public string MyRole { get; set; }

        // Tech stack
        public string Technologies { get; set; }

        // Detailed work
        public string Contribution { get; set; }

        // Final result
        public string Outcome { get; set; }

        public string ImageUrls { get; set; }

        [NotMapped]
        public List<IFormFile> Images { get; set; }

        [NotMapped]
        public string RemainingImages { get; set; }
    }
}
