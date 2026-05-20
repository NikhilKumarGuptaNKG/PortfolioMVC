namespace PortfolioMVC.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Question>? Questions { get; set; }
    }
}
