namespace PortfolioMVC.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string Institute { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
