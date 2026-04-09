using Microsoft.EntityFrameworkCore;
using PortfolioMVC.Models;

namespace PortfolioMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}


