namespace PortfolioMVC.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public string AnswerText { get; set; } = string.Empty;

        public int OrderNo { get; set; }
        public int SkillId { get; set; }
        public Skill? Skill { get; set; }
    }
}
