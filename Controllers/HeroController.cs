using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Data;
using PortfolioMVC.Models;

namespace PortfolioMVC.Controllers
{
    public class HeroController : Controller
    {
        private readonly AppDbContext _context;

        public HeroController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Edit()
        {
            var data = _context.HeroSections.FirstOrDefault();
            return PartialView("_EditHero", data);
        }

        [HttpPost]
        public IActionResult Edit(HeroSection model)
        {
            var data = _context.HeroSections.FirstOrDefault();

            if (data != null)
            {
                data.Name = model.Name;
                data.Role = model.Role;
                data.Company = model.Company;
                data.CompanyAddress = "";
                data.CurrentAddress = model.CurrentAddress;

                // PROFILE IMAGE UPLOAD
                if (model.ProfileImageFile != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImageFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        model.ProfileImageFile.CopyTo(stream);
                    }

                    data.ProfileImageUrl = "/images/profile/" + fileName;
                }

                // COVER IMAGE UPLOAD
                if (model.CoverImageFile != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(model.CoverImageFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/cover", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        model.CoverImageFile.CopyTo(stream);
                    }

                    data.CoverImageUrl = "/images/cover/" + fileName;
                }

                _context.SaveChanges();
            }

            return Ok();
        }
    }
}
