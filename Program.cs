using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioMVC.Data;
using PortfolioMVC.Models;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var context = services.GetRequiredService<AppDbContext>();

    string adminRole = "Admin";
    string adminEmail = "admin@gmail.com";
    string adminPassword = "Admin@123";

    // Create Role
    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }

    // Create User
    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
        user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(user, adminPassword);
        await userManager.AddToRoleAsync(user, adminRole);
    }

    // ---------- Seed portfolio data (idempotent) ----------
    // About
    if (!context.Abouts.Any())
    {
        context.Abouts.Add(new About
        {
            Title = "Hello, I'm Nikhil Gupta",
            Content = "I am a Full Stack Developer with 3+ years of experience working at Napasoft (Client: Tata Steel). I specialize in building enterprise-level applications using C#, ASP.NET, SQL, and JavaScript. I enjoy solving complex problems, optimizing performance, and designing scalable applications."
        });
    }

    // Experiences
    if (!context.Experiences.Any())
    {
        context.Experiences.AddRange(
            new Experience
            {
                Role = "Junior Software Developer",
                Company = "Napasoft (Client: Tata Steel)",
                DateRange = "Nov 2023 – Present",
                Order = 1,
                Description = "Worked on full software development lifecycle (Design ? Development ? Deployment). Developed real-time industrial applications using C#, WCF and front-end technologies."
            },
            new Experience
            {
                Role = "Intern – Full Stack Developer",
                Company = "Napasoft",
                DateRange = "Jan 2023 – Aug 2023",
                Order = 2,
                Description = "Developed web and desktop applications using ASP.NET and WinForms. Built projects like billing systems and games."
            }
        );
    }

    // Strengths
    if (!context.Strengths.Any())
    {
        context.Strengths.AddRange(
            new Strength { Title = "Strong problem-solving skills" },
            new Strength { Title = "Experience in real-time industrial systems" },
            new Strength { Title = "Quick learner and adaptable" },
            new Strength { Title = "Focus on clean and optimized code" }
        );
    }

    // Contact
    if (!context.Contacts.Any())
    {
        context.Contacts.Add(new Contact
        {
            Email = "nikhilkumarguptankg0657@gmail.com",
            Phone = "7903892879",
            LinkedIn = "https://linkedin.com/in/nikhil-gupta-nkg",
            PortfolioUrl = "https://your-deployed-link"
        });
    }

    // Optional: seed a sample Project/Skill if none exist (keeps Home page populated)
    if (!context.Projects.Any())
    {
        context.Projects.Add(new Project
        {
            Title = "Sample Project: Blast Furnace Monitoring",
            Description = "Real-time monitoring system used in Tata Steel to track temperature, pressure and levels.",
            Technologies = "ASP.NET Web Forms, C#, WCF, Oracle, AJAX"
        });
    }

    if (!context.Skills.Any())
    {
        context.Skills.AddRange(
            new Skill { Name = "C#" },
            new Skill { Name = "ASP.NET" },
            new Skill { Name = "SQL" }
        );
    }

    await context.SaveChangesAsync();
}
app.Run();
