namespace PortfolioMVC.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string PortfolioUrl { get; set; } = string.Empty;
        public string CurrentAdd { get; set; } = string.Empty;
        public string ParmanentAdd { get; set; } = string.Empty;
    }
}
