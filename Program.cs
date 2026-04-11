using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioMVC.Data;
using PortfolioMVC.Models;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Configuration["DatabaseProvider"];

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Database configuration (Toggle)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (provider == "SqlServer")
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("SqlServer"));
    }
    else
    {
        options.UseSqlite(
            builder.Configuration.GetConnectionString("SQLite"));
    }
});

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// 🔥 DB Migration + Seeding (VERY IMPORTANT)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();

    // ✅ Ensure DB + Tables created
    await context.Database.MigrateAsync();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string adminRole = "Admin";
    string adminEmail = "nkg@gmail.com";
    string adminPassword = builder.Configuration["AdminPassword"] ?? "Admin@123";

    // Create Role
    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }

    // Create Admin User
    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
        user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(user, adminPassword);
        await userManager.AddToRoleAsync(user, adminRole);
    }

    // ---------- Seed Data ----------

    if (!context.Abouts.Any())
    {
        context.Abouts.Add(new About
        {
            Title = "Hello, I'm Nikhil Gupta",
            Content = "I am a Full Stack Developer with experience in C#, ASP.NET, SQL, and JavaScript."
        });
    }

    if (!context.Experiences.Any())
    {
        context.Experiences.AddRange(
            new Experience
            {
                Role = "Junior Software Developer",
                Company = "Napasoft (Client: Tata Steel)",
                DateRange = "Nov 2023 – Present",
                Order = 1,
                Description = "Worked on enterprise applications and real-time systems."
            },
            new Experience
            {
                Role = "Intern – Full Stack Developer",
                Company = "Napasoft",
                DateRange = "Jan 2023 – Aug 2023",
                Order = 2,
                Description = "Built web and desktop applications using ASP.NET."
            }
        );
    }

    if (!context.Strengths.Any())
    {
        context.Strengths.AddRange(
            new Strength { Title = "Strong problem-solving skills" },
            new Strength { Title = "Quick learner" },
            new Strength { Title = "Clean coding practices" }
        );
    }

    if (!context.Contacts.Any())
    {
        context.Contacts.Add(new Contact
        {
            Email = "nkg@email.com",
            Phone = "7903892879",
            LinkedIn = "https://www.linkedin.com/in/nikhil-gupta-nkg/",
            PortfolioUrl = "https://nikhil-portfolio-mvc-ezf4g5akechyh4gj.centralindia-01.azurewebsites.net"
        });
    }

    if (!context.Projects.Any())
    {
        context.Projects.Add(new Project
        {
            Title = "Sample Project",
            Description = "Demo project for portfolio",
            Technologies = "ASP.NET Core, SQLite"
        });
    }

    if (!context.Skills.Any())
    {
        context.Skills.AddRange(
            new Skill { Name = "C#" },
            new Skill { Name = "ASP.NET Core" },
            new Skill { Name = "SQL" }
        );
    }

    await context.SaveChangesAsync();
}

app.Run();