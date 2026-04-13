using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioMVC.Models
{
    public class HeroSection
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;

        public string CurrentAddress { get; set; } = string.Empty;
        public string ParmanentAddress { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        [NotMapped]
        public IFormFile CoverImageFile { get; set; }
        [NotMapped]
        public IFormFile ProfileImageFile { get; set; }
    }
}
