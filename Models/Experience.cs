namespace PortfolioMVC.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DateRange { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
