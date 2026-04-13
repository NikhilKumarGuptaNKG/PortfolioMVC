using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddHeroSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeroSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Company = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyAddress = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ParmanentAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroSections", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroSections");
        }
    }
}
