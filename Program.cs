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

    // ---------- Seed Data ----------

    if (!context.Abouts.Any())
    {
        context.Abouts.Add(new About
        {
            Title = "Nikhil Kumar Gupta",
            Content = "Software Developer at Napasoft (Client: Tata Steel)",

            Company = "Napasoft (Client: Tata Steel)",
            WorkLocation = "Jamshedpur, Jharkhand",

            Role = "Full Stack Developer",

            Overview = "Curious by nature and driven by problem-solving, I started my journey in software development with a love for mathematics and logic. Over the past 3 years, I have grown into a Full Stack Developer working with C#, ASP.NET, SQL, and JavaScript to build enterprise solutions at Tata Steel.",

            ResumeUrl = "/Files/Resume/resume.pdf"
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
                Description = "Responsible for full development lifecycle including design, development, deployment, API integration, reverse engineering legacy applications, AJAX-based UI, SVG, CanvasJS, and enterprise application development."
            },
            new Experience
            {
                Role = "Intern – Full Stack Developer",
                Company = "Napasoft",
                DateRange = "Jan 2023 – Aug 2023",
                Order = 2,
                Description = "Built Master Mech billing & inventory management system using WinForms + ASP.NET MVC 5 and developed Click Booster Windows game."
            }
        );
    }

    if (!context.Strengths.Any())
    {
        context.Strengths.AddRange(
            new Strength { Title = "Strong debugging & problem-solving skills" },
            new Strength { Title = "Experience in real-time and industrial applications" },
            new Strength { Title = "Quick learner, adaptable to new technologies" },
            new Strength { Title = "Focused on clean coding & performance optimization" }
        );
    }

    if (!context.Contacts.Any())
    {
        context.Contacts.Add(new Contact
        {
            Email = "nikhilkumarguptankg0657@gmail.com",
            Phone = "7903892879",
            LinkedIn = "https://linkedin.com/in/nikhil-gupta-nkg",
            PortfolioUrl = "https://your-render-url.onrender.com",

            CurrentAdd = "Jamshedpur, Jharkhand - 831017",
            ParmanentAdd = "Jamshedpur, Jharkhand - 831017"
        });
    }

    if (!context.Projects.Any())
    {
        context.Projects.AddRange(

            new Project
            {
                Title = "Blast Furnace Monitoring System",
                Description = "Real-time monitoring system for blast furnace parameters in Tata Steel.",
                Architecture = "3-Layer Architecture using ASP.NET Web Forms + WCF + ADO.NET",
                MyRole = "Full Stack Developer",
                Technologies = "ASP.NET Web Forms, C#, WCF, Oracle, AJAX, CanvasJS, Highcharts, SVG",
                Contribution = "Developed UI, Login/Signup, dynamic side navigation, AJAX real-time updates, SVG visualizations, and dashboard monitoring system.",
                Outcome = "Improved monitoring and visualization of furnace parameters."
            },

            new Project
            {
                Title = "Ladle Tracking System",
                Description = "Real-time ladle and crane tracking system for industrial monitoring.",
                Architecture = "ASP.NET + ASMX Web Service + Oracle",
                MyRole = "Full Stack Developer",
                Technologies = "C#, Oracle, AJAX, jQuery, SVG, CanvasJS",
                Contribution = "Developed SVG-based plant layout, AJAX polling, dashboards, CRUD modules, and real-time tracking visualization.",
                Outcome = "Enabled live tracking and operational monitoring."
            },

            new Project
            {
                Title = "PLC Sensor Data Processor",
                Description = "Console application for processing PLC sensor data.",
                Architecture = "Console Application",
                MyRole = "Backend Developer",
                Technologies = "C#, Oracle",
                Contribution = "Processed OPC sensor data, validated tags, and handled continuous data flow.",
                Outcome = "Reliable sensor data processing and storage."
            },

            new Project
            {
                Title = "Process Monitor",
                Description = "Windows utility to automate process execution.",
                Architecture = "WinForms Desktop Application",
                MyRole = "Desktop Application Developer",
                Technologies = "C#, WinForms",
                Contribution = "Built UI for process management and monitoring using Windows APIs.",
                Outcome = "Automated execution and monitoring of multiple applications."
            }
        );
    }

    if (!context.Skills.Any())
    {
        context.Skills.AddRange(

            new Skill { Name = "C#" },
            new Skill { Name = "SQL" },
            new Skill { Name = "JavaScript" },
            new Skill { Name = "ASP.NET" },
            new Skill { Name = "ASP.NET Core MVC" },
            new Skill { Name = "Web API" },
            new Skill { Name = "Entity Framework Core" },
            new Skill { Name = "Identity Core" },
            new Skill { Name = "LINQ" },
            new Skill { Name = "WCF" },
            new Skill { Name = "MVC 5" },
            new Skill { Name = "WinForms" },
            new Skill { Name = "Web Forms" },
            new Skill { Name = "HTML5" },
            new Skill { Name = "CSS3" },
            new Skill { Name = "Bootstrap 5" },
            new Skill { Name = "jQuery / AJAX" },
            new Skill { Name = "Git / GitHub" }

        );
    }

    if (!context.Educations.Any())
    {
        context.Educations.AddRange(

            new Education
            {
                Degree = "B.Sc. Mathematics (Hons.)",
                Institute = "Kolhan University",
                Year = "2022",
                Order = 1
            },

            new Education
            {
                Degree = "Higher Secondary (Science)",
                Institute = "JAC Board",
                Year = "2018",
                Order = 2
            },

            new Education
            {
                Degree = "Matriculation",
                Institute = "JAC Board",
                Year = "2016",
                Order = 3
            }
        );
    }

    await context.SaveChangesAsync();
}

app.Run();