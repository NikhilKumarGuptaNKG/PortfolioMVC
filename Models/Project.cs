using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PortfolioMVC.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Architecture { get; set; }
        public string Contribution { get; set; }
        public string Technologies { get; set; }

        public string ImageUrls { get; set; } // saved paths

        [NotMapped]
        public List<IFormFile> Images { get; set; }
        [NotMapped]
        public string RemainingImages { get; set; }
    }
}
