using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Data;
using PortfolioMVC.Models;

namespace PortfolioMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        private readonly AppDbContext _context;

        public QuestionController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        public IActionResult Create(int skillId)
        {
            var question = new Question
            {
                SkillId = skillId
            };

            return View(question);
        }

        [HttpPost]
        public IActionResult Create(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();

            return RedirectToAction(
                "Details",
                "Skill",
                new { id = question.SkillId });
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            var question = _context.Questions.Find(id);

            return View(question);
        }

        [HttpPost]
        public IActionResult Edit(Question question)
        {
            _context.Questions.Update(question);
            _context.SaveChanges();

            return RedirectToAction(
                "Details",
                "Skill",
                new { id = question.SkillId });
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var question = _context.Questions.Find(id);

            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var question = _context.Questions.Find(id);

            int skillId = question.SkillId;

            _context.Questions.Remove(question);
            _context.SaveChanges();

            return RedirectToAction(
                "Details",
                "Skill",
                new { id = skillId });
        }
    }
}