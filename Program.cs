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
            new Skill { Name = "Interview" },
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

    if (!context.Questions.Any())
    {
        var interviewSkill = context.Skills
            .FirstOrDefault(s => s.Name == "Interview");

        if (interviewSkill != null)
        {
            context.Questions.AddRange(

                new Question
                {
                    QuestionText = "Where are you from?",
                    AnswerText = @"
• I belong from Jharkhand and currently living in Jamshedpur.
• I was in Bengaluru in April to explore some opportunities 
and also for some work related to my higher studies.",

                    OrderNo = 1,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Tell me about yourself?",
                    AnswerText = @"
• Name/ Place/ Education
My name is Nikhil Kumar Gupta. 
I am from Jamshedpur, Jharkhand. 
I completed my B.Sc. in Mathematics Honours. 

• Current Work
Currently, I am working as a Junior Software Developer at Tata Steel client side through Napasoft. 

• Experience with Skills
I have around 3 years of experience with C#, ASP.NET, MVC, SQL, JavaScript, AJAX, HTML, CSS, Bootstrap, and related tech. 

• Passion
I am passionate about learning new technologies and improving my skills.

• Goal
My goal is to build a strong career in software development, keep growing professionally, and become an SME (Subject Matter Expert) in the next five years.",

                    OrderNo = 2,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "What do you mean by SME?",
                    AnswerText = @"
• SME means a person who is an expert in a specific domain or technology. 
• They can solve technical problems. 
• They can guide the team. 
• They can suggest the best solutions. 
• And they have a good understanding of the business and processes.",

                    OrderNo = 3,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Tell me about your family?",
                    AnswerText = @"
• There are four members in my family.
• My father is a businessman. And my mother is a homemaker. 
• I also have a younger brother.
• My family is very supportive in my career and studies.
• Their support helps me stay motivated and positive.",

                    OrderNo = 4,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Tell me about your company?",
                    AnswerText = @"
• Currently, I am working through Napasoft for the client Tata Steel. 
• My company works on software development and industrial application solutions. 
• We develop and maintain applications that help improve monitoring, automation, and operational processes. 
• I mainly work on technologies like C#, ASP.NET, SQL, JavaScript, and related web technologies.",

                    OrderNo = 5,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Tell me about your role?",
                    AnswerText = @"
• Currently, I am working as a Junior Software Developer. 
• My responsibilities include application development, maintenance, debugging, and feature enhancement. 
• I work on both frontend and backend development using technologies such as C#, ASP.NET, ASP.NET Core, SQL, JavaScript, HTML, CSS, and Bootstrap. 
• I also interact with databases, write SQL queries, and support application performance improvements.",

                    OrderNo = 6,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Tell me about your projects?",
                    AnswerText = @"
I have worked on multiple industrial and web-based applications during my experience as a software developer. The major projects I worked on are:

• Blast Furnace Monitoring System 
• Ladle Tracking System 
• Personal Portfolio Project

These projects helped me improve both my technical skills and problem-solving abilities, especially in industrial monitoring and real-time application development.",

                    OrderNo = 7,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "What are you currently doing?",
                    AnswerText = @"
• Currently, I am working as a Junior Software Developer at Tata Steel client side through Napasoft. 
• My work involves application development, maintenance, real-time monitoring systems, and feature enhancement using technologies like C#, ASP.NET, SQL, JavaScript, and ASP.NET Core. 
• Along with my job, I am also improving my skills in ASP.NET Core and modern web development.",

                    OrderNo = 8,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Why are you looking for a change?",
                    AnswerText = @"
• My current role has given me good exposure to industrial applications and software development. 
• However, now I want to move toward opportunities 
• where I can work on modern technologies, take more responsibilities, grow professionally, and enhance my skills.",

                    OrderNo = 9,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Why not continue in your current company?",
                    AnswerText = @"
• My current company has given me good experience in software development. 
• I have learned a lot from my current role and projects. 
• However, the scope of modern technologies and long-term growth opportunities is currently limited for my career goals. 
• Now, I want to work on more modern technologies and take on more responsibilities. 
• That is why I am looking for a change to improve my skills and grow professionally.",

                    OrderNo = 10,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "What are your career goals?",
                    AnswerText = @"
• My career goal is to build a strong career in software development. 
• I want to continuously improve my technical and problem-solving skills. 
• I want to work on modern technologies and challenging projects. 
• In the next few years, I want to become a Subject Matter Expert in software development.",

                    OrderNo = 11,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "What are your strengths?",
                    AnswerText = @"
• My strengths are problem-solving and quick learning. 
• I am a hardworking and self-motivated person. 
• I like learning new technologies and improving my skills. 
• I can adapt quickly to new environments and responsibilities. 
• I also work well in a team and try to complete tasks on time.",

                    OrderNo = 12,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "What are your weaknesses?",
                    AnswerText = @"
• One of my weaknesses is that I sometimes focus too much on perfection, which can take extra time. But now I am improving my time management and balancing speed with accuracy.
• Earlier, I was not very confident in public speaking. But I am continuously improving by practicing communication and speaking more confidently.",

                    OrderNo = 13,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Why should we hire you?",
                    AnswerText = @"
• I believe I am a good fit for this role because I have good knowledge of software development and real project experience.
• I am a quick learner and adaptable to new technologies.
• I am hardworking and focused on improving my skills continuously.
• I am confident that I can contribute to the team and handle responsibilities sincerely.",

                    OrderNo = 14,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "How will you explain when someone wants modern tech and you have no experience on that?",
                    AnswerText = @"
• That is correct that my professional experience is mainly in ASP.NET Web Forms and industrial applications. 
• However, I have already started learning modern technologies like ASP.NET Core, Web API, Entity Framework Core, JWT authentication, and React through self-learning and personal projects. 
• Recently, I also built portfolio and authentication projects using ASP.NET Core technologies. 
• I believe my current development experience and problem-solving skills will help me adapt quickly to modern tech stacks.",

                    OrderNo = 15,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "Do you have experience in .NET Core and how?",
                    AnswerText = @"
• Most of my experience is in ASP.NET Web Forms. 
• However, I have been actively learning and working on ASP.NET Core through self-learning and personal projects. 
• Recently, I built a portfolio project and a JWT-based authentication project using ASP.NET Core MVC and ASP.NET Core Web API. 
• In these projects, I worked with Entity Framework Core, Identity Framework, JWT authentication, SQLite, REST APIs, and frontend integration. 
• Through these projects, I gained hands-on experience with modern .NET development concepts.",

                    OrderNo = 16,
                    SkillId = interviewSkill.Id
                },

                new Question
                {
                    QuestionText = "What technologies have you worked on?",
                    AnswerText = @"I have worked on .NET technologies like C#, ASP.NET, ASP.NET Core, MVC, Web API, SQL Server, JavaScript, AJAX, HTML, CSS, Bootstrap, and WCF services.",

                    OrderNo = 17,
                    SkillId = interviewSkill.Id
                },

                new Question
{
    QuestionText = "Can you explain all these technologies?",
    AnswerText = @"
C#
• C# is an object-oriented programming language developed by Microsoft. 
• I use it for backend development, business logic, APIs, database operations, and application development.

ASP.NET Web Forms
• ASP.NET Web Forms is a framework used to build web applications using server controls and event-driven programming. 
• I used it in industrial monitoring projects and enterprise applications.

ASP.NET Core
• ASP.NET Core is a modern, cross-platform framework for building web applications and APIs. 
• It is faster, lightweight, and widely used for modern application development.

MVC (Model View Controller)
• MVC is an architectural pattern that separates:
Model → Data
View → UI
Controller → Business logic/request handling

Web API
• Web API is used to create RESTful services that allow applications to communicate with each other using HTTP methods like GET, POST, PUT, and DELETE.

SQL Server
• SQL Server is a relational database management system. 
• I use it to store, retrieve, update, and manage application data using SQL queries, stored procedures, and joins.

Entity Framework Core
• EF Core is an ORM (Object Relational Mapper). 
• It allows us to interact with databases using C# objects instead of writing too many SQL queries manually.

ADO.NET
• ADO.NET is used for direct database connectivity in .NET applications.

JavaScript
• JavaScript is a scripting language used to make web pages interactive and dynamic.

AJAX
• AJAX is used for asynchronous communication between client and server without reloading the page.

HTML
• HTML is used to create the structure of web pages.

CSS
• CSS is used for styling and designing web pages.

Bootstrap
• Bootstrap is a frontend framework used to create responsive and mobile-friendly UI designs quickly.

WCF Services
• WCF (Windows Communication Foundation) is used for communication between applications and services.

SQLite
• SQLite is a lightweight database commonly used in small and portable applications.

LINQ
• LINQ stands for Language Integrated Query.

IIS Hosting
• IIS (Internet Information Services) is a web server provided by Microsoft.

Git
• Git is a version control system used to track code changes and manage source code efficiently.

GitHub
• GitHub is a cloud-based platform used to store and manage Git repositories.

Canvas & SVG
• Canvas and SVG are used for graphical visualization and dynamic drawing in web applications.",

    OrderNo = 18,
    SkillId = interviewSkill.Id
},
                
                new Question
{
    QuestionText = "What are your opinion on AI?",
    AnswerText = @"
• I believe AI is a powerful technology that is changing the software industry positively.
• It helps improve productivity and learning.
• However, I also believe strong programming fundamentals and problem-solving skills are still very important because AI works best when guided by skilled developers.
• I see AI as a powerful assistant that helps developers work more efficiently rather than replacing skilled engineers completely.",

    OrderNo = 19,
    SkillId = interviewSkill.Id
},
                
                new Question
{
    QuestionText = "How much do you know about Cloud, Azure, DevOps, and React.js?",
    AnswerText = @"
• I have basic knowledge of Cloud, Azure, DevOps, and React.js through self-learning and personal projects.
• I am continuously learning these technologies because they are important for modern software development.

About Azure
• Microsoft Azure is a cloud platform provided by Microsoft.
• I have explored application hosting and deployment concepts for ASP.NET Core applications.

About DevOps
• DevOps is a combination of Development and Operations.
• It focuses on improving collaboration between development and deployment teams.

About React.js
• React is a JavaScript library used for building dynamic and interactive user interfaces.
• It is component-based and widely used for modern frontend development.

About Cloud
• Cloud computing means using services over the internet instead of using only a local computer or server.",

    OrderNo = 20,
    SkillId = interviewSkill.Id
},
                
                new Question
{
    QuestionText = "Can you have any questions regarding this?",
    AnswerText = @"
About Role
• Can you please explain more about this role and daily responsibilities?

About Team
• How is the team structure for this project?

About Technologies
• What technologies and tools are mainly used in your current projects?

About Learning & Growth
• What kind of learning and growth opportunities are available in the company?

About Projects
• What type of projects will I mainly work on in this role?

About Work Culture
• How would you describe the work culture of the team?

About Next Steps
• What will be the next step in the interview process?",

    OrderNo = 21,
    SkillId = interviewSkill.Id
}
            );
        }
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